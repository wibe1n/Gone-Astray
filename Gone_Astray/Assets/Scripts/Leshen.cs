using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leshen : MonoBehaviour {

	bool atArea;
	public GameObject grownLeshen;
	public GameObject sapling;
	public GameObject lastLeshen;
	public GameObject saplingLocation; //tyhjä objecti joka on pelaajan lapsi ja sijaitsee pelaajan edessä

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("7")) {
			ActivateLeshen ();
		}
	}
	public void ActivateLeshen(){
		lastLeshen = GameObject.FindGameObjectWithTag ("Leshen");
		if(!(lastLeshen == null))
			lastLeshen.SetActive (false);
		if (atArea) {
			grownLeshen.SetActive (true);
		} else {
			Instantiate (sapling, saplingLocation.transform.position, gameObject.transform.rotation);
		}
			
	}

	void OnTriggerEnter(Collider player){
		atArea = true;

	}
	void OnTriggerExit(Collider player){
		atArea = false;
	}
}
