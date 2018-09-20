using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionRay : MonoBehaviour {

    public Camera mainCamera;


    public float cameraDistance = 3;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Ceil(cameraDistance)))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * cameraDistance, Color.yellow);
            Debug.Log("Did Hit");

            mainCamera.transform.position = hit.point;
        }
        
        else
        {
            mainCamera.transform.position = transform.position + -transform.forward * cameraDistance;
        }

    }
}
