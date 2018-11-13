using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int disturbTreshold;
    public GameObject checkpoint;
	public bool isBoss;
    public bool isTutorial;
    private List<Firefly> availableFireflies = new List<Firefly> { };

    void OnTriggerEnter(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<MovementControls>().stop = true;
            availableFireflies = player.gameObject.GetComponent<Character>().myFireflies;
            Encounter();
        }
        
	}
	void OnTriggerExit(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<MovementControls>().stop = false;
        }
    }

    private void Encounter() {
        StartCoroutine(StartEncounterIenum(this));
    }

    private IEnumerator StartEncounterIenum(Enemy enemy) {
        EncounterController enCon = GameObject.FindGameObjectWithTag("EncounterController").GetComponent<EncounterController>();
        enCon.StartEncounter(enemy, availableFireflies);
        yield return new WaitForSeconds(1);
    }
}
