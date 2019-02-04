using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButtonsDefaultCheck : MonoBehaviour {

	//varmistaa että options menun keybind napeissa lukee oikea keycode
	//kiinni keybind nappien text object lapsissa

	public int whichButton;
	public Undying_Object undyObj;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
	
			switch (whichButton) {
			case 0:
				gameObject.GetComponent<Text> ().text = undyObj.talkKey.ToString ();
				break;
			case 1:
				gameObject.GetComponent<Text> ().text = undyObj.crouchKey.ToString ();
				break;
			case 2:
				gameObject.GetComponent<Text> ().text = undyObj.leshenKey.ToString ();
				break;
			case 3:
				gameObject.GetComponent<Text> ().text = undyObj.pauseKey.ToString ();
				break;
			case 4:
				gameObject.GetComponent<Text> ().text = undyObj.altPauseKey.ToString ();
				break;
			case 5:
				gameObject.GetComponent<Text> ().text = undyObj.journalKey.ToString ();
				break;
			case 6:
				gameObject.GetComponent<Text> ().text = undyObj.jumpKey.ToString ();
				break;
			default:
				Debug.Log ("plöö");
				break;
			}
		}
	}
}
