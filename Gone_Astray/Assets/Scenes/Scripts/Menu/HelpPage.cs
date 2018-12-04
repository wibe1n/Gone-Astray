using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPage : MonoBehaviour {

	public Undying_Object undyObj;
	bool isUndyObjfound;
	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			isUndyObjfound = true;
		}else
			isUndyObjfound = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isUndyObjfound)
			gameObject.GetComponent<Text>().text = "talk : "+ undyObj.talkKey +"\ncrouch : "+ undyObj.crouchKey +"\nleshen : "+ undyObj.leshenKey +"\npause : "+ undyObj.pauseKey +" or "+ undyObj.altPauseKey +" \njump : " + undyObj.jumpKey;
		else
			gameObject.GetComponent<Text>().text = "talk : O\ncrouch : C\nleshen L\npause : P or esc \njump : space";
	}
}
