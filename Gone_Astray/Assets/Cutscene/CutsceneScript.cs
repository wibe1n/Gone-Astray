using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneScript : MonoBehaviour {

    public GameObject mainCamera, cutsceneCamera, cutsceneHolder, cutsceneCanvas;
    public PlayableDirector director;
    public bool checkTimeline = false;
    public MovementControls movementControls;

	// Use this for initialization
	void Start () {
        if (cutsceneCamera.activeSelf == true)
        {
            cutsceneCamera.SetActive(false);
        }
        if (mainCamera.activeSelf == false)
        {
            mainCamera.SetActive(true);
        }
    }
	
    void Update()
    {
        if (checkTimeline == true)
        {
            if(director.state == PlayState.Paused || Input.GetKeyDown(KeyCode.O))
            {
                EndCutscene();
            }

        }
    }

	void OnTriggerEnter(Collider player)
    {
        movementControls.stop = true;
        mainCamera.SetActive(false);
        cutsceneCamera.SetActive(true);
        director.Play();
        checkTimeline = true;
        cutsceneCanvas.SetActive(true);
    }

    void EndCutscene()
    {
        cutsceneCanvas.SetActive(false);
        cutsceneCamera.SetActive(false);
        mainCamera.SetActive(true);
        cutsceneHolder.SetActive(false);
        movementControls.stop = false;
    }
}
