using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogSpooked : MonoBehaviour {
	//lähettää siilin liikkeelle pelaajan lähestyessä

	void OnTriggerEnter(Collider other){
        if(gameObject.GetComponent<MoveToWaypoints>().current < gameObject.GetComponent<MoveToWaypoints>().waypoints.Length)
		gameObject.GetComponent<MoveToWaypoints> ().proceed = true;
	}
	void OnTriggerExit(Collider other){
		gameObject.GetComponent<MoveToWaypoints> ().proceed = false;
		transform.LookAt (other.transform);

	}
}
