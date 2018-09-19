using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	int maxSteps;
	int minSteps;
	public Character chara;
	[HideInInspector]
	int stepsNeeded;
	private int fireflies;

	// Use this for initialization
	void Start () {
		stepsNeeded = Random.Range(minSteps, maxSteps);
		//firefly path light animation here
		fireflies = chara.myFireflies.Count;
		stepsNeeded = stepsNeeded -fireflies;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void deleteLater(){ //tää on vain täällä et noi warning arvo määritetty mut ei käytetty jutut lähtis vekee
		maxSteps =0;
		minSteps = 0;
	}
}
