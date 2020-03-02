using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour {

    UnityEvent m_MyEvent = new UnityEvent();
    UnityEvent m_SecondEvent = new UnityEvent();
    public SpeechBubbleCreator speechCreator;
    public int id;
    public GameObject Canvas; // drag from hierarchy
    public int currentSpeechInstance, maxSpeechInstance;
    public bool talking = false;
    public bool walkedAway = false;
	public KeyCode talkKey = KeyCode.None;
    public KeyCode talkBackKey = KeyCode.None;
    private Character player;
    public TutoLvl TutorialCutsceneScript;

    private void Start() {
        if(id == 6) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
        if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			Undying_Object undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.talkKey == KeyCode.None)
				talkKey = KeyCode.E;
			else
				talkKey = undyObj.talkKey;
		}else
			talkKey = KeyCode.E;
        talkBackKey = KeyCode.P;
    }

    void OnTriggerEnter(Collider player) {
        //Lisätään kuuntelija ja avataan kyssäri-ikoni. 
        walkedAway = false;
        if (player.gameObject.GetComponent<Character>() != null && !speechCreator.StillTalking())
        {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(TalkEvent);
            m_SecondEvent.AddListener(Backwards);
            //Tietyt NPC voivat alkaa älisee heti
            if (id == 6)
            {
                switch (currentSpeechInstance)
                {
                    /*case 1:
                        player.gameObject.GetComponent<MovementControls>().stop = true;
                        m_MyEvent.Invoke();
                        break;*/
                    case 13:
                        if (player.GetComponent<Character>().hasRaskovnik)
                        {
                            currentSpeechInstance = 20;
                            maxSpeechInstance = 20;
                        }
                        //TutorialCutsceneScript.PlayNextCutscene(2);
                        m_MyEvent.Invoke();
                        break;
                    case 18:
                        TutorialCutsceneScript.PlayNextCutscene(3);
                        m_MyEvent.Invoke();
                        break;
                    case 20:
                        m_MyEvent.Invoke();
                        break;
                    case 21:
                        TutorialCutsceneScript.PlayNextCutscene(4);
                        m_MyEvent.Invoke();
                        break;
                    case 25:
                        m_MyEvent.Invoke();
                        break;
                    case 27:
                        TutorialCutsceneScript.PlayNextCutscene(5);
                        m_MyEvent.Invoke();
                        break;
                        /*case 28:
                            player.gameObject.GetComponent<MovementControls>().stop = true;
                            m_MyEvent.Invoke();
                            break;
                        case 37:
                            player.gameObject.GetComponent<MovementControls>().stop = true;
                            m_MyEvent.Invoke();
                            break;*/
                }

            }
        }

    }
    void OnTriggerExit(Collider player) {
        walkedAway = true;
        if (player.gameObject.GetComponent<Character>() != null) {
            //Jos lähtee pois kesken keskustelun niin keskusteluketju resetoituu ensimmäiseen
            if (!speechCreator.StillTalking() || currentSpeechInstance == maxSpeechInstance)
            {
                Canvas.SetActive(false);
                talking = false;
                //Jotkut NPC ei vät välttämättä resetoidu?
                if (id != 6) {
                    currentSpeechInstance = 1;
                } 
                speechCreator.CloseSpeechBubble(this);
                m_MyEvent.RemoveListener(TalkEvent);
            }
            //Jos pelaaja kävelee liian kauas puhujasta puhumisen aikana, niin npc ruikuttaa
            else if (!(currentSpeechInstance == maxSpeechInstance))
            {
                //Fian puhumisista ei tule aina valitusta
                /*if(id == 6 && maxSpeechInstance == 13) {
                    Canvas.SetActive(false);
                    talking = false;
                    speechCreator.CloseSpeechBubble(this);
                    m_MyEvent.RemoveListener(TalkEvent);
                    Destroy(this);
                }
                else if (id == 6 && maxSpeechInstance == 15) {
                    Canvas.SetActive(false);
                    talking = false;
                    speechCreator.CloseSpeechBubble(this);
                    m_MyEvent.RemoveListener(TalkEvent);
                    Destroy(this);
                }
                else if (id == 6 && maxSpeechInstance == 22)
                {
                    Canvas.SetActive(false);
                    talking = false;
                    speechCreator.CloseSpeechBubble(this);
                    m_MyEvent.RemoveListener(TalkEvent);
                    Destroy(this);
                }
                else {
                    speechCreator.WentTooFar(this);
                    StartCoroutine(CloseWhineBox(20));
                }*/
            }
        }
    }

    private void Update() {
        if (talking)
        {
            if (Input.GetKeyDown(talkKey) && m_MyEvent != null)
            {
                if (id == 6)
                {
                    m_MyEvent.Invoke();
                }
            }
            else if (Input.GetKeyDown(talkBackKey) && m_MyEvent != null)
            {
                m_SecondEvent.Invoke();
            }
        }
    }

    public void TalkEvent()
    {
        //tallennukselle välitetään pelaajan kautta missä kohtaa keskustelua mennään
        player.NPCspeechInstance = currentSpeechInstance;
        if (talking == true)
        {
            Debug.Log("MAX === " + maxSpeechInstance);
            Debug.Log("CURRENT === " + currentSpeechInstance);
            //Jos ollaan vikassa puhekerrassa niin suljetaan puhekupla
            if (currentSpeechInstance == maxSpeechInstance) {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                if (!walkedAway)
                    Canvas.SetActive(true);
                //Jotkut npc:t voivat lähteä kävelemään tms.
                if(id == 6) {
                    switch(maxSpeechInstance)
                    {
                        case 2:
                            speechCreator.CloseSpeechBubble(this);
                            Canvas.SetActive(false);
                            currentSpeechInstance = 3;
                            maxSpeechInstance = 12;
                            player.transform.position = TutorialCutsceneScript.MilaLocation2.transform.position;
                            player.transform.rotation = TutorialCutsceneScript.MilaLocation2.transform.rotation;
                            TutorialCutsceneScript.director2.Play();
                            TutorialCutsceneScript.once2 = false;
                            TutorialCutsceneScript.cutsceneId = 2;

                            m_MyEvent.RemoveListener(TalkEvent);
                            break;
                        case 12:
                            Canvas.SetActive(false);
                            speechCreator.CloseSpeechBubble(this);
                            //player.AddFirefly();
                            //Destroy(gameObject);
                            currentSpeechInstance = 13;
                            maxSpeechInstance = 17;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            TutorialCutsceneScript.EndCutscene();
                            break;
                        /*case 17:
                            Canvas.SetActive(false);
                            currentSpeechInstance = 18;
                            maxSpeechInstance = 19;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 19:
                            Canvas.SetActive(false);
                            currentSpeechInstance = 20;
                            maxSpeechInstance = 20;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 20:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 21;
                            maxSpeechInstance = 24;
                            break;
                        case 24:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 25;
                            maxSpeechInstance = 26;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 26:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 27;
                            maxSpeechInstance = 30;
                            break;
                        case 30:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            //currentSpeechInstance = 31;
                            //maxSpeechInstance = 32;
                            currentSpeechInstance = 33;
                            maxSpeechInstance = 35;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 36:
                            currentSpeechInstance = 37;
                            maxSpeechInstance = 37;
                            TutorialCutsceneScript.StopCoroutine(TutorialCutsceneScript.PlayingListener(0));
                            TutorialCutsceneScript.PlayNextCutscene(7);
                            break;
                        case 37:
                            currentSpeechInstance = 38;
                            maxSpeechInstance = 38;
                            TutorialCutsceneScript.StopCoroutine(TutorialCutsceneScript.PlayingListener(0));
                            TutorialCutsceneScript.PlayNextCutscene(8);
                            break;
                        case 38:
                            currentSpeechInstance = 39;
                            maxSpeechInstance = 39;
                            TutorialCutsceneScript.StopCoroutine(TutorialCutsceneScript.PlayingListener(0));
                            TutorialCutsceneScript.PlayNextCutscene(9);
                            break;
                        case 39:
                            currentSpeechInstance = 40;
                            maxSpeechInstance = 44;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 44:
                            currentSpeechInstance = 45;
                            maxSpeechInstance = 45;
                            TutorialCutsceneScript.StopCoroutine(TutorialCutsceneScript.PlayingListener(0));
                            TutorialCutsceneScript.PlayNextCutscene(11);
                            break;
                        case 45:
                            currentSpeechInstance = 46;
                            maxSpeechInstance = 46;
                            TutorialCutsceneScript.StopCoroutine(TutorialCutsceneScript.PlayingListener(0));
                            TutorialCutsceneScript.PlayNextCutscene(12);
                            break;
                        case 46:
                            //pick up prompt auki
                            currentSpeechInstance = 47;
                            maxSpeechInstance = 53;
                            break;
                        case 53:
                            TutorialCutsceneScript.StopCoroutine(TutorialCutsceneScript.PlayingListener(0));
                            TutorialCutsceneScript.PlayNextCutscene(13);
                            break;
                        /*case 9:
                            player.gameObject.GetComponent<MovementControls>().stop = false;
                            player.AddFirefly();
                            Canvas.SetActive(false); 
                            Destroy(gameObject);
                            currentSpeechInstance = 10;
                            maxSpeechInstance = 13;
                            break;
                        case 13:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 14;
                            break;
                        case 15:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 16;
                            break;
                        case 16:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 17;
                            break;
                        case 20:
                            player.gameObject.GetComponent<MovementControls>().stop = false;
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 21;
                            break;
                        case 22:
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            currentSpeechInstance = 23;
                            break;
                        case 27:
                            player.gameObject.GetComponent<MovementControls>().stop = false;
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            break;
                        case 36:
                            player.gameObject.GetComponent<MovementControls>().stop = false;
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            break;
                        case 39:
                            player.gameObject.GetComponent<MovementControls>().stop = false;
                            Canvas.SetActive(false);
                            Destroy(gameObject);
                            break;*/
                    }
                    
                }
            }
            //Jos kävelee kesken pois suljetaan puhekupla ja poistetaan kuuntelija
            else if (walkedAway == true)
            {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                m_MyEvent.RemoveListener(TalkEvent);
            }
            //Muussa tapauksessa päivitetään puhekupla
            else {
                speechCreator.UpdateSpeechBubble(this);

                //Ja päivitetään kamera puheenvuoron perusteella
                switch (currentSpeechInstance)
                {
                    case 2:
                        TutorialCutsceneScript.talkingMilaVCam.SetActive(true);
                        TutorialCutsceneScript.activeVCam = TutorialCutsceneScript.talkingMilaVCam;
                        break;
                    case 3:
                        TutorialCutsceneScript.talkingFiaVCam.SetActive(true);
                        TutorialCutsceneScript.talkingMilaVCam.SetActive(false);
                        Debug.Log("Fian pitäisi olla aktiivinen kamera");
                        break;
                    case 4:
                        TutorialCutsceneScript.talkingMilaVCam.SetActive(true);
                        TutorialCutsceneScript.talkingFiaVCam.SetActive(false);
                        break;
                    case 5:
                        TutorialCutsceneScript.talkingFiaVCam.SetActive(true);
                        TutorialCutsceneScript.talkingMilaVCam.SetActive(false);
                        break;
                    case 6:
                        TutorialCutsceneScript.talkingFiaVCam.SetActive(true);
                        TutorialCutsceneScript.talkingMilaVCam.SetActive(false);
                        break;
                    case 7:
                        TutorialCutsceneScript.talkingMilaVCam.SetActive(true);
                        TutorialCutsceneScript.talkingFiaVCam.SetActive(false);
                        break;
                    case 8:
                        TutorialCutsceneScript.talkingFiaVCam.SetActive(true);
                        break;
                    /*case 16:
                        if (TutorialCutsceneScript.director3.state == UnityEngine.Playables.PlayState.Playing)
                        {
                            TutorialCutsceneScript.director3.Stop();
                        }
                        TutorialCutsceneScript.vCam3.SetActive(true);
                        TutorialCutsceneScript.activeVCam = TutorialCutsceneScript.vCam3;
                        break;
                    case 17:
                        TutorialCutsceneScript.director4.Play();
                        break;
                    case 28:
                        TutorialCutsceneScript.director7.Play();
                        break;
                    case 30:
                        TutorialCutsceneScript.director7.Stop();
                        TutorialCutsceneScript.vCam4.SetActive(true);
                        break;
                    case 33:
                        TutorialCutsceneScript.SwitchVCam(2);
                        break;
                    case 34:
                        TutorialCutsceneScript.SwitchVCam(1);
                        break;
                    case 35:
                        TutorialCutsceneScript.SwitchVCam(2);
                        break;
                    case 47:
                        TutorialCutsceneScript.SwitchVCam(2);
                        break;
                    case 48:
                        TutorialCutsceneScript.SwitchVCam(2);
                        break;
                    case 49:
                        TutorialCutsceneScript.SwitchVCam(1);
                        break;
                    case 50:
                        TutorialCutsceneScript.SwitchVCam(2);
                        break;
                    case 51:
                        TutorialCutsceneScript.SwitchVCam(2);
                        break;
                    case 52:
                        TutorialCutsceneScript.SwitchVCam(1);
                        break;
                    case 53:
                        TutorialCutsceneScript.SwitchVCam(2);
                        break;*/

                }
            }
        }
        //Jos keskustelu aloitetaan, luodaan enismmäinen puhekupla
        else {
            speechCreator.GenerateSpeechBubble(this);
            talking = true;
            if (currentSpeechInstance == 3)
            {
                TutorialCutsceneScript.talkingFiaVCam.SetActive(true);
                TutorialCutsceneScript.talkingMilaVCam.SetActive(false);
            }
        }
    }

    public void Backwards() {
        speechCreator.BackWards(this);
    }
    //Ruikutuskupla pysyy tietyn aikaa
    IEnumerator CloseWhineBox(float time) {
        yield return new WaitForSeconds(time);

        if (walkedAway) {
            speechCreator.CloseSpeechBubble(this);
            talking = false;
            m_MyEvent.RemoveListener(TalkEvent);
        }
        else {
            Canvas.SetActive(true);
            speechCreator.CloseSpeechBubble(this);
            talking = false;
        }
    }

    public void NextSpeechFromCutscene(int newCurrentSpeechInstance)
    {
        currentSpeechInstance = newCurrentSpeechInstance;
        //Lisätään kuuntelija ja avataan kyssäri-ikoni. 
        walkedAway = false;
        if (player.gameObject.GetComponent<Character>() != null && !speechCreator.StillTalking())
        {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(TalkEvent);
            m_SecondEvent.AddListener(Backwards);
            if (id == 6)
            {
                switch (currentSpeechInstance)
                {
                    case 1:
                        m_MyEvent.Invoke();
                        break;
                    case 3:
                        m_MyEvent.Invoke();
                        break;
                    case 33:
                        m_MyEvent.Invoke();
                        break;
                    case 37:
                        m_MyEvent.Invoke();
                        break;
                    case 38:
                        m_MyEvent.Invoke();
                        break;
                    case 39:
                        m_MyEvent.Invoke();
                        break;
                    case 40:
                        m_MyEvent.Invoke();
                        break;
                    case 45:
                        m_MyEvent.Invoke();
                        break;
                    case 46:
                        m_MyEvent.Invoke();
                        break;
                    /*case 14:
                        m_MyEvent.Invoke();
                        break;
                    case 16:
                        m_MyEvent.Invoke();
                        break;
                    case 17:
                        player.gameObject.GetComponent<MovementControls>().stop = true;
                        m_MyEvent.Invoke();
                        break;
                    case 21:
                        m_MyEvent.Invoke();
                        break;
                    case 23:
                        player.gameObject.GetComponent<MovementControls>().stop = true;
                        m_MyEvent.Invoke();
                        break;
                    case 28:
                        player.gameObject.GetComponent<MovementControls>().stop = true;
                        m_MyEvent.Invoke();
                        break;
                    case 37:
                        player.gameObject.GetComponent<MovementControls>().stop = true;
                        m_MyEvent.Invoke();
                        break;*/
                }

            }
        }

    }
}
