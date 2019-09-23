﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionRay : MonoBehaviour {

    public Camera mainCamera;
    public GameObject player;
    RaycastHit hit;
    public float rayLenght = 1;
    public float zoomAmount = 0.1f;
    Vector3 startCamPos;
    Vector3 topRight;
    Vector3 topLeft;
    Vector3 bottomRight;
    Vector3 bottomLeft;
    RaycastHit hittr;
    RaycastHit hittl;
    RaycastHit hitbr;
    RaycastHit hitbl;
    float vecAngle;
    float moveLength;

    //Kameran etäisyys pelaajasta

    public float cameraDistance = 2;

    private void Awake() {
        mainCamera = Camera.main;
        startCamPos = mainCamera.transform.position;
    }



    // Update is called once per frame
    void Update() {

        // this example shows the different camera frustums when using asymmetric projection matrices (like those used by OpenVR).
        
        Vector3[] frustumCorners = new Vector3[4];
        mainCamera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), mainCamera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        for (int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = mainCamera.transform.TransformVector(frustumCorners[i]);
            Debug.DrawRay(mainCamera.transform.position, worldSpaceCorner, Color.cyan);

            if (Physics.Raycast(transform.position, worldSpaceCorner, out hit, rayLenght))
            {
                Debug.DrawRay(transform.position, worldSpaceCorner * rayLenght, Color.yellow);
                if (!hit.rigidbody)
                    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, player.transform.position, zoomAmount);
            }else {
                mainCamera.transform.position = startCamPos;
            }
        }



        //RaycastHit hit;
        ////Ammu raycast taaksepäin, jos osuu niin kameran sijainti vaihtuu osumakohdan sijainniksi
        //if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Ceil(cameraDistance)))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * cameraDistance, Color.yellow);
        //    if (!hit.rigidbody)
        //        mainCamera.transform.position = hit.point;
        //}

        //else
        //{
        //    mainCamera.transform.position = transform.position + -transform.forward * cameraDistance;
        //}
        ////check if there's anything between camera view corners and player
        //topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        //bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        //topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        //bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        //if (Physics.Raycast(bottomRight, player.transform.position - bottomRight, out hitbr, Mathf.Ceil(Vector3.Distance(player.transform.position, bottomRight))))
        //{
        //    Debug.DrawRay(bottomRight, player.transform.position - bottomRight, Color.magenta);
        //    if (!hitbr.rigidbody)
        //    {
        //        Debug.Log("bottom right hit");
        //        vecAngle = Vector3.Angle((player.transform.position - bottomRight), (player.transform.position - mainCamera.transform.position));
        //        moveLength = hitbr.distance * vecAngle;
        //        mainCamera.transform.position = (player.transform.position - mainCamera.transform.position).normalized * moveLength;
        //    }
        //}
        //if (Physics.Raycast(bottomLeft, player.transform.position - bottomLeft, out hitbl, Mathf.Ceil(Vector3.Distance(player.transform.position, bottomLeft))))
        //{
        //    Debug.DrawRay(bottomLeft, player.transform.position - bottomLeft, Color.cyan);
        //    if (!hitbl.rigidbody)
        //    {
        //        Debug.Log("bottom right hit");
        //        vecAngle = Vector3.Angle((player.transform.position - bottomLeft), (player.transform.position - mainCamera.transform.position));
        //        moveLength = hitbl.distance * vecAngle;
        //        mainCamera.transform.position = (player.transform.position - mainCamera.transform.position).normalized * moveLength;
        //    }
        //}
        //if (Physics.Raycast(topRight, player.transform.position - topRight, out hittr, Mathf.Ceil(Vector3.Distance(player.transform.position, topRight))))
        //{
        //    Debug.DrawRay(topRight, player.transform.position - topRight, Color.green);
        //    if (!hittr.rigidbody)
        //    {
        //        Debug.Log("top right hit");
        //        vecAngle = Vector3.Angle((player.transform.position - topRight),(player.transform.position - mainCamera.transform.position));
        //        moveLength = hittr.distance* vecAngle;
        //        mainCamera.transform.position = (player.transform.position - mainCamera.transform.position).normalized * moveLength;
        //    }
        //}
        //if (Physics.Raycast(topLeft, player.transform.position - topLeft, out hittl, Mathf.Ceil(Vector3.Distance(player.transform.position, topLeft))))
        //{
        //    Debug.DrawRay(topLeft, player.transform.position - topLeft, Color.yellow);
        //    if (!hittl.rigidbody)
        //    {
        //        Debug.Log("top left hit");
        //        vecAngle = Vector3.Angle((player.transform.position - topLeft), (player.transform.position - mainCamera.transform.position));
        //        moveLength = hittl.distance * vecAngle;
        //        mainCamera.transform.position = (player.transform.position - mainCamera.transform.position).normalized * moveLength;
        //    }
        //}

    }
}

