using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	//tämä scripti teleporttaa pelaajan määrättyyn sijaintiin scenessä jos pelaajalla on avain/raskovnik

	public KeyCode talkKey;
	public GameObject otherDoor;


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
	}
	
	void OnTriggerStay(Collider player) {
		Debug.Log ("ff");
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
}
