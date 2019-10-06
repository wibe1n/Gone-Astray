using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class TutoLvl : MonoBehaviour {

    public GameObject mainCamera, cutsceneCamera, cutsceneHolder, cutsceneCanvas, inGameCanvas, player;
    public PlayableDirector director;
    public MovementControls movementControls;
    public UnityEvent cutsceneEvent = new UnityEvent();
    public bool playCutscenes;

    // Use this for initialization
    void Start ()
    {
        if (playCutscenes)
        {
            PlayNextCutscene(1);
        }
    }

    public void PlayNextCutscene(int id)
    {
        
        movementControls.stop = true;
        mainCamera.SetActive(false);
        cutsceneCamera.SetActive(true);

        inGameCanvas.SetActive(false);
        cutsceneCanvas.SetActive(true);
        

        switch (id)
        {
            case 1:
                //jotain
                //director.Play();
                //cutsceneEvent.AddListener(PlayingListener);
                break;
            case 2:
                //jotain
                break;
            default:
                //error
                EndCutscene();
                break;
        }
    }

    void PlayingListener()
    {
        if (director.state == PlayState.Paused || Input.GetKeyDown(KeyCode.E))
        {
            EndCutscene();
        }
    }

        void EndCutscene()
    {
        cutsceneEvent.RemoveListener(PlayingListener);

        cutsceneCanvas.SetActive(false);
        cutsceneCamera.SetActive(false);
        inGameCanvas.SetActive(true);
        mainCamera.SetActive(true);
        cutsceneHolder.SetActive(false);
        movementControls.stop = false;
    }
}
