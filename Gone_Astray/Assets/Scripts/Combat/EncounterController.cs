using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour {

    public GameObject player;
    public Character character;
    public CombatController combatController;
    public Enemy myEnemy;
    public List<Firefly> myFireflies = new List<Firefly>();
    public List<Firefly> usedFireflies = new List<Firefly>();
    public List<int> deck = new List<int>() { };
    public List<int> enemyHand;
    public List<int> myHand;
    public int myScore, enemyScore, winTarget;
    public GameObject gameCanvas, textPanel, runButton, approachButton, fireflyIcon, darknessIcon, proceedButton, loseIcon, winIcon;
    public GameObject runAwayScreen;
    public bool reached = false;
    public Image fireFlyImage;
    public Image darknessImage;


    public void StartEncounter(Enemy enemy, List<Firefly> fireflyList) {

        //TODO: Turn camera to make player feel small

        //starting Fireflies for testing purposes
        for (int i = 0; i < 12; i++) {
            Firefly newfirefly = new Firefly(0);
            character.myFireflies.Add(newfirefly);
        }
        myEnemy = enemy;
        myFireflies = fireflyList;
        gameCanvas.SetActive(true);
    }

    public void StartBlackJack() {
        Debug.Log(myFireflies.Count);
        if(myFireflies.Count < 3) {
            Debug.Log("ei uskalla");
            RunAway();
        }
        else {
            textPanel.SetActive(false);
            runButton.SetActive(false);
            approachButton.SetActive(false);
            fireflyIcon.SetActive(true);
            darknessIcon.SetActive(true);
            proceedButton.SetActive(true);
            GenerateBlackJackDeck();
            ShuffleDeck();
            enemyHand.Add(deck[0]);
            deck.RemoveAt(0);
            myHand.Add(deck[0]);
            deck.RemoveAt(0);
            enemyHand.Add(deck[0]);
            deck.RemoveAt(0);
            myHand.Add(deck[0]);
            deck.RemoveAt(0);
            usedFireflies.Add(myFireflies[myFireflies.Count - 1]);
            myFireflies.RemoveAt(myFireflies.Count - 1);
            usedFireflies.Add(myFireflies[myFireflies.Count - 1]);
            myFireflies.RemoveAt(myFireflies.Count - 1);
            combatController.PlayersTurn();
        }

    }

    public void RunAway() {
        StartCoroutine(RunAwayRoutine());
    }

    IEnumerator RunAwayRoutine() {
        runAwayScreen.SetActive(true);
        runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
        player.transform.position = myEnemy.checkpoint.transform.position;
        //TODO: fancy effects for running away
        yield return new WaitForSeconds(1f);
        character.inCombat = false;
        gameCanvas.SetActive(false);
        runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(3f);
        runAwayScreen.SetActive(false);
    }

    void GenerateBlackJackDeck() {
        for (int i = 0; i < 4; i++) {
            deck.Add(2);
            deck.Add(3);
            deck.Add(4);
            deck.Add(5);
            deck.Add(6);
            deck.Add(7);
            deck.Add(8);
            deck.Add(9);
            deck.Add(11);
        }
        for(int i = 0; i < 16; i++) {
            deck.Add(10);
        }
    }

    void ShuffleDeck() {
        for (int i = deck.Count - 1; i > 0; --i) {
            int k = Random.Range(0, i);
            int temp = deck[i];
            deck[i] = deck[k];
            deck[k] = temp;
        }
    }

    public void RoundLost() {
        StartCoroutine(RoundLostRoutine());
    }

    IEnumerator RoundLostRoutine() {
        loseIcon.SetActive(true);
        loseIcon.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 0.0f, false);
        loseIcon.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2);
        loseIcon.SetActive(false);
    }

    public void RoundWon() {
        StartCoroutine(RoundWonRoutine());
    }

    IEnumerator RoundWonRoutine() {
        winIcon.SetActive(true);
        winIcon.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 0.0f, false);
        winIcon.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2);
        winIcon.SetActive(false);
    }


    public void NewRound() {
        foreach (int item in enemyHand) {
            deck.Add(item);
        }
        enemyHand.Clear();
        foreach (int item in myHand) {
            deck.Add(item);
        }
        myHand.Clear();
        ShuffleDeck();
        combatController.myHandText = "";
        combatController.enemyHandText = "";
        combatController.myHand.GetComponent<Text>().text = combatController.myHandText;
        combatController.enemyHand.GetComponent<Text>().text = combatController.enemyHandText;
        if (myFireflies.Count < 3)
        {
            deck.Clear();
            textPanel.SetActive(true);
            runButton.SetActive(true);
            approachButton.SetActive(true);
            fireflyIcon.SetActive(false);
            darknessIcon.SetActive(false);
            proceedButton.SetActive(false);
            RunAway();
            
        }
        else {
            enemyHand.Add(deck[0]);
            deck.RemoveAt(0);
            myHand.Add(deck[0]);
            deck.RemoveAt(0);
            enemyHand.Add(deck[0]);
            deck.RemoveAt(0);
            myHand.Add(deck[0]);
            deck.RemoveAt(0);
            usedFireflies.Add(myFireflies[myFireflies.Count - 1]);
            myFireflies.RemoveAt(myFireflies.Count - 1);
            usedFireflies.Add(myFireflies[myFireflies.Count - 1]);
            myFireflies.RemoveAt(myFireflies.Count - 1);
            combatController.PlayersTurn();
            
            fireFlyImage.rectTransform.sizeDelta = new Vector2(combatController.myHandNumber * 100 / 21, combatController.myHandNumber * 100 / 21);

            //MIKSI TÄMÄ EI TOIMI??????????????????????????????????????????????????????????????????+++++
            //darknessImage.rectTransform.sizeDelta = new Vector2(combatController.enemyHandNumber * 100 / 21, combatController.enemyHandNumber * 100 / 21);
        }
        
    }

    public void GameLost() {
        Debug.Log("hävisin pelin");
        enemyHand.Clear();
        myHand.Clear();
        deck.Clear();
        enemyScore = 0;
        myScore = 0;
        combatController.myHandText = "";
        combatController.enemyHandText = "";
        combatController.myHand.GetComponent<Text>().text = combatController.myHandText;
        combatController.enemyHand.GetComponent<Text>().text = combatController.enemyHandText;
        textPanel.SetActive(true);
        runButton.SetActive(true);
        approachButton.SetActive(true);
        fireflyIcon.SetActive(false);
        darknessIcon.SetActive(false);
        proceedButton.SetActive(false);
        RunAway();
        //TODO affect world???       
    }

    public void GameWon() {
        Debug.Log("voitin pelin");
        enemyHand.Clear();
        myHand.Clear();
        deck.Clear();
        Destroy(myEnemy);
        character.myEnemy = null;
        character.enemyIsNear = false;
        character.inCombat = false;
        combatController.myHandText = "";
        combatController.enemyHandText = "";
        combatController.myHand.GetComponent<Text>().text = combatController.myHandText;
        combatController.enemyHand.GetComponent<Text>().text = combatController.enemyHandText;
        textPanel.SetActive(true);
        runButton.SetActive(true);
        approachButton.SetActive(true);
        fireflyIcon.SetActive(false);
        darknessIcon.SetActive(false);
        proceedButton.SetActive(false);
        gameCanvas.SetActive(false);
        //TODO animation for monster transforming to something regiular???
    }
}
