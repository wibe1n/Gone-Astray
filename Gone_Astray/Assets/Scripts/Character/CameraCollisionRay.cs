using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionRay : MonoBehaviour {

    public Camera mainCamera;

    //Kameran etäisyys pelaajasta

    public float cameraDistance = 2;

    private void Awake() {
        mainCamera = Camera.main;
    }

 

    // Update is called once per frame
    void Update() {

        RaycastHit hit;
        //Ammu raycast taaksepäin, jos osuu niin kameran sijainti vaihtuu osumakohdan sijainniksi
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Ceil(cameraDistance)))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * cameraDistance, Color.yellow);
            if (!hit.rigidbody)
            mainCamera.transform.position = hit.point;
        }
        
        else
        {
            mainCamera.transform.position = transform.position + -transform.forward * cameraDistance;
        }

    }
}
