using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogSpooked : MonoBehaviour {
	//lähettää siilin liikkeelle pelaajan lähestyessä

	void OnTriggerEnter(Collider other){
		gameObject.GetComponent<MoveToWaypoints> ().proceed = true;
		gameObject.GetComponent<BoxCollider> ().enabled = false;
	}
}
