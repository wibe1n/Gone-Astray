using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class TutoLvl : MonoBehaviour {

    public GameObject mainCamera, cutsceneCamera, cutsceneHolder, cutsceneCanvas, inGameCanvas, player, startLocation;
    public GameObject activeVCam, vCam1, vCam2, vCam3, vCam4, vCam5;
    public GameObject talkingMilaVCam, talkingFiaVCam;
    public PlayableDirector director1, director2, director3, director4, director5, director6, director7, director8, director9, director10, director11, director12, director13, director14, director15;
    public MovementControls movementControls;
    public NPC NPCScript;
    public CameraController2 mainCameraScript;
    public UnityEvent cutsceneEvent = new UnityEvent();
    public bool playCutscenes, cutsceneFinished, once, once2, once3, once4, once5, once6, once7, once8, once9, once10;

    // Use this for initialization
    void Start ()
    {
        if (playCutscenes)
        {
            PlayNextCutscene(1);
        }
        else
        {
            cutsceneCamera.SetActive(false);
            mainCamera.SetActive(true);
        }

        once = true;
        once2 = true;
        once3 = true;
        once4 = true;
        once5 = true;
        once6 = true;
        once7 = true;
        once8 = true;
        once9 = true;
        once10 = true;
    }

    public void PlayNextCutscene(int id)
    {
        if (playCutscenes)
        {
            movementControls.stop = true;
            //mainCamera.SetActive(false);
            //vCam1.SetActive(true);
            cutsceneCamera.SetActive(true);

            inGameCanvas.SetActive(false);

            cutsceneFinished = false;


            switch (id)
            {
                case 1:
                    cutsceneCanvas.SetActive(true);
                    activeVCam = vCam1;
                    player.transform.position = startLocation.transform.position;
                    player.transform.rotation = startLocation.transform.rotation;
                    mainCameraScript.ResetCameraPos();
                    director1.Play();
                    once = false;
                    StartCoroutine(PlayingListener(1));
                    break;
                case 2:
                    //Puun kierto ja apilapusikko
                    director3.Play();
                    StartCoroutine(PlayingListener(2));
                    break;
                case 3:
                    //Raskovnikin esittely
                    director5.Play();
                    once5 = false;
                    StartCoroutine(PlayingListener(3));
                    break;
                case 4:
                    //Mila astuu sisään puuhun
                    director6.Play();
                    StartCoroutine(PlayingListener(4));
                    break;
                case 5:
                    //Tullaan kellariin
                    vCam4.SetActive(true);
                    StartCoroutine(PlayingListener(5));
                    break;
                case 6:
                    //Mennään ulos arkun kanssa
                    director7.Play();
                    StartCoroutine(PlayingListener(6));
                    break;
                case 7:
                    //juuri ennen think we should open it
                    director8.Play();
                    once3 = false;
                    StartCoroutine(PlayingListener(7));
                    break;
                case 8:
                    //arkku aukeaa
                    director9.Play();
                    once4 = false;
                    StartCoroutine(PlayingListener(8));
                    break;
                case 9:
                    //arkku aukeaa
                    director10.Play();
                    once5 = false;
                    StartCoroutine(PlayingListener(9));
                    break;
                case 10:
                    //huhuillaan metsässä
                    director11.Play();
                    once6 = false;
                    StartCoroutine(PlayingListener(10));
                    break;
                case 11:
                    //juuri ennen kuin fia huomaa kirjan edessä
                    director12.Play();
                    once7 = false;
                    StartCoroutine(PlayingListener(11));
                    break;
                case 12:
                    //juostaan kirjan luo
                    director13.Play();
                    once8 = false;
                    StartCoroutine(PlayingListener(12));
                    break;
                case 13:
                    //kävellään yhdessä pimeyteen
                    director15.Play();
                    once10 = false;
                    StartCoroutine(PlayingListener(13));
                    break;
                default:
                    //error
                    EndCutscene();
                    break;
            }
        }
    }

    public IEnumerator PlayingListener(int id)
    {
        while (!(cutsceneFinished == true || Input.GetKeyDown(KeyCode.E)))
        {
            if (id == 1)
            {
                Debug.Log("BBBBBBBBBBbb");
                Debug.Log("once on " + once);
                if (director1.state == PlayState.Paused && once == false)
                {
                    Debug.Log("AAAAAAAAAAAAAAAAAA");
                    NPCScript.NextSpeechFromCutscene(1);
                    once = true;
                }
            }
            if (id == 2)
            {
                if (director2.state == PlayState.Paused && once2 == false)
                {
                    NPCScript.NextSpeechFromCutscene(3);
                    once2 = true;
                }
            }
            /*if (id == 6)
            {
                if (director7.state == PlayState.Paused && once3 == false)
                {
                    NPCScript.NextSpeechFromCutscene(33);
                    once3 = true;
                }
            }
            if (id == 7)
            {
                if (director8.state == PlayState.Paused && once4 == false)
                {
                    NPCScript.NextSpeechFromCutscene(37);
                    once4 = true;
                }
            }
            if (id == 8)
            {
                if (director9.state == PlayState.Paused && once5 == false)
                {
                    NPCScript.NextSpeechFromCutscene(38);
                    once5 = true;
                }
            }
            if (id == 9)
            {
                if (director10.state == PlayState.Paused && once6 == false)
                {
                    NPCScript.NextSpeechFromCutscene(39);
                    once6 = true;
                }
            }
            if (id == 10)
            {
                if (director11.state == PlayState.Paused && once7 == false)
                {
                    vCam5.SetActive(true);
                    NPCScript.NextSpeechFromCutscene(40);
                    once7 = true;
                }
            }
            if (id == 11)
            {
                if (director12.state == PlayState.Paused && once8 == false)
                {
                    NPCScript.NextSpeechFromCutscene(45);
                    once8 = true;
                }
            }
            if (id == 12)
            {
                if (director13.state == PlayState.Paused && once9 == false)
                {
                    //kuvataan ylhäältä
                    director14.Play();
                    once9 = false;
                    NPCScript.NextSpeechFromCutscene(46);
                    once9 = true;
                }
            }
            if (id == 10)
            {
                if (director15.state == PlayState.Paused && once10 == false)
                {
                    //thanks for playing ikkuna auki
                    once10 = true;
                }
            }*/
        }
        yield return new WaitUntil(() => (cutsceneFinished == true || Input.GetKeyDown(KeyCode.E)));
        EndCutscene();
    }

    public void EndCutscene()
    {
        StopCoroutine(PlayingListener(0));
        cutsceneFinished = false;

        cutsceneCanvas.SetActive(false);
        cutsceneCamera.SetActive(false);
        inGameCanvas.SetActive(true);
        mainCamera.SetActive(true);
        //cutsceneHolder.SetActive(false);
        movementControls.stop = false;
    }

    public void SwitchVCam(int target)
    {
        //target 1 = Mila
        //target 2 = Fia
        if (target == 1)
        {
            activeVCam.SetActive(false);
            talkingMilaVCam.SetActive(true);
            activeVCam = talkingMilaVCam;
        }
        if (target == 2)
        {
            activeVCam.SetActive(false);
            talkingFiaVCam.SetActive(true);
            activeVCam = talkingFiaVCam;
        }
    }
}
