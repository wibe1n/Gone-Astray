using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour {

    public EncounterController encounterController;
    public GameObject enemyHand, myHand;
    public int myHandNumber, enemyHandNumber;
    public string myHandText = "";
    public string enemyHandText = "";

    public void PlayersTurn() {
        myHandNumber = 0;
        enemyHandNumber = 0;
        StartCoroutine(PlayerTurn());
    }


    //Players turn routine
    IEnumerator PlayerTurn() {
        for (int i = 0; i < encounterController.myHand.Count; i++) {
            myHandNumber += encounterController.myHand[i];
            myHandText += encounterController.myHand[i].ToString() + " ";
        }
        myHand.GetComponent<Text>().text = myHandText;

        //TODO: animation for adding the hand
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < encounterController.enemyHand.Count; i++) {
            enemyHandNumber += encounterController.enemyHand[i];
            enemyHandText += encounterController.enemyHand[i].ToString() + " ";
        }
        enemyHand.GetComponent<Text>().text = enemyHandText;
        //TODO: animation for adding the hand
        //ball of light size update
        encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);
        //ball of darkness size update
        encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
        yield return new WaitForSeconds(0.5f);
        encounterController.character.proceed = true;
    }


    public void Proceed() {
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn() {
        Debug.Log("enemyTurn");
        while(enemyHandNumber < myHandNumber) {
            encounterController.enemyHand.Add(encounterController.deck[0]);
            enemyHandNumber += encounterController.deck[0];
            encounterController.deck.RemoveAt(0);
            enemyHandText += encounterController.myHand[encounterController.myHand.Count - 1].ToString() + " ";
            enemyHand.GetComponent<Text>().text = enemyHandText;
            //ball of darkness size update
            encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
            yield return new WaitForSeconds(1f);
        }
        if(enemyHandNumber > 21 || myHandNumber > enemyHandNumber) {
            Debug.Log("minä voitin");
            encounterController.RoundWon();
            encounterController.myScore += 1;
            if (encounterController.myScore == 3) {
                encounterController.GameWon();
            }
            else {
                encounterController.NewRound();
            }
        }
        else if (enemyHandNumber > myHandNumber) {
            Debug.Log("vihollinen voitti");
            encounterController.RoundLost();
            encounterController.enemyScore += 1;
            if (encounterController.enemyScore == 3) {
                encounterController.GameLost();
            }
            else {
                encounterController.NewRound();
            }
        }
        else {
            encounterController.NewRound();
        }

        Debug.Log("vuoro ohitse");
    }

    public void AddFirefly() {
        Debug.Log("lisätään firefly");
        if (encounterController.myFireflies.Count == 0) {
            Debug.Log("Not enough fireflies!");
        }
        else {
            encounterController.myHand.Add(encounterController.deck[0]);
            myHandNumber += encounterController.deck[0];
            encounterController.deck.RemoveAt(0);
            encounterController.usedFireflies.Add(encounterController.myFireflies[encounterController.myFireflies.Count - 1]);
            encounterController.myFireflies.RemoveAt(encounterController.myFireflies.Count - 1);
            myHandText += encounterController.myHand[encounterController.myHand.Count - 1].ToString() + " ";
            myHand.GetComponent<Text>().text = myHandText;
        }
        if (myHandNumber > 21) {
            encounterController.RoundLost();
            Debug.Log("mennään täällä");
            encounterController.enemyScore += 1;
            if (encounterController.enemyScore == 3) {
                encounterController.GameLost();
            }
            else {
                encounterController.NewRound();
            }
        }

        //ball of light size update
        encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);

        Debug.Log("pois lisäämisestä");
    }

}
