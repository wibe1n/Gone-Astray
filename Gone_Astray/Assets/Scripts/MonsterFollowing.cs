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
    public bool monsterFaced, checking, turningRight;
    // Use this for initialization
    void Start () {
        checking = true;
        monsterFaced = false;
        maxDistance = 30;
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
                if (!turningRight && lastShadow != null) {
                    Debug.Log("oikealla Täällä");
                    if (!lastShadow.GetComponent<Renderer>().isVisible) {
                        Destroy(lastShadow);
                    } 
                }
                turningRight = true;
                rotY = transform.eulerAngles.y - startingAngle;
                if (rotY >= 90 && monsterFaced == false) {
                    if (lastShadow == null) {
                        Debug.Log("90 astetta oikea");
                        lastShadow = Instantiate(monsterShadow, monsterPosition, monsterShadow.transform.rotation);
                        monsterFaced = true;
                    }
                    else {
                        Destroy(lastShadow);
                    }
                }
                else if (rotY >= 180) {
                    Debug.Log("180 astetta oikea");
                    Destroy(lastShadow);
                    checking = true;
                    monsterFaced = false;
                }
            }
            else if (transform.eulerAngles.y < startingAngle && checking == false) {
                if (turningRight && lastShadow != null) {
                    Debug.Log("vasemmalla Täällä");
                    if (!lastShadow.GetComponent<Renderer>().isVisible) {
                        Destroy(lastShadow);     
                    }
                }
                turningRight = false;
                rotY = transform.eulerAngles.y - startingAngle;
                if (rotY <= -90 && monsterFaced == false) {
                    if (lastShadow == null) {
                        Debug.Log("90 astetta vasen");
                        lastShadow = Instantiate(monsterShadow, monsterPosition, monsterShadow.transform.rotation);
                        monsterFaced = true;
                        }
                    else {
                        Destroy(lastShadow);
                    }
                }
                else if (rotY <= -180) {
                    Debug.Log("180 astetta vasen");
                    Destroy(lastShadow);
                    checking = true;
                    monsterFaced = false;
                    
                }
            }

        }

    }
    
}
