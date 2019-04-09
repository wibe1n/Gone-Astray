﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	//tämä scripti teleporttaa pelaajan määrättyyn sijaintiin scenessä jos pelaajalla on avain/raskovnik

	public KeyCode talkKey;
	public GameObject otherDoor;
    public GameObject blackScreen;
    private FMOD.Studio.EventInstance ambientPiano;


    // Use this for initialization
    void Start () {
		//säädetään keybindit undying objectilla
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			Undying_Object undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.talkKey == KeyCode.None)
				talkKey = KeyCode.E;
			else
				talkKey = undyObj.talkKey;
		}else
			talkKey = KeyCode.E;
        ambientPiano = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/AmbientPiano");
    }
	
	void OnTriggerStay(Collider player) {
		//jos ovella on pelaaja
		if (player.gameObject.GetComponent<Character>() != null) {
			//ja pelaajalla on avain
			if (player.gameObject.GetComponent<Character> ().hasRaskovnik) {
				//ja pelaaja avaa oven
				if (Input.GetKeyDown(talkKey)){
					//ovenavaus cutscene ja äänet tähän
					player.transform.position = otherDoor.transform.position;
				}
			}
		}
	}

    //public void TeleportIn()
    //{
    //    StartCoroutine(RunAwayRoutine());
    //}

    //IEnumerator RunAwayRoutine()
    //{
    //    blackScreen.SetActive(true);
    //    ambientPiano.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //    runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
    //    player.transform.position = myEnemy.checkpoint.transform.position;
    //    camera.transform.position = myEnemy.checkpoint.transform.position;
    //    //TODO: fancy effects for running away
    //    yield return new WaitForSeconds(1f);
    //    gameCanvas.SetActive(false);
    //    runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(0.0f, 3.0f, false);
    //    yield return new WaitForSeconds(3f);
    //    runAwayScreen.SetActive(false);
    //}
}
