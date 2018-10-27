using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour {

    public float rotationSpeed; // kameran nopeus kun sitä käännetään hiirellä
    public float followRotationSpeed; // smoothisti seuraavan kameran kääntönopeus
    public GameObject target; //kohde, mitä kamera seuraa
    public GameObject cameraPos;
    public Quaternion originalRotationValue;
    public float movementSpeed;
    public float backwardsSpeed;
    private bool goingBackwards;
    public Vector3 offset;

    private bool rotationLock = true;
    private float rotationDir = 1;

    // Use this for initialization
    void Start () {
        originalRotationValue = transform.rotation; // save the initial rotation

        transform.position = cameraPos.transform.position;
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate () {
        //Kamera pyrkii aina CameraPos-objektin sijaintiin, kun hiiren nappeja ei paineta
        if (Input.GetAxis("Fire1") == 0)
        {
            if ((target.transform.position - transform.position).magnitude <= (cameraPos.transform.position - target.transform.position).magnitude && transform.position != cameraPos.transform.position && (cameraPos.transform.position - transform.position).magnitude > 1)
            {
                //lyhyempi suunta cameraposiin kun kierretään hahmon ympäri
                if (Input.GetAxis("Horizontal") != 0)
                {
                    if (Input.GetAxis("Horizontal") < 0)
                        rotationDir = -1;
                    else
                        rotationDir = 1;
                    rotationLock = false;
                }
                if (rotationLock == false && (transform.position - cameraPos.transform.position).magnitude < 1)
                {
                    rotationLock = true;
                }
                if (rotationLock == false)
                {
                    transform.RotateAround(target.transform.position, Vector3.up, rotationDir * rotationSpeed * Time.deltaTime);
                }


                transform.position = new Vector3(transform.position.x, cameraPos.transform.position.y, transform.position.z);
            }
            else
            {
                if ((cameraPos.transform.position - transform.position).magnitude > 0) {                   
                        transform.Translate(0f, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed, 0f, Space.World);
                        transform.position = Vector3.MoveTowards(transform.position, cameraPos.transform.position, movementSpeed * Time.deltaTime);
                        transform.position = new Vector3(transform.position.x, cameraPos.transform.position.y, transform.position.z);                   
                }
            }
        }

        if (Input.GetAxis("Fire1") != 0)
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            if (mouseInputX != 0)
            {
                if (mouseInputX > 0)
                {
                    transform.RotateAround(cameraPos.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.RotateAround(cameraPos.transform.position, Vector3.up, -1 * rotationSpeed * Time.deltaTime);
                }
                

            }

            if (mouseInputY != 0)
            {
                Vector3 lookHereY = new Vector3(-1 * mouseInputY * rotationSpeed * Time.deltaTime, 0, 0);
                transform.Rotate(lookHereY);
            }
        }
        else if (Input.GetAxis("Fire2") != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationSpeed);
        }
        else
        {
            if (Input.GetAxis("Vertical") < 0)
            {

            }
            else
            {
                Vector3 newRotation = target.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(newRotation);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followRotationSpeed * Time.deltaTime);
            }
        }


    }
}
