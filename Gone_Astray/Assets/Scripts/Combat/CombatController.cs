using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour {

    public EncounterController encounterController;
    public bool proceed;
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
        yield return new WaitForSeconds(1);
        for (int i = 0; i < encounterController.enemyHand.Count; i++) {
            enemyHandNumber += encounterController.enemyHand[i];
            enemyHandText += encounterController.enemyHand[i].ToString() + " ";
        }
        enemyHand.GetComponent<Text>().text = enemyHandText;
        //TODO: animation for adding the hand
        yield return new WaitForSeconds(1);
    }

    public void AddFirefly() {
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
            encounterController.enemyScore += 1;
            if(encounterController.enemyScore == 3) {
                encounterController.GameLost();
            }
            else {
                encounterController.NewRound();
            }
        }
    }

}
