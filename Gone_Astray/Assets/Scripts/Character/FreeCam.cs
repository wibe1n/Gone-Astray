using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float rotationSpeed; // kameran nopeus kun sitä käännetään hiirellä
    public float followRotationSpeed; // smoothisti seuraavan kameran kääntönopeus
    public GameObject player; //kohde, mitä kamera seuraa
    public GameObject target; //kohde, mitä kamera katsoo
    public GameObject cameraPos;
    public Quaternion originalRotationValue;
    public float movementSpeed;
    public float backwardsSpeed;
    private bool goingBackwards;
    public Vector3 offset;
    public bool combatLock = false;
    private float rotationDir = 1;

    float yRotate;
    float xRotate;
    public float MaxAngleY = 100f;
    public float MinAngleY = -35f;

    // Use this for initialization
    void Start()
    {
        originalRotationValue = transform.rotation; // save the initial rotation
        ResetCameraPos();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yRotate += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        yRotate = Mathf.Clamp(yRotate, MinAngleY, MaxAngleY);
        xRotate += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        //xRotate = Mathf.Clamp (xRotate, -MaxAnglex, MaxAnglex);
        transform.eulerAngles = new Vector3(yRotate, xRotate, 0.0f);

        //Kamera seuraa pelaajaa
        if ((cameraPos.transform.position - transform.position).magnitude > 0)
        {
            transform.Translate(0f, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed, 0f, Space.World);
            transform.position = Vector3.MoveTowards(transform.position, cameraPos.transform.position, movementSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, cameraPos.transform.position.y, transform.position.z);
        }
    }

    public void ResetCameraPos()
    {
        transform.position = cameraPos.transform.position;
        transform.LookAt(target.transform);
        Debug.Log("Alkup " + transform.position);
    }
}
