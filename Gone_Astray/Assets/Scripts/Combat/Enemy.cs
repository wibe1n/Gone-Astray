using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int disturbTreshold;
    public GameObject checkpoint;
	public bool isBoss;

	void OnTriggerEnter(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<Character>().enemyIsNear = true;
            player.gameObject.GetComponent<Character>().myEnemy = this;
        }
        
	}
	void OnTriggerExit(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<Character>().enemyIsNear = false;
            player.gameObject.GetComponent<Character>().myEnemy = null;
        }
    }
}
