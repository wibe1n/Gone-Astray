using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public CombatController combatController;
    public bool proceed;

    public void PlayersTurn() {
        StartCoroutine(PlayWaitTime());
    }


    //Players turn routine
    IEnumerator PlayWaitTime() {
        proceed = false;
        yield return new WaitUntil(() => proceed);
    }
}
