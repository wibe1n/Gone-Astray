using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour {

    public EncounterController encounterController;
    public GameObject enemyHand, myHand;
    public int myHandNumber, enemyHandNumber, enemyTreshold;

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
        }

        //TODO: animation for adding the hand
        yield return new WaitForSeconds(0.5f);

        //Vihollisen käsi esille
        
        enemyHandNumber = encounterController.enemyHand;
        enemyTreshold = encounterController.myEnemy.disturbTreshold;
        
        //TODO: animation for adding the hand

        //ball of light size update
        encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);
        //ball of darkness size update
        encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
        yield return new WaitForSeconds(0.5f);
    }


    public void Proceed() {
        encounterController.m_Proceed.RemoveListener(Proceed);
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn() {
        //Tutorial version
        // Vihollisen vuoro muutetaan pian, en kommentoi
        if(encounterController.tutorial == true) {
            //Katsotaan kummalla on parempi käsi
            encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
            yield return new WaitForSeconds(1f);
            //Jos pelaaja ei pääse tresholdin sisään häviää kierroksen
            encounterController.NextTutorialPart();
            yield return new WaitUntil(() => encounterController.reached == true);
            encounterController.reached = false;
            if (myHandNumber < enemyHandNumber - enemyTreshold || myHandNumber > enemyHandNumber + enemyTreshold) {
                encounterController.RoundLost();
                encounterController.enemyScore += 1;
                if (encounterController.enemyScore == 3) {
                    encounterController.GameLost();
                }
                //Muuten uusi kierros
                else {
                    encounterController.ShowScore(0);
                }
            }
            //Jos pelaaja pääsee Tresholdin sisään hän voittaa pelin
            else {
                Debug.Log("voitto");
                enemyHandNumber = 0;
                myHandNumber = 0;
                encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);
                encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
                enemyHand.GetComponent<Text>().text = "";
                myHand.GetComponent<Text>().text = "";
                yield return new WaitUntil(() => encounterController.reached == true);
                encounterController.GameWon();
            }
        }
        else {
            //Katsotaan kummalla on parempi käsi
            encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
            yield return new WaitForSeconds(1f);
            //Jos pelaaja ei pääse tresholdin sisään häviää kierroksen
            if (myHandNumber < enemyHandNumber - enemyTreshold || myHandNumber > enemyHandNumber + enemyTreshold) {
                encounterController.RoundLost();
                encounterController.enemyScore += 1;
                if (encounterController.enemyScore == 3) {
                    encounterController.GameLost();
                }
                //Muuten uusi kierros
                else {
                    encounterController.ShowScore(0);
                }
            }
            //Jos pelaaja pääsee Tresholdin sisään hän voittaa pelin
            else {
                Debug.Log("voitto");
                enemyHandNumber = 0;
                myHandNumber = 0;
                encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);
                encounterController.darknessImage.rectTransform.sizeDelta = new Vector2(enemyHandNumber * 100 / 21, enemyHandNumber * 100 / 21);
                enemyHand.GetComponent<Text>().text = "";
                myHand.GetComponent<Text>().text = "";
                encounterController.GameWon();
            }
 
        }

    }

    public void AddLight() {
        //Tutorial versio
        if (encounterController.tutorial == true) {
            encounterController.myHand.Add(9);
            myHandNumber += 9;
            encounterController.nextButton.SetActive(true);
            encounterController.fireflyIcon.GetComponent<Button>().interactable = false;
            encounterController.NextTutorialPart();
        }
        else {
            //Jos ei ole tarpeeksi tulikärpäsiä
            if (encounterController.myFireflies.Count == 0) {
                Debug.Log("Not enough fireflies!");
                encounterController.OutOfFlies();
            }
            //Pakan päällimäinen kortti lisätään omaan käteen, poistetaan yksi tulikärpänen varastosta, päivitetään teksti
            else
            {
                encounterController.myHand.Add(encounterController.deck[0]);
                myHandNumber += encounterController.deck[0];
                encounterController.deck.RemoveAt(0);
            }
            //Jos oma käsi on yli hirviön häiritsemisrajan, häviää kierroksen
            if (myHandNumber > enemyHandNumber + enemyTreshold) {
                encounterController.RoundLost();
                Debug.Log("mennään täällä");
                encounterController.enemyScore += 1;
                //Jos vihollisen pisteet ovat kolme häviää pelin
                if (encounterController.enemyScore == 3) {
                    encounterController.GameLost();
                }
                //Uusi kierros
                else {
                    encounterController.ShowScore(0);
                }
            }
        }
        //ball of light size update
        encounterController.fireFlyImage.rectTransform.sizeDelta = new Vector2(myHandNumber * 100 / 21, myHandNumber * 100 / 21);
    }

    public void UseFirefly() {
        if (encounterController.tutorial) {
            encounterController.NextTutorialPart();
        }
        if (encounterController.myFireflies.Count > 0) {
            encounterController.usedFireflies.Add(encounterController.myFireflies[encounterController.myFireflies.Count - 1]);
            encounterController.UpdateFlyAmount(encounterController.usedFireflyCounter, encounterController.usedFireflies.Count);
            encounterController.myFireflies.RemoveAt(encounterController.myFireflies.Count - 1);
            encounterController.UpdateFlyAmount(encounterController.fireflyCounter, encounterController.myFireflies.Count);

            enemyTreshold += 1;
        }
        Debug.Log(enemyHandNumber + enemyTreshold);
    }

}
