using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJack : MonoBehaviour {

	//vaihda omat muuttujat muilta scripteiltä saatuihin muuttujiin
	public Enemy enemy;
	//public int difficulty;
	public int firefliesUsed = 0;
	//private int treshold = 21;
	public bool disturbed = false;
	public bool success = false;
	private int firefliesNeeded;
	public bool active = false;
	bool firefliesCalculated = false;

	// Use this for initialization
	void Start () {
		//treshold = treshold + difficulty;

		//

	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			if (!firefliesCalculated) {
				firefliesNeeded = Random.Range (0, enemy.disturbTreshold);
				firefliesCalculated = true;
			}
			if (firefliesUsed > enemy.disturbTreshold) {
				disturbed = true;
			}
			if (firefliesUsed >= firefliesNeeded) {
				success = true;
			}
			if (Input.GetKeyDown ("3")) {
				firefliesUsed++;
			}
		} else {
			disturbed = false;
			success = false;
			firefliesCalculated = false;
			firefliesUsed = 0;
		}
	}

	void deleteLater(){ //tää on vain täällä et noi warning arvo määritetty mut ei käytetty jutut lähtis vekee
		if (disturbed && success) {
			//plääplöö
		}
	}
}
