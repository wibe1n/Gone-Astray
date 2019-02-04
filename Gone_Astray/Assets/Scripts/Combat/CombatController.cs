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
        //Pelaajan käsi esille
        for (int i = 0; i < encounterController.myHand.Count; i++) {
            myHandNumber += encounterController.myHand[i];
            myHandText += encounterController.myHand[i].ToString() + " ";
        }
        myHand.GetComponent<Text>().text = myHandText;

        //TODO: animation for adding the hand
        yield return new WaitForSeconds(0.5f);

        //Vihollisen käsi esille
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
    }


    public void Proceed() {
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn() {
        //Tutorial version
        // Vihollisen vuoro muutetaan pian, en kommentoi
        if(encounterController.tutorial == true) {
            while (enemyHandNumber < myHandNumber) {
                if(enemyHandNumber == 14) {
                    encounterController.enemyHand.Add(2);
                    enemyHandNumber += 2;
                }
                else {
                    encounterController.enemyHand.Add(9);
                    enemyHandNumber += 9;
                }
                enemyHandText += encounterController.enemyHand[encounterController.enemyHand.Count - 1].ToString() + " ";
                enemyHand.GetComponent<Text>().text = enemyHandText;
                //ball of darkness size update
                encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
                encounterController.nextButton.SetActive(true);
                encounterController.proceedButton.SetActive(false);
                encounterController.NextTutorialPart();
                yield return new WaitUntil(() => encounterController.reached == true);
                encounterController.reached = false;
            }
            if (enemyHandNumber > 21 || myHandNumber > enemyHandNumber) {
                encounterController.RoundWon();
                enemyHandNumber = 0;
                myHandNumber = 0;
                encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);
                encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
                enemyHand.GetComponent<Text>().text = "";
                myHand.GetComponent<Text>().text = "";
                yield return new WaitUntil(() => encounterController.reached == true);
                encounterController.GameWon();                
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
        }
        else {
            //Niin kauan kuin vihollisen käsi on pienempi kuin pelaajan vihollinen jatkaa
            while (enemyHandNumber < myHandNumber) {
                //Uusi kortti pakasta. Päivitetään arvo, teksti ja valopallo
                encounterController.enemyHand.Add(encounterController.deck[0]);
                enemyHandNumber += encounterController.deck[0];
                encounterController.deck.RemoveAt(0);
                enemyHandText += encounterController.enemyHand[encounterController.enemyHand.Count - 1].ToString() + " ";
                enemyHand.GetComponent<Text>().text = enemyHandText;
                //ball of darkness size update
                encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
                yield return new WaitForSeconds(1f);
            }
            Debug.Log(enemyHandNumber);
            //Jos vihollisen käsi on yli 21 voittaa pelin
            if (enemyHandNumber > 21 || myHandNumber > enemyHandNumber) {
                encounterController.RoundWon();
                encounterController.GameWon();                
            }
            //Jos vihollisen käsi on isompi kuin pelaajan, häviää kierroksen
            else if (enemyHandNumber > myHandNumber) {
                Debug.Log("vihollinen voitti");
                encounterController.RoundLost();
                encounterController.enemyScore += 1;
                //Jos vihollisen score on kolme, häviää pelin
                if (encounterController.enemyScore == 3) {
                    encounterController.GameLost();
                }
                //Muuten uusi kierros
                else {
                    encounterController.NewRound();
                }
            }
            else {
                encounterController.NewRound();
            }
        }

    }

    public void AddFirefly() {
        //Tutorial versio
        if (encounterController.tutorial == true) {
            encounterController.myHand.Add(9);
            myHandNumber += 9;
            encounterController.usedFireflies.Add(encounterController.myFireflies[encounterController.myFireflies.Count - 1]);
            encounterController.myFireflies.RemoveAt(encounterController.myFireflies.Count - 1);
            myHandText += encounterController.myHand[encounterController.myHand.Count - 1].ToString() + " ";
            myHand.GetComponent<Text>().text = myHandText;
            encounterController.nextButton.SetActive(true);
            encounterController.fireflyIcon.GetComponent<Button>().interactable = false;
            encounterController.NextTutorialPart();
        }
        else {
            //Jos ei ole tarpeeksi tulikärpäsiä
            if (encounterController.myFireflies.Count == 0) {
                Debug.Log("Not enough fireflies!");
            }
            //Pakan päällimäinen kortti lisätään omaan käteen, poistetaan yksi tulikärpänen varastosta, päivitetään teksti
            else
            {
                encounterController.myHand.Add(encounterController.deck[0]);
                myHandNumber += encounterController.deck[0];
                encounterController.deck.RemoveAt(0);
                encounterController.usedFireflies.Add(encounterController.myFireflies[encounterController.myFireflies.Count - 1]);
                encounterController.myFireflies.RemoveAt(encounterController.myFireflies.Count - 1);
                myHandText += encounterController.myHand[encounterController.myHand.Count - 1].ToString() + " ";
                myHand.GetComponent<Text>().text = myHandText;
            }
            //Jos oma käsi on yli 21 Häviää kierroksen
            if (myHandNumber > 21) {
                encounterController.RoundLost();
                Debug.Log("mennään täällä");
                encounterController.enemyScore += 1;
                //Jos vihollisen pisteet ovat kolme häviää pelin
                if (encounterController.enemyScore == 3) {
                    encounterController.GameLost();
                }
                //Uusi kierros
                else {
                    encounterController.NewRound();
                }
            }
        }
        //ball of light size update
        encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);
    }

}
