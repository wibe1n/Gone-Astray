using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJack : MonoBehaviour {

	//vaihda omat muuttujat muilta scripteiltä saatuihin muuttujiin
	public int difficulty;
	public int firefliesUsed;
	private int treshold = 21;
	bool disturbed = false;
	bool success = false;
	private int firefliesNeeded;

	// Use this for initialization
	void Start () {
		treshold = treshold + difficulty;
		firefliesNeeded = Random.Range (0, treshold);
	}
	
	// Update is called once per frame
	void Update () {
		if (firefliesUsed > treshold) {
			disturbed = true;
		}
		if (firefliesUsed >= firefliesNeeded) {
			success = true;
		}
	}

	void deleteLater(){ //tää on vain täällä et noi warning arvo määritetty mut ei käytetty jutut lähtis vekee
		if (disturbed && success) {
			difficulty = 0;
		}
	}
}
