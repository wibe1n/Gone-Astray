using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTextKey : MonoBehaviour {
	
	public Undying_Object undyObj;
	public KeyCode talkKey = KeyCode.None;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.talkKey == KeyCode.None)
				talkKey = KeyCode.O;
			else
				talkKey = undyObj.talkKey;
		}else
			talkKey = KeyCode.O;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text> ().text = "Press " + talkKey;
	}
}
