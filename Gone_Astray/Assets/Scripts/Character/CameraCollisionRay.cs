using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionRay : MonoBehaviour {

    public Camera mainCamera;
    public GameObject player;

    //Kameran etäisyys pelaajasta

    public float cameraDistance = 2;
    //public float cameraHeight = 0.2f;

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
        //RaycastHit hit2;
        ////Ammu raycast taaksepäin, jos osuu niin kameran sijainti vaihtuu osumakohdan sijainniksi
        //if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit2, cameraHeight))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * cameraHeight, Color.cyan);
        //    if (!hit2.rigidbody)
        //        mainCamera.transform.position = hit2.point;
        //}
        //else
        //{
        //    mainCamera.transform.position = transform.position + -transform.up * cameraHeight;
        //}

    }
}

