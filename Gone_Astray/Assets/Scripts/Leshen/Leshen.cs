using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leshen : MonoBehaviour {

	//tämä scripti tyhjälle leshen controllerille

	public bool atArea;					//onko pelaaja trigger arealla
	public GameObject grownLeshen;		//varsinainen leshen joka kasvaa oikeassa paikassa
	public GameObject sapling;			//säälittävä pikku taimi joka leshenistä kasvaa random paikassa
	public GameObject lastLeshen;		//scenessä jo valmiiksi actiivinen leshen
	public GameObject saplingLocation; //tyhjä objecti joka on pelaajan lapsi ja sijaitsee pelaajan edessä
	public GameObject leshenLocation;	//tyhjä objecti trigger arean vieressä joka saadaan leshenTriggerArea scriptistä
	public bool leshenActive = false;
	//LeshenLerp despawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("7") && !(leshenActive && atArea)) { //&& !(grownLeshen.activeSelf && atArea)
			ActivateLeshen ();
		}
	}
	public void ActivateLeshen(){
		//lastLeshen = GameObject.FindGameObjectWithTag ("Leshen");
		if (!(lastLeshen == null)) { 			//vain jos scenessä on jo leshen
			//despawn = lastLeshen.GetComponent<LeshenLerp> ();
			//despawn.DespawnLerp ();
			Destroy(lastLeshen);
		}
		if (atArea) {
			lastLeshen = Instantiate (grownLeshen, leshenLocation.transform.position, gameObject.transform.rotation);
			leshenActive = true;
		} else {
			lastLeshen = Instantiate (sapling, saplingLocation.transform.position, gameObject.transform.rotation);
			leshenActive = false;
		}	//taimella on scripti joka tuhoaa sen jos setActive on false
			
	}

}
