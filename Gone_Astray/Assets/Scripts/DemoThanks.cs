using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoThanks : MonoBehaviour {

    public GameObject blackCanvas;
    public Text text;

    private void OnTriggerEnter(Collider player) {
        if (player.GetComponent<Character>() != null){
            player.GetComponent<MovementControls>().stop = true;
            text.text = "Thank you for playing the Sestra: Gone Astray Demo! Press P to go back to menu.";
            blackCanvas.SetActive(true);
        }
    }
}
