using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTextKey : MonoBehaviour {
	
	public Undying_Object undyObj;
	public KeyCode talkKey = KeyCode.None;
    public bool isLeshenIcon = false;


    
	// Use this for initialization
	void Start () {
        if (isLeshenIcon)
        {
            if (GameObject.FindGameObjectWithTag("UndyingObject") != null)
            {
                undyObj = GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>();
                if (undyObj.leshenKey == KeyCode.None)
                    talkKey = KeyCode.L;
                else
                    talkKey = undyObj.leshenKey;
            }
            else
                talkKey = KeyCode.L;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("UndyingObject") != null)
            {
                undyObj = GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>();
                if (undyObj.talkKey == KeyCode.None)
                    talkKey = KeyCode.E;
                else
                    talkKey = undyObj.talkKey;
            }
            else
                talkKey = KeyCode.E;
        }
		gameObject.GetComponent<Text> ().text = "Press " + talkKey;
	}
}
