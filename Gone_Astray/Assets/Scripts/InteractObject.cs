using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameObject Canvas; // drag from hierarchy

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
            player.gameObject.GetComponent<Character>().interactableNear = true;
        }
        
    }
    void OnTriggerExit(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
            player.gameObject.GetComponent<Character>().interactableNear = false;
        }
        
    }
}
