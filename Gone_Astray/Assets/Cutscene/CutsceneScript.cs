using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneScript : MonoBehaviour {

    public GameObject mainCamera, cutsceneCamera, cutsceneHolder;
    public PlayableDirector director;
    public bool checkTimeline = false;

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
            if(director.state == PlayState.Paused)
            {
                cutsceneCamera.SetActive(false);
                mainCamera.SetActive(true);
                cutsceneHolder.SetActive(false);
            }
        }
    }

	void OnTriggerEnter(Collider player)
    {
        mainCamera.SetActive(false);
        cutsceneCamera.SetActive(true);
        director.Play();
        checkTimeline = true;
    }
}
