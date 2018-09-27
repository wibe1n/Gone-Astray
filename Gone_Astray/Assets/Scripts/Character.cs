using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public SpeechBubbleCreator speechCreator;
    public CombatController combatController;
    public bool interactableNear = false;
    public bool enemyIsNear = false;
    public bool bossIsNear = false;
    public bool npcIsNear = false;
    public bool talking = false;
    public bool playerInCombat = false;
    public bool inCombat = false;
    public bool proceed = false;
    public NPC myNPC;
    public Enemy myEnemy;
    public List<Firefly> myFireflies = new List<Firefly> { };
    public float stressLevel = 0;
    public List<bool> items = new List<bool> { };
    List<Firefly> fiaFamily = new List<Firefly> { };

	void Start () {
		
	}
	
	void Update () {
        if (npcIsNear == true) {
            if (Input.GetKeyDown(KeyCode.O)) {
                if(talking == true) {
                    if (myNPC.currentSpeechInstance == myNPC.maxSpeechInstance) {
                        speechCreator.CloseSpeechBubble(myNPC);
                        talking = false;
                        myNPC.Canvas.SetActive(true);
                    }
                    else {
                        speechCreator.UpdateSpeechBubble(myNPC);
                    }                   
                }
                else {
                    speechCreator.GenerateSpeechBubble(myNPC);
                    talking = true;
                }
            }
        }
        if (enemyIsNear == true && inCombat == false) {
            inCombat = true;
            Encounter();
        }

    }

    private void Encounter() {
        StartCoroutine(StartEncounterIenum(myEnemy));       
    }

    private IEnumerator StartEncounterIenum(Enemy enemy) {       
        EncounterController comCon = GameObject.FindGameObjectWithTag("EncounterController").GetComponent<EncounterController>();
        comCon.StartEncounter(enemy, myFireflies);
        yield return new WaitForSeconds(1);
    }


}
