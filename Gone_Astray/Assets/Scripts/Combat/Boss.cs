using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int maxSteps;
	public int minSteps;
	public int maxStepLenght;
	public int stepsLeft;
	public int stepLenght =0;
	public int totalDistance =0;
	public Character chara;
	public Enemy thisBoss;
	//[HideInInspector]
	public bool win = false;
	public bool fail = false;
	public bool bossBattleOn = false;
	public int stepsNeeded;
	private int fireflies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (chara.myEnemy.isBoss && !bossBattleOn && !win) {
			bossBattleOn = true;
			StartBossBattle ();
		}
		if (bossBattleOn) {
			BossBattleTurn ();
		}
		if (win) {
			bossBattleOn = false;
			thisBoss.gameObject.SetActive (false);
		}
		if (fail) {
			//chara.gameObject.transform.position = chara.startPosition;
			//tähän se respawnaus
		}
	}
	void StartBossBattle(){
		stepsNeeded = Random.Range(minSteps, maxSteps);
		//firefly path light animation here
		fireflies = chara.myFireflies.Count;
		stepsNeeded = stepsNeeded -fireflies;
	}

	void BossBattleTurn(){
		if (Input.GetKeyDown ("3")) {
			stepLenght++;
		}
		if (stepLenght > maxStepLenght) {
			stepLenght = maxStepLenght;
		}
		if (Input.GetKeyDown ("4")) {
			stepsLeft--;
			totalDistance = totalDistance + stepLenght;
			stepLenght = 0;
		}
		if (totalDistance > thisBoss.disturbTreshold) {
			fail = true;
		} else if (stepsNeeded <= totalDistance) {
			win = true;
		} else if (stepsLeft < 0) {
			fail = true;
		}
	}
}
