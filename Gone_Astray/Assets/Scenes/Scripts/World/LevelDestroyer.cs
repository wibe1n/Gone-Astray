using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDestroyer : MonoBehaviour {

    public GameObject previousLevel;

    private void OnTriggerEnter(Collider player) {
        if(player.GetComponent<Character>() != null) {
            player.GetComponent<Character>().level++;
        }
        Destroy(previousLevel);
        
    }
}
