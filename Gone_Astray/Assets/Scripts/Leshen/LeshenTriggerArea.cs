using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeshenTriggerArea : MonoBehaviour {

	//tämä scripti sieniympyrälle johon leshen kuuluu istuttaa

	public Leshen leshen;
	public GameObject spawnPosition;

	void OnTriggerEnter(Collider player){
		leshen.atArea = true;
		leshen.leshenLocation = spawnPosition;

	}
	void OnTriggerExit(Collider player){
		leshen.atArea = false;
		leshen.leshenLocation = null;
	}
}
