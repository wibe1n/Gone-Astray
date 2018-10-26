using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public GameObject rayCastDrawer, lastSapling, sapling;
    public PauseMenuController _pauseController;
    public bool enemyIsNear = false;
    public bool bossIsNear = false;
    public bool playerInCombat = false;
    public bool inCombat = false;
    public bool proceed = false;
    public bool leshenObjectNear = false;
    public int level = 0;
    public Enemy myEnemy;
    public List<Firefly> myFireflies = new List<Firefly> { };
    public float stressLevel = 0;
    public List<bool> items = new List<bool> { };
    public List<Firefly> fiaFamily = new List<Firefly> { };
    float dist = 10;
    

    void Start () {
        
    }
	
	void Update () {
		if (!leshenObjectNear) {
			if (Input.GetKeyDown ("7")) {
				RaycastHit objectHit;
				Vector3 down = rayCastDrawer.transform.TransformDirection (Vector3.down);
				Physics.Raycast (rayCastDrawer.transform.position, down, out objectHit, dist);
				if (!(lastSapling == null)) {

					Destroy (lastSapling);
				}
				lastSapling = Instantiate (sapling, objectHit.point, Quaternion.identity);
			}
		} else {
			if (!(lastSapling == null) && Input.GetKeyDown ("7")) {
				Destroy (lastSapling);
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
