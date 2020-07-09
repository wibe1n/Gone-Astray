using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutoLvl : MonoBehaviour {

    public GameObject mainCamera, cutsceneCamera, holder01, holder02, holder03, holder05, holder06, holder07, holder08, cutsceneCanvas, inGameCanvas, blackScreen, player, Fia, cutsceneFia, hedgehog;
    public GameObject chest, chestLocation, cutsceneJournal, startPos, exclaMark, pressToSkipText;
    public GameObject MilaLocation1, MilaLocation2, MilaLocation3, MilaLocation4, MilaLocation5, MilaLocation6, MilaLocation7, MilaLocation8, FiaLocation1, FiaLocation2, FiaLocation3, FiaLocation4, FiaLocation5, FiaLocation6, FiaLocation7, FiaLocation8, FiaLocation9, FiaLocation10, hiddenFiaLocation;
    public GameObject activeVCam, vCam1, vCam2, vCam3, vCam4, vCam5, vCam7, vCam8, vCam9, vCam10, vCam11, vCam12, vCam13, vCam14, vCam15, dollyCam1, TLCam2;
    public GameObject talkingMilaVCam, talkingFiaVCam, talkingCutsceneFiaVCam;
    public PlayableDirector director1, director2, director3, director5, director6, director7, director8;
    public MovementControls movementControls;
    public NPC NPCScript;
    public CameraController2 mainCameraScript;
    public UnityEvent cutsceneEvent = new UnityEvent();
    public bool playCutscenes, cutsceneFinished;
    //alla pointtereita coroutineille, jotta niitä voisi keskeyttää tarvittaessa
    public Coroutine CurrentPlayingListener, currentPressToSkipText, currentBlackScreenFadeCut;

    // Use this for initialization
    void Start ()
    {
        if (playCutscenes)
        {
            currentBlackScreenFadeCut = StartCoroutine(BlackScreenFadeCut(1, 3.0f));
            currentPressToSkipText = StartCoroutine(PressToSkipText());
            PlayNextCutscene(1);
        }
        else
        {
            cutsceneCamera.SetActive(false);
            mainCamera.SetActive(true);
        }

    }

    private void Update()
    {
        
    }

    public void PlayNextCutscene(int id)
    {
        if (playCutscenes)
        {
            movementControls.stop = true;

            if (id != 7 && mainCamera.activeSelf == true)
            {
                mainCamera.SetActive(false);
                cutsceneCamera.SetActive(true);
            }
            inGameCanvas.SetActive(false);

            cutsceneFinished = false;


            switch (id)
            {
                case 1:
                    //Kamera kiertää metsässä
                    cutsceneCanvas.SetActive(true);
                    vCam1.SetActive(true);
                    activeVCam = vCam1;
                    player.transform.position = MilaLocation1.transform.position;
                    player.transform.rotation = MilaLocation1.transform.rotation;
                    Fia.transform.position = hiddenFiaLocation.transform.position;
                    mainCameraScript.ResetCameraPos();
                    director1.Play();
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
                case 2:
                    //Fian puuta esitellään
                    holder02.SetActive(true);
                    director2.Play();
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
                case 3:
                    //siili ilmestyy
                    cutsceneCanvas.SetActive(true);
                    holder03.SetActive(true);
                    cutsceneFia.SetActive(true);
                    cutsceneFia.transform.position = FiaLocation3.transform.position;
                    player.transform.position = MilaLocation3.transform.position;
                    player.transform.rotation = MilaLocation3.transform.rotation;
                    director3.Play();
                    hedgehog.SetActive(true);
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
                case 4:
                    //Rashkovnikin esittely
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
                case 5:
                    //Mila astuu sisään puuhun
                    cutsceneCanvas.SetActive(true);
                    holder05.SetActive(true);
                    cutsceneFia.transform.position = FiaLocation10.transform.position;
                    cutsceneFia.GetComponentInChildren<HoveringObject>().ResetPos();
                    cutsceneFia.SetActive(true);
                    director5.Play();
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
                case 6:
                    //Tullaan kellariin
                    cutsceneCanvas.SetActive(true);
                    holder06.SetActive(true);
                    player.transform.position = MilaLocation4.transform.position;
                    player.transform.rotation = MilaLocation4.transform.rotation;
                    cutsceneFia.transform.position = FiaLocation4.transform.position;
                    cutsceneFia.GetComponentInChildren<HoveringObject>().ResetPos();
                    cutsceneFia.SetActive(true);
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
                case 7:
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
                case 8:
                    cutsceneCanvas.SetActive(true);
                    holder08.SetActive(true);
                    player.transform.position = MilaLocation7.transform.position;
                    player.transform.rotation = MilaLocation7.transform.rotation;
                    cutsceneFia.transform.position = FiaLocation7.transform.position;
                    cutsceneFia.transform.rotation = FiaLocation7.transform.rotation;
                    cutsceneFia.SetActive(true);
                    cutsceneFia.GetComponentInChildren<HoveringObject>(true).ResetPos();
                    NPCScript.auto = true;
                    director8.Play();
                    CurrentPlayingListener = StartCoroutine(PlayingListener(id));
                    break;
            }
        }
    }

    public IEnumerator PlayingListener(int id)
    {
        switch (id)
        {
            case 1:
                bool skipped = false;
                yield return new WaitUntil(() => (director1.state == PlayState.Paused || Input.GetKeyDown(KeyCode.R)));
                //Jos skipattiin niin musta ruutu -haihdutusleikkaus
                if (director1.state != PlayState.Paused)
                {
                    StartCoroutine(BlackScreenFadeCut());
                    yield return new WaitForSeconds(1.0f);
                    director1.Stop();
                    skipped = true;
                }

                if (director1.state == PlayState.Paused)
                {
                    vCam1.SetActive(true);
                    dollyCam1.SetActive(false);
                    if (skipped)
                        StartCoroutine(PressToSkipText());
                    yield return new WaitForSeconds(3.0f);
                    //tähän asti fia nyyhkyttää istuen puuta vasten (crouch toistaiseksi), sitten hätkähtää (mikä toistaiseksi????)
                    NPCScript.NextSpeechFromCutscene(1, 1);
                }
                yield return new WaitUntil(() => !(Input.GetKeyDown(KeyCode.R)));
                yield return new WaitUntil(() => (cutsceneFinished == true || Input.GetKeyDown(KeyCode.R)));
                if (cutsceneFinished != true)
                {
                    cutsceneFinished = true;
                    NPCScript.currentSpeechInstance = 12;
                    NPCScript.maxSpeechInstance = 12;
                    NPCScript.TalkEvent();
                    StartCoroutine(BlackScreenFadeCut());
                    yield return new WaitForSeconds(2f);
                    Fia.transform.position = FiaLocation1.transform.position;
                }
                holder01.SetActive(false);
                player.transform.position = startPos.transform.position;
                player.transform.rotation = startPos.transform.rotation;
                mainCameraScript.ResetCameraPos();
                break;
            case 2:
                while (cutsceneFinished != true)
                {
                    yield return null;
                }
                director2.Stop();
                vCam2.SetActive(false);
                cutsceneFinished = false;
                yield return new WaitUntil(() => (cutsceneFinished == true));
                break;
            case 3:
                if (playCutscenes)
                {
                    currentPressToSkipText = StartCoroutine(PressToSkipText(1f));
                    yield return new WaitUntil(() => (director3.state == PlayState.Paused || Input.GetKeyDown(KeyCode.R)));
                }
                if (director3.state != PlayState.Paused || !playCutscenes)
                {
                    hedgehog.SetActive(true);
                    StartCoroutine(BlackScreenFadeCut());
                    yield return new WaitForSeconds(1.0f);
                    director3.Stop();
                    Vector3 temp = new Vector3(79.3f, 0.4f, 50.9f);
                    hedgehog.transform.position = temp;
                }
                holder03.SetActive(false);
                cutsceneFia.SetActive(false);
                break;
            case 4:
                vCam8.SetActive(true);
                vCam8.transform.position = mainCamera.transform.position;
                vCam8.transform.rotation = mainCamera.transform.rotation;
                activeVCam = vCam8;
                StartCoroutine(LerpFieldOfView());
                if (mainCamera.activeSelf == true)
                {
                    mainCamera.SetActive(false);
                    cutsceneCamera.SetActive(true);
                }
                yield return null;
                SwitchVCam(7);
                NPCScript.NextSpeechFromCutscene(18, 19);
                yield return new WaitUntil(() => (cutsceneFinished == true));
                vCam7.SetActive(false);
                break;
            case 5:
                currentPressToSkipText = StartCoroutine(PressToSkipText(1f));
                yield return new WaitForSeconds(3.0f);
                NPCScript.NextSpeechFromCutscene(21, 24);
                yield return new WaitUntil(() => (cutsceneFinished == true || Input.GetKeyDown(KeyCode.R)));
                if (cutsceneFinished != true)
                {
                    NPCScript.currentSpeechInstance = 24;
                    NPCScript.maxSpeechInstance = 24;
                    NPCScript.TalkEvent();
                    StartCoroutine(BlackScreenFadeCut());
                    yield return new WaitForSeconds(2.0f);
                }
                holder05.SetActive(false);
                cutsceneFia.SetActive(false);
                break;
            case 6:
                currentPressToSkipText = StartCoroutine(PressToSkipText());
                yield return new WaitForSeconds(3.0f);
                NPCScript.NextSpeechFromCutscene(27, 30);
                yield return new WaitUntil(() => (cutsceneFinished == true || Input.GetKeyDown(KeyCode.R)));
                if (cutsceneFinished != true)
                {
                    NPCScript.currentSpeechInstance = 30;
                    NPCScript.maxSpeechInstance = 30;
                    NPCScript.TalkEvent();
                    StartCoroutine(BlackScreenFadeCut());
                    yield return new WaitForSeconds(2.0f);
                }
                holder06.SetActive(false);
                cutsceneFia.SetActive(false);
                break;
            case 7:
                StartCoroutine(BlackScreenFadeCut());
                yield return new WaitForSeconds(1.0f);
                if (mainCamera.activeSelf == true)
                {
                    mainCamera.SetActive(false);
                    cutsceneCamera.SetActive(true);
                }
                holder07.SetActive(true);
                cutsceneCanvas.SetActive(true);
                //Skippausta kuunteleva coroutine
                Coroutine skipCheck = StartCoroutine(SkipCutscene(7));

                cutsceneFia.transform.position = FiaLocation5.transform.position;
                cutsceneFia.GetComponentInChildren<HoveringObject>().ResetPos();
                cutsceneFia.SetActive(true);
                player.transform.position = MilaLocation5.transform.position;
                player.transform.rotation = MilaLocation5.transform.rotation;
                chest.transform.position = chestLocation.transform.position;
                chest.transform.rotation = chestLocation.transform.rotation;
                chest.transform.localScale = new Vector3(70, 70, 70);
                currentPressToSkipText = StartCoroutine(PressToSkipText());

                //timeline jossa mila kiskoo arkkua vähän matkaa
                yield return new WaitForSeconds(5.0f);

                NPCScript.NextSpeechFromCutscene(33, 36);

                yield return new WaitUntil(() => cutsceneFinished == true);
                cutsceneFinished = false;

                //korppikohta

                SwitchVCam(12);
                NPCScript.NextSpeechFromCutscene(37, 37);

                yield return new WaitUntil(() => cutsceneFinished == true);
                cutsceneFinished = false;
                director7.Play();
                vCam11.SetActive(false);
                vCam12.SetActive(false);
                yield return new WaitUntil(() => director7.state == PlayState.Paused);
                /*activeVCam.SetActive(false );
                TLCam2.SetActive(false);
                activeVCam = TLCam2;*/
                player.transform.position = MilaLocation6.transform.position;
                player.transform.rotation = MilaLocation6.transform.rotation;
                cutsceneFia.transform.position = FiaLocation6.transform.position;
                cutsceneFia.transform.rotation = FiaLocation6.transform.rotation;
                NPCScript.NextSpeechFromCutscene(38, 39);
                //Tähän v ehkä oma kamera joka näyttää molemmat?
                SwitchVCam(2);

                yield return new WaitUntil(() => cutsceneFinished == true);

                holder07.SetActive(false);
                cutsceneFia.SetActive(false);
                //Jos ei skipattu niin suljetaan sitä kuunteleva coroutine
                StopCoroutine(skipCheck);
                break;
            case 8:
                Coroutine skipCheck2 = StartCoroutine(SkipCutscene(8));
                currentPressToSkipText = StartCoroutine(PressToSkipText());
                yield return new WaitUntil(() => director8.state == PlayState.Paused);
                NPCScript.auto = false;

                NPCScript.NextSpeechFromCutscene(44, 44);
                FiaMove(2);
                yield return new WaitUntil(() => NPCScript.talking == false);
                yield return new WaitForSeconds(2.0f);
                NPCScript.NextSpeechFromCutscene(45, 45);
                yield return new WaitUntil(() => NPCScript.talking == false);
                //mila kääntyy
                //while
                //player.transform.Rotate(Vector3.up, -50f * Time.deltaTime);
                yield return new WaitForSeconds(2.0f);
                player.transform.position = MilaLocation8.transform.position;
                player.transform.rotation = MilaLocation8.transform.rotation;
                cutsceneFia.transform.position = FiaLocation8.transform.position;
                cutsceneFia.GetComponentInChildren<HoveringObject>().ResetPos();
                SwitchVCam(14);
                StartCoroutine(RotateCamera());
                NPCScript.NextSpeechFromCutscene(46, 46);
                yield return new WaitUntil(() => player.GetComponent<Character>().hasJournal == true);
                cutsceneFinished = true;
                cutsceneJournal.SetActive(true);
                cutsceneFia.transform.position = FiaLocation9.transform.position;
                SwitchVCam(1);
                yield return new WaitForSeconds(3.0f);
                SwitchVCam(2);
                cutsceneFinished = false;
                NPCScript.NextSpeechFromCutscene(47, 53);
                yield return new WaitUntil(() => cutsceneFinished == true);
                cutsceneFinished = false;
                SwitchVCam(15);
                yield return new WaitForSeconds(3.0f);
                StartCoroutine(PullBack());

                StartCoroutine(BlackScreenFadeCut());
                yield return new WaitForSeconds(2.0f);
                cutsceneFinished = true;
                holder08.SetActive(false);
                cutsceneFia.SetActive(false);
                StopCoroutine(skipCheck2);
                break;
        }

        EndCutscene();

        if (id == 1)
        {
            yield return new WaitForSeconds(1f);
            //INFO1 movement controls
            StartCoroutine(FiaMove(1));
        }
    }

    public void EndCutscene()
    {
        //StopCoroutine(CurrentPlayingListener);
        cutsceneFinished = false;

        cutsceneCanvas.SetActive(false);
        cutsceneCamera.SetActive(false);
        mainCamera.SetActive(true);
        inGameCanvas.SetActive(true);
        movementControls.stop = false;
    }

    public void SwitchVCam(int target)
    {
        activeVCam.SetActive(false);

        //target 1 = Mila
        //target 2 = Fia
        // 3 = apilapusikko jossa siili on
        // 7 = rashkovnik
        // 12 = lähikuvaa juuri ennen ku arkku avataan
        // 14 = journalia kuvataan ylhäältä
        // 15 = vika vcam
        switch (target)
        {
            case 1:
                talkingMilaVCam.SetActive(true);
                activeVCam = talkingMilaVCam;
                break;
            case 2:
                if (NPCScript.FiaChild.activeSelf == true)
                {
                    talkingFiaVCam.SetActive(true);
                    activeVCam = talkingFiaVCam;
                }
                else
                {
                    talkingCutsceneFiaVCam.SetActive(true);
                    activeVCam = talkingCutsceneFiaVCam;
                }
                break;
            case 3:
                vCam3.SetActive(true);
                activeVCam = vCam3;
                break;
            case 4:
                vCam4.SetActive(true);
                activeVCam = vCam4;
                break;
            case 7:
                vCam7.SetActive(true);
                activeVCam = vCam7;
                break;
            case 12:
                vCam12.SetActive(true);
                activeVCam = vCam12;
                break;
            case 14:
                vCam14.SetActive(true);
                activeVCam = vCam14;
                break;
            case 15:
                vCam15.SetActive(true);
                activeVCam = vCam15;
                break;
            default:
                //Error target num
                break;
        }
    }

    public IEnumerator FiaMove(int moveId)
    {
        if (moveId == 1)
        {
            while ((Fia.transform.position - FiaLocation2.transform.position).magnitude > 0)
            {
                Fia.transform.position = Vector3.MoveTowards(Fia.transform.position, FiaLocation2.transform.position, Time.deltaTime * /*speed*/10);
                yield return null;
            }
            Fia.GetComponent<Collider>().enabled = true;
            exclaMark.SetActive(true);
            exclaMark.transform.localScale = new Vector3(0, 0, 0);
            while (exclaMark.transform.localScale.x < 1)
            {
                exclaMark.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                yield return null;
            }
        }
        else
        {
            while ((Fia.transform.position - FiaLocation8.transform.position).magnitude > 0)
            {
                Fia.transform.position = Vector3.MoveTowards(Fia.transform.position, FiaLocation8.transform.position, Time.deltaTime * /*speed*/10);
                yield return null;
            }
        }
    }

    public IEnumerator BlackScreenFadeCut(int onlyFadeIn = 0, float fadeDuration = 1.0f)
    {
        blackScreen.SetActive(true);

        if (onlyFadeIn == 0)
        {
            if (currentBlackScreenFadeCut != null)
                StopCoroutine(currentBlackScreenFadeCut);
            else
                blackScreen.GetComponentInChildren<Image>().canvasRenderer.SetAlpha(0.0f);
            blackScreen.GetComponentInChildren<Image>().CrossFadeAlpha(1.0f, 0.5f, false);
            yield return new WaitForSeconds(2f);
        }
        else
            blackScreen.GetComponentInChildren<Image>().canvasRenderer.SetAlpha(1.0f);

        //jos press to skip väläytys on kesken, keskeytetään ja nollataan alfa
        if (currentPressToSkipText != null)
        {
            StopCoroutine(currentPressToSkipText);
            pressToSkipText.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        }

        blackScreen.GetComponentInChildren<Image>().CrossFadeAlpha(0.0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);
        blackScreen.GetComponentInChildren<Image>().canvasRenderer.SetAlpha(0.0f);
        blackScreen.SetActive(false);

        currentBlackScreenFadeCut = null;
    }

    public IEnumerator SkipCutscene(int id)
    {
        if (playCutscenes)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R));

        cutsceneFinished = true;
        StopCoroutine(CurrentPlayingListener);
        if (id == 7)
        {
            if (NPCScript.talking)
            {
                NPCScript.currentSpeechInstance = 39;
                NPCScript.maxSpeechInstance = 39;
                NPCScript.TalkEvent();
            }
            StartCoroutine(BlackScreenFadeCut());
            yield return new WaitForSeconds(2.0f);
            player.transform.position = MilaLocation6.transform.position;
            player.transform.rotation = MilaLocation6.transform.rotation;
            mainCameraScript.ResetCameraPos();
            holder07.SetActive(false);
        }
        if (id == 8)
        {
            if (NPCScript.talking)
            {
                NPCScript.currentSpeechInstance = 53;
                NPCScript.maxSpeechInstance = 53;
                NPCScript.TalkEvent();
            }
            StartCoroutine(BlackScreenFadeCut());
            yield return new WaitForSeconds(2.0f);
            if (director8.state == PlayState.Playing)
                director8.Stop();
            player.transform.position = MilaLocation8.transform.position;
            player.transform.rotation = MilaLocation8.transform.rotation;
            mainCameraScript.ResetCameraPos();
            holder08.SetActive(false);
            if (player.GetComponent<Character>().hasJournal == false)
                NPCScript.journal.gameObject.GetComponent<Collider>().enabled = true; 
        }
        cutsceneFia.SetActive(false);
        EndCutscene();
    }

    public IEnumerator RotateCamera()
    {
        //kameraa pyöritetään hitaasti kohdassa jossa kuvataan ylhäältä maassa olevaa journalia
        while (cutsceneFinished != true)
        {
            vCam14.transform.Rotate(Vector3.forward, 2f * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator PullBack()
    {
        //kameraa liikutetaan hitaasti taaksepäin
        while (cutsceneFinished != true)
        {
            vCam14.transform.Translate(-Vector3.forward * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator PressToSkipText(float waitTime = 2f)
    {
        pressToSkipText.SetActive(true);
        pressToSkipText.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForSeconds(waitTime);
        pressToSkipText.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.5f, false);
        yield return new WaitForSeconds(2f);
        pressToSkipText.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        pressToSkipText.SetActive(false);

        currentPressToSkipText = null;
    }

    public IEnumerator LerpFieldOfView()
    {
        cutsceneCamera.GetComponent<Camera>().fieldOfView = 94.97911f;
        /*
        while (cutsceneFinished != true)
        {
            vCam14.transform.Translate(-Vector3.forward * Time.deltaTime);
            yield return null;
        }
        */
        yield return null;
    }
}
