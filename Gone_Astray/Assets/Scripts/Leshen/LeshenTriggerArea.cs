using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeshenTriggerArea : MonoBehaviour {

	//tämä scripti sieniympyrälle johon leshen kuuluu istuttaa

	public Leshen leshen;

	void OnTriggerEnter(Collider player){
		leshen.atArea = true;

	}
	void OnTriggerExit(Collider player){
		leshen.atArea = false;
	}
}
