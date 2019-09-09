using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionRay : MonoBehaviour {

    public Camera mainCamera;
    public GameObject player;
    Vector3 playerPos;
    Vector3 topRight;
    Vector3 topLeft;
    Vector3 bottomRight;
    Vector3 bottomLeft;
    RaycastHit hittr;
    RaycastHit hittl;
    RaycastHit hitbr;
    RaycastHit hitbl;

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
        //check if there's anything between camera view corners and player
        topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        playerPos = player.transform.position;
        if (Physics.Raycast(topRight, playerPos - topRight, out hittr, Mathf.Ceil(Vector3.Distance(playerPos, topRight))))
        {
            if (!hittr.rigidbody)
                Debug.Log("top right hit");
        }
        if (Physics.Raycast(topLeft, playerPos - topLeft, out hittl, Mathf.Ceil(Vector3.Distance(playerPos, topLeft))))
        {
            if(!hittl.rigidbody)
                Debug.Log("top left hit");
        }
        if (Physics.Raycast(bottomRight, playerPos - bottomRight, out hitbr, Mathf.Ceil(Vector3.Distance(playerPos, bottomRight))))
        {
            if (!hitbr.rigidbody)
                Debug.Log("bottom right hit");
        }
        if (Physics.Raycast(bottomLeft, playerPos - bottomLeft, out hitbl, Mathf.Ceil(Vector3.Distance(playerPos, bottomLeft))))
        {
            if (!hitbl.rigidbody)
                Debug.Log("bottom right hit");
        }
    }
}

