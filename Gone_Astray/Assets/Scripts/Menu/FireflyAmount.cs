﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireflyAmount : MonoBehaviour {

	public Character chara;

	//käynnistetään PauseMenuControllissa
	public void UpdateFireflies () {
		gameObject.GetComponent<Text> ().text = "Fireflies: " + chara.myFireflies.Count;
	}
}
