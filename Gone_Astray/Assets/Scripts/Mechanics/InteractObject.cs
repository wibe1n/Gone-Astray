using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public int itemIndex;
    public GameObject Canvas; // drag from hierarchy
	public bool isCollectable;
	public bool isFirefly;

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
        }       
    }

    private void OnTriggerStay(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            if (Input.GetKeyDown("4") && isCollectable) {
                if (isFirefly) {
                    Firefly firefly = new Firefly(0);
                    player.GetComponent<Character>().myFireflies.Add(firefly);
                }
                else {
                    player.GetComponent<Character>().items[itemIndex] = true;
                }
                Destroy(gameObject);
            }
        }
        
    }

    void OnTriggerExit(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
        }
        
    }
}
