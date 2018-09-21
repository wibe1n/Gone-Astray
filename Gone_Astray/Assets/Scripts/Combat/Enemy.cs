﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int disturbTreshold;
	public BlackJack blackjack;

	void OnTriggerEnter(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<Character>().enemyIsNear = true;
            player.gameObject.GetComponent<Character>().myEnemy = this;
			blackjack.enemy = this;
			blackjack.active = true;
        }
        
	}
	void OnTriggerExit(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<Character>().enemyIsNear = false;
            player.gameObject.GetComponent<Character>().myEnemy = null;
			blackjack.active = false;
			blackjack.enemy = null;
        }
    }
}
