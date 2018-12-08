using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour {

    public GameObject player, camera;
    public Character character;
    public CombatController combatController;
    public Enemy myEnemy;
    public List<Firefly> myFireflies = new List<Firefly>();
    public List<Firefly> usedFireflies = new List<Firefly>();
    public List<int> deck = new List<int>() { };
    public List<int> enemyHand;
    public List<int> myHand;
    public int myScore, enemyScore, winTarget;
    private int tutorialPart = 0;
    public GameObject gameCanvas, textPanel, runButton, approachButton, tutorialButton, fireflyIcon, darknessIcon, proceedButton, loseIcon, winIcon, nextButton;
    public Text infoText;
    public GameObject runAwayScreen;
    public bool reached = false;
    public bool tutorial;
    public Image fireFlyImage;
    public Image darknessImage;
    private FMOD.Studio.EventInstance battleMusic;

    private void Start() {
        battleMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gone_Astray_Battle_Music_demo_2");
    }


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
        if (myEnemy.isTutorial) {
            tutorialButton.SetActive(true);
        }
    }

    public void StartTutorial() {
        battleMusic.start();
        if (myFireflies.Count < 3) {
            RunAway();
        }
        else {
            tutorial = true;
            fireFlyImage.rectTransform.sizeDelta = new Vector2(combatController.myHandNumber * 100 / 21, combatController.myHandNumber * 100 / 21);
            darknessImage.rectTransform.sizeDelta = new Vector2(combatController.enemyHandNumber * 100 / 21, combatController.enemyHandNumber * 100 / 21);
            runButton.SetActive(false);
            tutorialButton.SetActive(false);
            approachButton.SetActive(false);
            fireflyIcon.SetActive(true);
            darknessIcon.SetActive(true);
            nextButton.SetActive(true);
            infoText.text = NameDescContainer.GetCombatTutorialPart("part0");
            GenerateBlackJackDeck();
            ShuffleDeck();
            enemyHand.Add(5);
            myHand.Add(4);
            enemyHand.Add(9);
            myHand.Add(7);
            usedFireflies.Add(myFireflies[myFireflies.Count - 1]);
            myFireflies.RemoveAt(myFireflies.Count - 1);
            usedFireflies.Add(myFireflies[myFireflies.Count - 1]);
            myFireflies.RemoveAt(myFireflies.Count - 1);
        }
    }

    public void NextTutorialPart() {
        tutorialPart++;
        infoText.text = NameDescContainer.GetCombatTutorialPart("part" + tutorialPart.ToString());
        if(tutorialPart == 3) {
            nextButton.SetActive(false);
            combatController.PlayersTurn();
        }
        else if (tutorialPart == 8) {
            nextButton.SetActive(false);
            proceedButton.SetActive(true);
        }
        else if (tutorialPart == 10) {
            reached = true;
        }
        else  if (tutorialPart == 12) {
            reached = true;
        }
        else if (tutorialPart == 14) {
            reached = true;
        }
    }

    public void StartBlackJack() {
        battleMusic.start();
        if(myFireflies.Count < 3) {
            Debug.Log("ei uskalla");
            RunAway();
        }
        else {
            tutorial = false;
            textPanel.SetActive(false);
            runButton.SetActive(false);
            tutorialButton.SetActive(false);
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
        camera.transform.position = myEnemy.checkpoint.transform.position;
        //TODO: fancy effects for running away
        yield return new WaitForSeconds(1f);
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
        }
        
    }

    public void GameLost() {
        battleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
        myEnemy.eye1.SetActive(false);
        myEnemy.eye2.SetActive(false);
        battleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        enemyHand.Clear();
        myHand.Clear();
        deck.Clear();
        Destroy(myEnemy);
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
        player.GetComponent<MovementControls>().stop = false;
        //TODO animation for monster transforming to something regiular???
    }
}
