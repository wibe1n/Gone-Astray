using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EncounterController : MonoBehaviour {

    public GameObject player, camera, cameraPosTarget;
    UnityEvent m_MyEvent = new UnityEvent();
    public UnityEvent m_Proceed = new UnityEvent();
    public Character character;
    public CombatController combatController;
    public InGameCanvasController igcController;
    public PencilContourEffect pencilEffects;
    public Enemy myEnemy;
    public List<Firefly> myFireflies = new List<Firefly>();
    public List<Firefly> usedFireflies = new List<Firefly>();
    private List<GameObject> lights = new List<GameObject>();
    public Animator lightballAnimator, darkBallAnimator, lightIconAnimator, darkIconAnimator, useFireflyAnimator;
    public List<int> deck = new List<int>() { };
    public int enemyHand;
    public List<int> myHand;
    public int myScore, enemyScore, winTarget, minimFireflies, round;
    private int tutorialPart = 0;
    public GameObject gameCanvas, textPanel, runButton, approachButton, tutorialButton, fireflyIcon, darknessIcon, proceedButton, loseIcon, winIcon,
        nextButton, notEnoughIcon, outOfFliesIcon, scoreCanvas, roundText, scoreText, wonOrLostText, encounterEndText, encounterEndCanvas, fireflyCounter, usedFireflyCounter, useFirefly;
    public Text infoText;
    public GameObject runAwayScreen;
    public bool reached = false;
    public bool tutorial;
    public bool proceed = false;
    public Image fireFlyImage;
    public Image darknessImage;
    public GameObject Fireflyball;
    private FMOD.Studio.EventInstance battleMusic;

    private void Start() {
        battleMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gone_Astray_Battle_Music_demo_2");
        lightballAnimator.SetBool("Clicked", false);
        darkBallAnimator.SetBool("Clicked", false);
        lightIconAnimator.SetBool("Showdown", false);
        darkIconAnimator.SetBool("Showdown", false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && m_MyEvent != null) {
            m_MyEvent.Invoke();
        }
        //else if (Input.GetKeyDown(KeyCode.Space) && m_Proceed != null && !proceed) {
        //    m_Proceed.Invoke();
        //}
    }

    // haetaan vihollinen ja tulikärpäset saadulta objektilta, kyssäri lähestymisestä näkyviin
    public void StartEncounter(Enemy enemy, List<Firefly> fireflyList) {
        
        player.GetComponent<MovementControls>().encounter = true;
        player.GetComponent<MovementControls>().stop = true;
        camera.GetComponent<CameraController2>().combatLock = true;
        //in game canvas käyttökieltoon
        igcController.ToggleInGameCanvas(false);
        myEnemy = enemy;
        myFireflies = fireflyList;
        round = 1;
        UpdateFlyAmount(fireflyCounter, myFireflies.Count);
        gameCanvas.SetActive(true);
        for (int i = 0; i < enemy.lightPath.Count; i++) {
            enemy.lightPath[i].SetActive(true);
            lights.Add(enemy.lightPath[i]);
        }
        if (myEnemy.isTutorial) {
            tutorialButton.SetActive(true);
        }
        
        if (myFireflies.Count < minimFireflies)
        {
            //jos kärpäsiä ei ole tarpeeksi encounteriin, napit disabletaan ja kerrotaan monta puuttuu
            approachButton.GetComponent<Button>().interactable = false;
            tutorialButton.GetComponent<Button>().interactable = false;
            int missing = minimFireflies - myFireflies.Count;
            notEnoughIcon.GetComponent<Text>().text = "Not enough fireflies...\nYou need at least " + missing + " more.";
            notEnoughIcon.SetActive(true);
        }
        else
        {
            approachButton.GetComponent<Button>().interactable = true;
            tutorialButton.GetComponent<Button>().interactable = true;
            notEnoughIcon.SetActive(false);
        }
    }

    //Aloitetaan tutoriaaali versio, muuten sama kuin normaaliversio alempana, mutta tesktiavut näkyvissä ja tauotettu taistelu
    public void StartTutorial() {
        battleMusic.start();
        if (myFireflies.Count < 1) {
            RunAway();
        }
        else {
            tutorial = true;
            m_MyEvent.AddListener(NextTutorialPart);
            fireFlyImage.rectTransform.sizeDelta = new Vector2(combatController.myHandNumber * 100 / 21, combatController.myHandNumber * 100 / 21);
            darknessImage.rectTransform.sizeDelta = new Vector2(combatController.enemyHandNumber * 100 / 21, combatController.enemyHandNumber * 100 / 21);
            runButton.SetActive(false);
            tutorialButton.SetActive(false);
            approachButton.SetActive(false);
            fireflyIcon.SetActive(true);
            fireflyIcon.GetComponent<Button>().interactable = false;
            darknessIcon.SetActive(true);
            nextButton.SetActive(true);
            textPanel.SetActive(true);
            infoText.text = NameDescContainer.GetCombatTutorialPart("part0");
            GenerateBlackJackDeck();
            ShuffleDeck();
            enemyHand = 15;
        }
    }

    public void Proceed() {
        proceed = true;
        combatController.Proceed();
    }

    //Lisää tutoriaalilaskinta ja laittaa seuraavan puhekuplan sen mukaan. Vihollinen myös käyttäytyy tiettyjen kohtien aikana
    public void NextTutorialPart() {
        tutorialPart++;
        infoText.text = NameDescContainer.GetCombatTutorialPart("part" + tutorialPart.ToString());
        if(tutorialPart == 3) {
            fireflyIcon.GetComponent<Button>().interactable = true;
            nextButton.SetActive(false);
            m_MyEvent.RemoveListener(NextTutorialPart);
            combatController.PlayersTurn();
        }
        else if(tutorialPart == 4) {
            m_MyEvent.AddListener(NextTutorialPart);
        }
        else if(tutorialPart == 7) {
            nextButton.SetActive(false);
            m_MyEvent.RemoveListener(NextTutorialPart);
            fireflyIcon.GetComponent<Button>().interactable = true;
        }
        else if (tutorialPart == 8) {
            nextButton.SetActive(false);
            m_Proceed.AddListener(Proceed);
            proceedButton.SetActive(true);
        }
        else if (tutorialPart == 9) {
            nextButton.SetActive(true);
            m_MyEvent.AddListener(NextTutorialPart);
            proceedButton.SetActive(false);
            m_Proceed.RemoveListener(Proceed);
            reached = true;
        }
        else if (tutorialPart == 11) {
            useFirefly.SetActive(true);
            useFirefly.GetComponent<Button>().interactable = false;
        }
        else if (tutorialPart == 13) {
            nextButton.SetActive(false);
            m_MyEvent.RemoveListener(NextTutorialPart);
            useFirefly.GetComponent<Button>().interactable = true;
        }
        else if (tutorialPart == 14) {
            nextButton.SetActive(false);

            useFirefly.GetComponent<Button>().interactable = false;
            fireflyIcon.GetComponent<Button>().interactable = true;
        }
        else if (tutorialPart == 15) {
            nextButton.SetActive(false);

            fireflyIcon.GetComponent<Button>().interactable = true;
            
        }
        else if (tutorialPart == 16) {
            nextButton.SetActive(false);
            m_Proceed.AddListener(Proceed);
            proceedButton.SetActive(true);
        }
        else if (tutorialPart == 17) {
            nextButton.SetActive(true);
            m_MyEvent.AddListener(NextTutorialPart);
            proceedButton.SetActive(false);
            m_Proceed.RemoveListener(Proceed);
            reached = true;
        }
        else if (tutorialPart == 20) {
            m_MyEvent.RemoveListener(NextTutorialPart);
            reached = true;
        }
    }

    //Pelin aloitus
    public void StartBlackJack() {
        battleMusic.start();

        //Jos tulikärpäsiä ei ole tarpeeksi niin pelaaja juoksee karkuun
        if(myFireflies.Count < minimFireflies) {
            Debug.Log("ei uskalla");
            RunAway();
        }

        //Muuten, nykyiset elementit canvaksella pois päältä ja scorecanvas auki
        else {
            tutorial = false;
            textPanel.SetActive(false);
            runButton.SetActive(false);
            tutorialButton.SetActive(false);
            approachButton.SetActive(false);

            UpdateFlyAmount(usedFireflyCounter, 0);
            usedFireflyCounter.SetActive(true);

            ShowScore(-1);
        }

    }


    public void RunAway()
    {
        StartCoroutine(RunAwayRoutine());
    }

    IEnumerator RunAwayRoutine() {
        runAwayScreen.SetActive(true);
        battleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        player.gameObject.GetComponent<MovementControls>().destination = null;
        player.gameObject.GetComponent<MovementControls>().destination2 = Vector3.zero;
        player.GetComponent<MovementControls>().encounter = false;
        player.GetComponent<MovementControls>().running = false;
        camera.GetComponent<CameraController2>().combatLock = false;
        runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
        player.transform.position = myEnemy.checkpoint.transform.position;
        camera.transform.position = myEnemy.checkpoint.transform.position;
        yield return new WaitForSeconds(1f);
        player.GetComponent<MovementControls>().stop = false;
        gameCanvas.SetActive(false);
        runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(3f);
        runAwayScreen.SetActive(false);
    }

    public void OutOfFlies()
    {
        StartCoroutine(OutOfFliesRoutine());
    }

    //Kun kärpäset ei riitä kesken matsin niin väläytetään teksti
    IEnumerator OutOfFliesRoutine()
    {
        outOfFliesIcon.SetActive(true);
        outOfFliesIcon.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 0.0f, false);
        outOfFliesIcon.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2);
        outOfFliesIcon.SetActive(false);
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

    public void GetCloser() {
        StartCoroutine(GetCloserRoutine());
    }

    IEnumerator GetCloserRoutine()
    {
        player.GetComponent<MovementControls>().stop = false;
        yield return new WaitForSeconds(2);
        fireflyIcon.GetComponent<Button>().interactable = true;
        if (player.GetComponent<MovementControls>().encounter == true) {
            player.GetComponent<MovementControls>().stop = true;
        }
        yield return null;
    }

    public void GetAway() {
        StartCoroutine(GetAwayRoutine());
    }

    IEnumerator GetAwayRoutine()
    {
        player.GetComponent<MovementControls>().stop = false;
        player.GetComponent<MovementControls>().running = true;
        yield return new WaitUntil(() => Vector3.Distance(player.transform.position, player.GetComponent<MovementControls>().destination2) < 0.2f);
        player.GetComponent<MovementControls>().running = false;
        yield return new WaitForSeconds(1f);
        player.GetComponent<MovementControls>().stop = true;
        fireflyIcon.GetComponent<Button>().interactable = true;
        yield return null;
    }

    public void UseFirefly() {
        StartCoroutine(UseFireflyRoutine());
    }

    IEnumerator UseFireflyRoutine(){
        useFireflyAnimator.SetBool("Clicked", true);
        yield return new WaitForSeconds(1f);
        useFireflyAnimator.SetBool("Clicked", false);
    }

    public void RollLightBall() {
        StartCoroutine(RollLightBallRoutine());
    }

    IEnumerator RollLightBallRoutine() {
        lightballAnimator.SetBool("Clicked", true);
        yield return new WaitForSeconds(0.5f);
        lightballAnimator.SetBool("Clicked", false);
    }

    public void RollDarkBall() {
        StartCoroutine(RollDarkBallRoutine());
    }

    IEnumerator RollDarkBallRoutine() {
        darkBallAnimator.SetBool("Clicked", true);
        yield return new WaitForSeconds(0.5f);
        darkBallAnimator.SetBool("Clicked", false);
    }

    public void ShowDownL() {
        StartCoroutine(ShowdownLRoutine());
    }

    IEnumerator ShowdownLRoutine() {
        lightIconAnimator.SetBool("Showdown", true);
        yield return new WaitForSeconds(2);
        lightIconAnimator.SetBool("Showdown", false);
    }

    public void ShowDownD() {
        StartCoroutine(ShowDownDRoutine());
    }

    IEnumerator ShowDownDRoutine() {
        darkIconAnimator.SetBool("Showdown", true);
        yield return new WaitForSeconds(2);
        darkIconAnimator.SetBool("Showdown", false);
    }

    public void RoundLost() {
        round++;
        StartCoroutine(RoundLostRoutine());
    }
    //häviö ikonin väläytys
    IEnumerator RoundLostRoutine() {
        loseIcon.SetActive(true);
        loseIcon.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 0.0f, false);
        loseIcon.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2);
        loseIcon.SetActive(false);
    }

    public void RoundWon() {
        round++;
        StartCoroutine(RoundWonRoutine());
    }
    //Voitto ikonin väläytys
    IEnumerator RoundWonRoutine() {
        winIcon.SetActive(true);
        winIcon.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 0.0f, false);
        winIcon.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2);
        winIcon.SetActive(false);
    }

    public void MakeLightsSpoopier(float duration, float currentDarkness, float endDarkness) {
        StartCoroutine(MakeLightsSpoopierRoutine(duration, currentDarkness, endDarkness));
    }

    IEnumerator MakeLightsSpoopierRoutine(float duration, float currentDarkness, float endDarkness) {
        float timeRemaining = duration;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            pencilEffects.m_EdgesOnly = Mathf.Lerp(currentDarkness, endDarkness, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;
        }
        pencilEffects.m_EdgesOnly = endDarkness;
    }

    public void ShowScore(int result)
    {
        
        if (round != 1)
        {
            fireflyIcon.SetActive(false);
            darknessIcon.SetActive(false);
            useFirefly.SetActive(false);
            Fireflyball.SetActive(false);
            proceedButton.SetActive(false);
        }

        //Päivitetään scoreruutuun kumpi voitti edellisen kierroksen
        switch (result)
        {
            case 0:
                wonOrLostText.GetComponent<Text>().text = "Enemy won...";
                break;
            case 1:
                wonOrLostText.GetComponent<Text>().text = "You won!";
                break;
            case -1:
                wonOrLostText.GetComponent<Text>().text = "";
                break;
            default:
                Debug.Log("Round result error");
                break;
        }
        //päivitetään oikea kierrosluku ja pisteet tekstiruutuihin
        roundText.GetComponent<Text>().text = "Round " + round;
        scoreText.GetComponent<Text>().text = "Score\nYou: " + myScore + "\nEnemy: " + enemyScore;

        scoreCanvas.SetActive(true);
    }

    public void WhoWon(int result)
    {
        switch (result)
        {
            case 0:
                encounterEndText.GetComponent<Text>().text = "Encounter failed";
                break;
            case 1:
                encounterEndText.GetComponent<Text>().text = "Encounter won!!";
                break;
            default:
                Debug.Log("Encounter result error");
                break;
        }
        StartCoroutine(WhoWonRoutine());
    }
    //väläytetään koko encounterin lopputulos
    IEnumerator WhoWonRoutine()
    {
        encounterEndCanvas.SetActive(true);
        encounterEndText.SetActive(true);
        encounterEndText.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 0.0f, false);
        encounterEndText.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2);
        encounterEndText.SetActive(false);
        encounterEndCanvas.SetActive(false);
    }

    public void FirstRound()
    {
        //Laitetaan oikeat napit esille, luodaan ja sekoitetaan pakka otetaan saatavilla olevista tulikärpäsistä pois kaksi
        //Aloitetaan pelaajan vuoro
        fireflyIcon.SetActive(true);
        darknessIcon.SetActive(true);
        useFirefly.SetActive(true);
        Fireflyball.SetActive(true);
        proceedButton.SetActive(true);
        GenerateBlackJackDeck();
        ShuffleDeck();
        enemyHand = myEnemy.disturbLimit;
        combatController.PlayersTurn();
        UpdateFlyAmount(fireflyCounter, myFireflies.Count);
        UpdateFlyAmount(usedFireflyCounter, usedFireflies.Count);
    }

    //Putsataan kädet tyhjiksi ja sekoitetaan pakka
    public void NewRound() {
        scoreCanvas.SetActive(false);
 
        if (round == 1) {
            FirstRound();
            m_Proceed.AddListener(Proceed);
        }
        else { 
            foreach (int item in myHand) {
                deck.Add(item);
            }
            myHand.Clear();
            ShuffleDeck();
            //Jos ei ole tulikärpäsiä uuteen kierrokseen, lähdetään karkuun
            /*if (myFireflies.Count < 3)
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
            //Muuten uudet kortit ja taas pelaajan vuoro
            else {*/
            fireflyIcon.SetActive(true);
            darknessIcon.SetActive(true);
            if (!tutorial){
                useFirefly.SetActive(true);
                proceedButton.SetActive(true);
                Fireflyball.SetActive(true);
            }
            UpdateFlyAmount(fireflyCounter, myFireflies.Count);
            UpdateFlyAmount(usedFireflyCounter, usedFireflies.Count);
            m_Proceed.AddListener(Proceed);
            proceed = false;
            combatController.PlayersTurn();
            /*}*/
        }
    }

    //Lopetetaan taistelumusiikki, tyhjennetään kädet ja pakka, laitetaan encounternapit esille ja taistelunapit piiloon
    //Lopuksi juostaan karkuun
    public void GameLost() {
        battleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        enemyHand = 0;
        myHand.Clear();
        deck.Clear();
        enemyScore = 0;
        myScore = 0;
        textPanel.SetActive(true);
        runButton.SetActive(true);
        approachButton.SetActive(true);
        fireflyIcon.SetActive(false);
        useFirefly.SetActive(false);
        darknessIcon.SetActive(false);
        proceedButton.SetActive(false);
        textPanel.SetActive(false);
        igcController.ToggleInGameCanvas(true);
        player.gameObject.GetComponent<MovementControls>().destination = null;
        player.gameObject.GetComponent<MovementControls>().destination2 = Vector3.zero;
        RunAway();
        WhoWon(0);
        StartCoroutine(CameraRoutine());
        //TODO affect world???       
    }

    public void GameWon() {
        //Vihollisen silmät kiinni, muuten sama kuin ylhäällä, paitsi että pelaaja ei lähde karkuun ja vapautetaan liikkuminen
        if (myEnemy.hasEyes) {
            myEnemy.eye1.SetActive(false);
            myEnemy.eye2.SetActive(false);
        }
        player.GetComponent<MovementControls>().encounter = false;
        battleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        enemyHand = 0;
        myHand.Clear();
        deck.Clear();
        Destroy(myEnemy);
        for(int i = 0; i < usedFireflies.Count; i++) {
            player.GetComponent<Character>().myFireflies.Add(usedFireflies[i]);
        }
        textPanel.SetActive(true);
        runButton.SetActive(true);
        approachButton.SetActive(true);
        fireflyIcon.SetActive(false);
        darknessIcon.SetActive(false);
        useFirefly.SetActive(false);
        proceedButton.SetActive(false);
        usedFireflyCounter.SetActive(false);
        gameCanvas.SetActive(false);
        textPanel.SetActive(false);
        igcController.ToggleInGameCanvas(true);
        player.gameObject.GetComponent<MovementControls>().destination = null;
        player.gameObject.GetComponent<MovementControls>().destination2 = Vector3.zero;
        player.GetComponent<MovementControls>().stop = false;
        camera.GetComponent<CameraController2>().combatLock = false;
        player.GetComponent<MovementControls>().encounter = false;
        WhoWon(1);
        StartCoroutine(CameraRoutine());
        //TODO animation for monster transforming to something regiular???
    }

    IEnumerator CameraRoutine() {
        float i = 0;
        while (i < 1) {
            i += 0.01f;
            Vector3 endPosition = new Vector3(myEnemy.cameraPos.transform.position.x, myEnemy.cameraPos.transform.position.y + 0.01f, myEnemy.cameraPos.transform.position.z);
            myEnemy.cameraPos.transform.position = Vector3.Lerp(myEnemy.cameraPos.transform.position, endPosition, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void UpdateFlyAmount(GameObject counter, int amount)
    {
        counter.GetComponentInChildren<Text>().text = amount.ToString();
    }
}
