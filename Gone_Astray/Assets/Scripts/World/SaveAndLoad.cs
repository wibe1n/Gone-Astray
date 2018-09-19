﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	//lisää tallennettavat muuttujat ennen savea ja instansioi ne loadin jälkeen
	void Update () {
		if (Input.GetKeyDown ("1")) {
			SaveGame.Instance.playerPosition = transform.position;
			SaveGame.Save ();
		}
		if (Input.GetKeyDown("2")){
			SaveGame.Load ();
			transform.position = SaveGame.Instance.playerPosition;
		}
	}
}
