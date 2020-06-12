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
    public bool auto = false;
    public KeyCode talkKey = KeyCode.None;
    public KeyCode talkBackKey = KeyCode.None;
    private Character player;
    public TutoLvl TutorialCutsceneScript;
    public GameObject FiaChild, journal, nextSpeechButton, burstParticleSystem;
    //public ParticleSystem burstParticleSystem;

    private void Start()
    {
        if (id == 6)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
        if (GameObject.FindGameObjectWithTag("UndyingObject") != null)
        {
            Undying_Object undyObj = GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>();
            if (undyObj.talkKey == KeyCode.None)
                talkKey = KeyCode.E;
            else
                talkKey = undyObj.talkKey;
        }
        else
            talkKey = KeyCode.E;
        talkBackKey = KeyCode.P;
    }

    void OnTriggerEnter(Collider player)
    {
        //Lisätään kuuntelija ja avataan kyssäri-ikoni. 
        walkedAway = false;
        if (player.gameObject.GetComponent<Character>() != null && !speechCreator.StillTalking())
        {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(TalkEvent);
            m_SecondEvent.AddListener(Backwards);
            //Tietyt NPC voivat alkaa älisee heti
            if (id == 6 && currentSpeechInstance == 25)
            {
                m_MyEvent.Invoke();
                /*switch (currentSpeechInstance)
                {
                    /*case 1:
                        player.gameObject.GetComponent<MovementControls>().stop = true;
                        m_MyEvent.Invoke();
                        break;*/
                /*case 13:
                    if (Input.GetKeyDown(talkKey))
                    {
                        m_MyEvent.Invoke();
                    }
                    /*if (player.GetComponent<Character>().hasRaskovnik)
                    {
                        currentSpeechInstance = 20;
                        maxSpeechInstance = 20;
                    }
                    TutorialCutsceneScript.PlayNextCutscene(2);
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
                        break;
            }

                if (Input.GetKeyDown(talkKey))
                {
                    m_MyEvent.Invoke();
                }*/
            }
        }
    }
    void OnTriggerExit(Collider player)
    {
        Canvas.SetActive(false);
        talking = false;
        speechCreator.CloseSpeechBubble(this);
        m_MyEvent.RemoveListener(TalkEvent);

        /*walkedAway = true;
        if (player.gameObject.GetComponent<Character>() != null)
        {
            //Jos lähtee pois kesken keskustelun niin keskusteluketju resetoituu ensimmäiseen
            if (!speechCreator.StillTalking() || currentSpeechInstance == maxSpeechInstance)
            {
                Canvas.SetActive(false);
                talking = false;
                //Jotkut NPC ei vät välttämättä resetoidu?
                if (id != 6)
                {
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
                }
            }
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(talkKey) && m_MyEvent != null && auto == false)
        {
            if (id == 6)
            {
                m_MyEvent.Invoke();
            }
        }
        else if (Input.GetKeyDown(talkBackKey) && m_MyEvent != null && auto == false)
        {
            m_SecondEvent.Invoke();
        }
    }

    public void TalkEvent()
    {
        Debug.Log("BBBBBBBBBBB");
        //tallennukselle välitetään pelaajan kautta missä kohtaa keskustelua mennään
        player.NPCspeechInstance = currentSpeechInstance;
        if (talking == true)
        {
            //Jos ollaan vikassa puhekerrassa niin suljetaan puhekupla
            if (currentSpeechInstance == maxSpeechInstance)
            {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                if (!walkedAway)
                    Canvas.SetActive(true);
                //Jotkut npc:t voivat lähteä kävelemään tms.
                if (id == 6)
                {
                    Canvas.SetActive(false);
                    m_MyEvent.RemoveListener(TalkEvent);
                    m_SecondEvent.RemoveListener(Backwards);

                    switch (maxSpeechInstance)
                    {
                        case 1:
                            currentSpeechInstance = 2;
                            maxSpeechInstance = 2;
                            //Mila nousee seisomaan -animaatio
                            if (TutorialCutsceneScript.playCutscenes == true)
                            {
                                StartCoroutine(WaitASec(2.0f));
                            }
                            else
                            {
                                StartCoroutine(WaitASec(0f));
                            }
                            break;
                        case 2:
                            currentSpeechInstance = 3;
                            maxSpeechInstance = 12;
                            //Mila kävelee vähän eteen päin -animaatio, tohon^ ehkä uus yleiskuva-vcam?)
                            if (TutorialCutsceneScript.playCutscenes == true)
                            {
                                TutorialCutsceneScript.SwitchVCam(1);
                                StartCoroutine(WaitASec(2.0f));
                            }
                            else
                            {
                                StartCoroutine(WaitASec(0f));
                            }
                            break;
                        case 12:
                            currentSpeechInstance = 13;
                            maxSpeechInstance = 17;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            if (!TutorialCutsceneScript.playCutscenes)
                            {
                                StartCoroutine(TutorialCutsceneScript.FiaMove(1));
                            }
                            break;
                        case 17:
                            currentSpeechInstance = 18;
                            maxSpeechInstance = 19;
                            TutorialCutsceneScript.holder02.SetActive(false);
                            TutorialCutsceneScript.cutsceneFinished = true;
                            player.AddFirefly();
                            StartCoroutine(SpiralAround());
                            break;
                        case 19:
                            currentSpeechInstance = 20;
                            maxSpeechInstance = 20;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 24:
                            TutorialCutsceneScript.cutsceneFinished = true;
                            currentSpeechInstance = 25;
                            maxSpeechInstance = 26;
                            break;
                        case 30:
                            transform.position = TutorialCutsceneScript.hiddenFiaLocation.transform.position;
                            TutorialCutsceneScript.exclaMark.SetActive(false);
                            TutorialCutsceneScript.cutsceneFinished = true;
                            currentSpeechInstance = 31;
                            maxSpeechInstance = 32;
                            break;
                        case 32:
                            currentSpeechInstance = 33;
                            maxSpeechInstance = 36;
                            TutorialCutsceneScript.PlayNextCutscene(7);
                            if (!TutorialCutsceneScript.playCutscenes)
                                TutorialCutsceneScript.CurrentPlayingListener = StartCoroutine(TutorialCutsceneScript.PlayingListener(7));
                            break;
                        case 36:
                            //korppikohta tähän
                            currentSpeechInstance = 37;
                            maxSpeechInstance = 37;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 37:
                            //arkku aukeaa
                            currentSpeechInstance = 38;
                            maxSpeechInstance = 39;
                            //ehkä tähän väliin prompt open
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 39:
                            currentSpeechInstance = 40;
                            maxSpeechInstance = 44;
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
                        case 46:
                            currentSpeechInstance = 47;
                            maxSpeechInstance = 53;
                            journal.gameObject.GetComponent<Collider>().enabled = true;
                            break;
                        case 53:
                            TutorialCutsceneScript.cutsceneFinished = true;
                            break;
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
            else
            {
                speechCreator.UpdateSpeechBubble(this);
                //Ja päivitetään kamera puheenvuoron perusteella
                if (TutorialCutsceneScript.playCutscenes)
                {
                    switch (currentSpeechInstance)
                    {
                        case 3:
                            TutorialCutsceneScript.Fia.transform.position = TutorialCutsceneScript.FiaLocation1.transform.position;
                            TutorialCutsceneScript.Fia.GetComponent<HoveringObject>().ResetPos();
                            TutorialCutsceneScript.SwitchVCam(2);
                            break;
                        case 4:
                        case 7:
                        case 34:
                        case 49:
                        case 52:
                            TutorialCutsceneScript.SwitchVCam(1);
                            break;
                        case 5:
                        case 6:
                        case 8:
                        case 35:
                        case 48:
                        case 50:
                        case 51:
                        case 53:
                            TutorialCutsceneScript.SwitchVCam(2);
                            break;
                        case 14:
                            TutorialCutsceneScript.PlayNextCutscene(2);
                            break;
                        case 16:
                            TutorialCutsceneScript.cutsceneFinished = true;
                            TutorialCutsceneScript.SwitchVCam(3);
                            break;
                        case 17:
                            TutorialCutsceneScript.SwitchVCam(4);
                            break;
                        case 28:
                            TutorialCutsceneScript.vCam9.SetActive(false);
                            TutorialCutsceneScript.vCam10.SetActive(true);
                            break;
                        case 29:
                            TutorialCutsceneScript.director6.Play();
                            break;
                        case 30:
                            TutorialCutsceneScript.vCam10.SetActive(false);
                            TutorialCutsceneScript.vCam9.SetActive(true);
                            break;
                    }
                }
            }
        }
        //Jos keskustelu aloitetaan, luodaan enismmäinen puhekupla
        else
        {
            Debug.Log("CCCCCCCCCCCCC");
            speechCreator.GenerateSpeechBubble(this);
            talking = true;
            //Sulava kamerasiirtymä kun Fia näytetään ekan kerran
            if (id == 6 && currentSpeechInstance == 2 && TutorialCutsceneScript.playCutscenes)
            {
                TutorialCutsceneScript.Fia.transform.position = TutorialCutsceneScript.FiaLocation1.transform.position;
                TutorialCutsceneScript.SwitchVCam(2);
            }
            //kun fia alkaa puhumaan niin aktivoidaan sen vcam
            else if (id == 6 && currentSpeechInstance > 2 && currentSpeechInstance < 18 && TutorialCutsceneScript.playCutscenes)
            {
                if (TutorialCutsceneScript.mainCamera.activeSelf == true)
                {
                    TutorialCutsceneScript.mainCamera.SetActive(false);
                    TutorialCutsceneScript.cutsceneCamera.SetActive(true);
                }
                TutorialCutsceneScript.SwitchVCam(2);
                player.transform.position = TutorialCutsceneScript.MilaLocation2.transform.position;
                player.transform.rotation = TutorialCutsceneScript.MilaLocation2.transform.rotation;

                if (currentSpeechInstance == 13)
                    TutorialCutsceneScript.exclaMark.SetActive(false);
            }
        }
    }

    public void Backwards()
    {
        speechCreator.BackWards(this);
    }
    //Ruikutuskupla pysyy tietyn aikaa
    IEnumerator CloseWhineBox(float time)
    {
        yield return new WaitForSeconds(time);

        if (walkedAway)
        {
            speechCreator.CloseSpeechBubble(this);
            talking = false;
            m_MyEvent.RemoveListener(TalkEvent);
        }
        else
        {
            Canvas.SetActive(true);
            speechCreator.CloseSpeechBubble(this);
            talking = false;
        }
    }

    public void NextSpeechFromCutscene(int newCurrentSpeechInstance, int newMaxSpeechInstance)
    {
        currentSpeechInstance = newCurrentSpeechInstance;
        maxSpeechInstance = newMaxSpeechInstance;
        //Lisätään kuuntelija ja avataan kyssäri-ikoni. 
        walkedAway = false;
        if (player.gameObject.GetComponent<Character>() != null && !speechCreator.StillTalking())
        {
            m_MyEvent.AddListener(TalkEvent);
            m_SecondEvent.AddListener(Backwards);
            if (id == 6)
            {
                m_MyEvent.Invoke();
                Debug.Log("AAAAAAAAAAAAAAAAAAAA");
            }
        }
    }

    public IEnumerator WaitASec(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextSpeechFromCutscene(currentSpeechInstance, maxSpeechInstance);
    }

    public IEnumerator SpiralAround()
    {
        //kerätty kärpänen kiertää spiraalissa milan positioon, ja "katoaa takin alle"

        //spiraali alkaa aina samasta kohdasta milan edessä, vois myöhemmin muokata sulavammaks koska kärpänen ei aina oo just siinä kun sen kerää
        Vector3 temp;
        temp = player.transform.position;
        temp.z++;

        GameObject parentHolder = transform.parent.gameObject;
        transform.parent = player.transform;

        int spiralSpeed = 30;
        float distance = (transform.position - player.transform.position).magnitude / 150;
        float halfDistance = (transform.position - player.transform.position).magnitude / 2;
        Collider col = null;
        if (FiaChild.GetComponentInChildren<Collider>() != null)
        {
            col = FiaChild.GetComponentInChildren<Collider>();
            col.enabled = false;
        }
        //spiralointi
        while (transform.position.x != player.transform.position.x && transform.position.z != player.transform.position.z)
        {
            yield return new WaitForEndOfFrame();
            temp = player.transform.position;
            temp.y += 0.6f;

            transform.RotateAround(temp, Vector3.up, spiralSpeed * Time.deltaTime);
            spiralSpeed += 5;

            temp = transform.position;
            temp.y = player.transform.position.y;

            if ((transform.position - player.transform.position).magnitude < halfDistance && transform.localScale.x > 0)
            {
                Vector3 tmp = new Vector3(transform.localScale.x - 0.001f, transform.localScale.x - 0.001f, transform.localScale.x - 0.001f);
                transform.localScale -= new Vector3(0.002f, 0.002f, 0.002f);
            }

            if ((temp - player.transform.position).magnitude > 0.1f)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, distance);
            else
                transform.position = player.transform.position;
            yield return null;
        }

        //partikkeliefekti kun katoaa
        burstParticleSystem.SetActive(true);

        //DESTROY EI KÄY se pilaa kaikki loput puhelut
        //Destroy(gameObject);
        FiaChild.SetActive(false);
        this.GetComponent<Collider>().enabled = false;
        transform.parent = parentHolder.transform;
    }
}