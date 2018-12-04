using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAtCheckpoint : MonoBehaviour {

	

	void OnTriggerEnter(Collider player){
        if (player.GetComponent<Character>() != null) {
            SaveGame.Instance.cameraPosition = transform.position;
            SaveGame.Instance.playerPosition = transform.position;
            SaveGame.Instance.fireflies = player.GetComponent<Character>().myFireflies;
            SaveGame.Instance.fiaFamily = player.GetComponent<Character>().fiaFamily;
            SaveGame.Instance.level = player.GetComponent<Character>().level;
            //todo: levelin tila, collectablet, mätkityt vihut, sidequestit, itemit
            SaveGame.Save();
            Debug.Log(SaveGame.Instance.level);
        }
		
	}
}
