﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameObject Canvas; // drag from hierarchy
	public bool isCollectable;
	public bool isFirefly;
	public Character chara;

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
            player.gameObject.GetComponent<Character>().interactableNear = true;
        }
		if (Input.GetKeyDown ("4") && isCollectable) {
			if (isFirefly) {
				Firefly firefly = new Firefly();
				chara.myFireflies.Add (firefly);
			}
			gameObject.SetActive = false;
		}
        
    }
    void OnTriggerExit(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
            player.gameObject.GetComponent<Character>().interactableNear = false;
        }
        
    }
}
