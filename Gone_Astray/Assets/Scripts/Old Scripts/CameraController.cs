using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//public Camera myCam;
	public float rotationSpeed; // kameran käännön nopeus
	public Quaternion originalRotationValue; //tallentaa kameran default rotaation
	public float MaxAngle = 100f;
	public float MinAngle = -35f;
	float mouseInput;
	float yRotate;
	public float MaxAngleY = 100f;
	public float MinAngleY = -35f;
	public float MaxAnglex = 110f;
	float xRotate;


	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked; //piilottaa kursorin pelin aikana (inspectorissa kursorin saa takaisin escillä)
		originalRotationValue = transform.rotation; // save the initial rotation
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Fire1") != 0) { //kääntää kameraa hiirellä jos hiiri klickattu
			// Käännetään pelaajaa hiiren liikkeeellä
			yRotate += Input.GetAxis ("Mouse Y") * rotationSpeed * Time.deltaTime;
			yRotate = Mathf.Clamp (yRotate, MinAngleY, MaxAngleY);
			xRotate += Input.GetAxis ("Mouse X") * rotationSpeed * Time.deltaTime;
			//xRotate = Mathf.Clamp (xRotate, -MaxAnglex, MaxAnglex);
			transform.eulerAngles = new Vector3 (yRotate, xRotate, 0.0f);
		}

			//float mouseInput = Input.GetAxis ("Mouse X");
			/*
			mouseInput = Input.GetAxis ("Mouse Y");
			Vector3 lookHere = new Vector3 (mouseInput * rotationSpeed * Time.deltaTime, 0, 0);
			transform.Rotate (lookHere);
			Debug.Log ("dddddddddddd " + transform.rotation.x);

		}
		/*
		if (Input.GetAxis ("Fire2") != 0) { //resettaa kameran rotaation
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationSpeed);
			!(transform.rotation.x > MaxAngle || transform.rotation.x < MinAngle)
		}
		*/
	}
}
