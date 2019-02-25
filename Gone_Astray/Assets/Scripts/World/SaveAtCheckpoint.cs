using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAtCheckpoint : MonoBehaviour {
	
    //Pelaajan tullessä checkpointtiin, tallennetaan kameran ja pelaajan positio, tulikärpästen määrä ja mikä levelin osa on instantioitu
	void OnTriggerEnter(Collider player){
        if (player.GetComponent<Character>() != null) {
            SaveGame.Instance.cameraPosition = transform.position;
            SaveGame.Instance.playerPosition = transform.position;
            SaveGame.Instance.fireflies = player.GetComponent<Character>().myFireflies;
            SaveGame.Instance.fiaFamily = player.GetComponent<Character>().fiaFamily;
            SaveGame.Instance.level = player.GetComponent<Character>().level;
			SaveGame.Instance.speechInstance = player.GetComponent<Character>().NPCspeechInstance;
            //todo: levelin tila, collectablet, mätkityt vihut, sidequestit, itemit
            SaveGame.Save();
            Debug.Log(SaveGame.Instance.level);
        }
		
	}
}
