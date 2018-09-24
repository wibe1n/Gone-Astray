using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour {

    //public Camera myCam;
    public float rotationSpeed; // kameran nopeus kun sitä käännetään hiirellä
    public float followRotationSpeed; // smoothisti seuraavan kameran kääntönopeus
    public GameObject target; //kohde, mitä kamera seuraa
    public GameObject cameraPos;
    public Movement2 pelihahmoScript;
    public Quaternion originalRotationValue;


    // Use this for initialization
    void Start () {
        transform.LookAt(target.transform.position);
		Cursor.lockState = CursorLockMode.Locked;
        originalRotationValue = transform.rotation; // save the initial rotation


        target = GameObject.Find("pelihahmo");
        pelihahmoScript = target.GetComponent<Movement2>();
        cameraPos = GameObject.Find("pelihahmo/CameraPos"); //hakee pelihahmon lapsista tyhjän objektin nimeltä CameraPos
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(0f, Input.GetAxis("Vertical") * Time.deltaTime * pelihahmoScript.speed, 0f, Space.World);

        //Kamera pyrkii aina CameraPos-objektin sijaintiin, kun hiiren nappeja ei paineta
        if (Input.GetAxis("Fire1") == 0)
        {
            if ((cameraPos.transform.position - transform.position).magnitude > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, cameraPos.transform.position, pelihahmoScript.speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, cameraPos.transform.position.y, transform.position.z);
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
                    transform.RotateAround(target.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.RotateAround(target.transform.position, Vector3.up, -1 * rotationSpeed * Time.deltaTime);
                }
                transform.position = new Vector3(transform.position.x, cameraPos.transform.position.y, transform.position.z);

            }

            //Kameran kääntö ylös ja alas, näyttää jotenkin sekavalta imo??

            //if (mouseInputY != 0)
            //{
            //    Vector3 lookHereY = new Vector3(-1 * mouseInputY * rotationSpeed * Time.deltaTime, 0, 0);
            //    transform.Rotate(lookHereY);
            //}
        }
        else if (Input.GetAxis("Fire2") != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationSpeed);
        }
        else
        {
            Vector3 newRotation = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(newRotation);
            //Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, newRotation);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followRotationSpeed * Time.deltaTime);
        }

        //transform.LookAt(target.transform.position);

    }
}
