using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionRay : MonoBehaviour {

    public Camera mainCamera;
    Vector3 topRight;
    Vector3 topLeft;
    Vector3 bottomRight;
    Vector3 bottomLeft;

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
        topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        
    }
}
