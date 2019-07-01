using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MonsterFollowing : MonoBehaviour {
    private Transform characterPosition;
    private Quaternion characterRotation;
    public GameObject monsterShadow;
    public GameObject lastShadow, secondLastShadow;
    public Vector3 monsterPosition;
    float rotY;
    public float startingAngle;
    public float maxDistance;
    private bool monsterFaced, checking;
    // Use this for initialization
    void Start () {
        checking = true;
        monsterFaced = false;
        maxDistance = 10;
	}

    private void FixedUpdate() {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        if (true) {
            RaycastHit hit;
            if (h != 0 && checking) {
                checking = false;              
                Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit,  maxDistance);
                monsterPosition = transform.position - transform.forward * maxDistance;
                monsterPosition.y += 3.5f;
                characterPosition = transform;
                startingAngle = characterPosition.eulerAngles.y;
                Debug.Log(hit.collider);
            }
            Debug.Log(rotY);
            if (transform.eulerAngles.y > startingAngle && checking == false) {
                rotY = transform.eulerAngles.y - startingAngle;
                if (rotY >= 90) {
                    if (monsterFaced == false) {
                        if (lastShadow != null) {
                            Destroy(secondLastShadow);
                        }
                        lastShadow = Instantiate(monsterShadow, monsterPosition, monsterShadow.transform.rotation);
                        monsterFaced = true;
                    }
                }
                if (rotY >= 180) {
                    secondLastShadow = lastShadow;
                    lastShadow = Instantiate(monsterShadow, monsterPosition, monsterShadow.transform.rotation);
                    monsterFaced = false;
                    checking = true;
                }

            }
            else if (transform.eulerAngles.y < startingAngle && checking == false) {
                rotY = transform.eulerAngles.y - startingAngle;
                if (rotY <= -90) {
                    if(monsterFaced == false) {
                        if (lastShadow != null) {
                            Destroy(secondLastShadow);
                        }
                        lastShadow = Instantiate(monsterShadow, monsterPosition, monsterShadow.transform.rotation);
                        monsterFaced = true;
                    }
                }
                if (rotY <= -180) {
                    secondLastShadow = lastShadow;
                    lastShadow = Instantiate(monsterShadow, monsterPosition, monsterShadow.transform.rotation);
                    monsterFaced = false;
                    checking = true;
                }
            }

        }

    }
    
}
