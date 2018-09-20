using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public SpeechBubbleCreator speechCreator;
    public bool interactableNear = false;
    public bool enemyIsNear = false;
    public bool bossIsNear = false;
    public bool npcIsNear = false;
    public bool playerInCombat = false;
    public NPC myNPC;
    public Enemy myEnemy;
    public List<Firefly> myFireflies = new List<Firefly> { };
    public float stressLevel = 0;
    List<bool> items = new List<bool> { };
    List<Firefly> fiaFamily = new List<Firefly> { };

	void Start () {
		
	}
	
	
	void Update () {
        if (npcIsNear == true) {
            if (Input.GetKey(KeyCode.O)) {
                speechCreator.GenerateSpeechBubble(myNPC);
            }
        }

    }

    private void StartCombat() {
        StartCoroutine(StartCombatIenum(myEnemy));       
    }

    private IEnumerator StartCombatIenum(Enemy enemy) {       
        CombatController comCon = GameObject.FindGameObjectWithTag("CombatController").GetComponent<CombatController>();
        comCon.StartBlackJack(enemy, myFireflies);
        yield return new WaitForSeconds(1);
    }


}
