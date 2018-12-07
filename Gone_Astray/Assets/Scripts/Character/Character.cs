using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public GameObject rayCastDrawer, lastSapling, sapling;
    public PauseMenuController _pauseController;
    public ParticleSystem fireflies;
    public bool bossIsNear = false;
    public bool spooped = false;
    public bool leshenObjectNear = false;
    public int level = 0;
    public List<Firefly> myFireflies = new List<Firefly> { };
    public float stressLevel = 0;
    public List<bool> items = new List<bool> { };
    public List<Firefly> fiaFamily = new List<Firefly> { };
    float dist = 10;
	public Undying_Object undyObj;
	public KeyCode leshenKey = KeyCode.None;

    void Start () {
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.leshenKey == KeyCode.None)
				leshenKey = KeyCode.L;
			else
				leshenKey = undyObj.leshenKey;
		}else
			leshenKey = KeyCode.L;
    }
	
	void Update () {
		if (!leshenObjectNear) {
			if (Input.GetKeyDown (leshenKey)) {
				RaycastHit objectHit;
				Vector3 down = rayCastDrawer.transform.TransformDirection (Vector3.down);
				Physics.Raycast (rayCastDrawer.transform.position, down, out objectHit, dist);
				if (!(lastSapling == null)) {
					Destroy(lastSapling);
				}
				lastSapling = Instantiate (sapling, objectHit.point, sapling.transform.rotation);
			}
		} else {
			if (!(lastSapling == null) && Input.GetKeyDown (leshenKey)) {
				Destroy (lastSapling);
			}
		}

    }

    public void AddFirefly() {
        Firefly firefly = new Firefly(0);
        myFireflies.Add(firefly);
        
        
        ParticleSystem.MainModule system = fireflies.main;
        system.maxParticles += 1;
    }


}
