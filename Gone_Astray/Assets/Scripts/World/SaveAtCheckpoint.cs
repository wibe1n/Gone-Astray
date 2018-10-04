using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAtCheckpoint : MonoBehaviour {

	public Character chara;

	void OnTriggerEnter(Collider player){
		SaveGame.Instance.playerPosition = transform.position;
		SaveGame.Instance.fireflies = chara.myFireflies;
		SaveGame.Instance.fiaFamily = chara.fiaFamily;
		//todo: levelin tila, collectablet, mätkityt vihut, sidequestit, itemit
		SaveGame.Save ();
	}
}
