using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [HideInInspector]
    public bool interactableNear = false;
    public bool enemyIsNear = false;
    public bool bossIsNear = false;
    public bool playerInCombat = false;
    public Enemy myEnemy;
    List<Firefly> myFireflies = new List<Firefly> { };
    public float stressLevel = 0;
    List<bool> items = new List<bool> { };

	void Start () {
		
	}
	
	
	void Update () {
        if (playerInCombat == false) {
            //movement and controls here
        }
        else {
            //Blackjack controls here
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
