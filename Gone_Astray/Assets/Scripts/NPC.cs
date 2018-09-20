using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public int id;
    public GameObject Canvas; // drag from hierarchy
    public int currentSpeechInstance, maxSpeechInstance;
    private void Start() {
        id = 5;
        currentSpeechInstance = 1;
        maxSpeechInstance = 3;
    }

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
            player.gameObject.GetComponent<Character>().npcIsNear = true;
            player.gameObject.GetComponent<Character>().myNPC = this;
        }

    }
    void OnTriggerExit(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
            player.gameObject.GetComponent<Character>().npcIsNear = false;
            player.gameObject.GetComponent<Character>().myNPC = null;
        }

    }
}
