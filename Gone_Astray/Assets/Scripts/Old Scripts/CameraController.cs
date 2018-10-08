using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//public Camera myCam;
	public float rotationSpeed; // kameran käännön nopeus
	public Quaternion originalRotationValue; //tallentaa kameran default rotaation

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked; //piilottaa kursorin pelin aikana (inspectorissa kursorin saa takaisin escillä)
		originalRotationValue = transform.rotation; // save the initial rotation
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Fire1") !=0) { //kääntää kameraa hiirellä jos hiiri klickattu
			// Käännetään pelaajaa hiiren liikkeeellä
			float mouseInput = Input.GetAxis ("Mouse X");
			//float mouseInputY = Input.GetAxis ("Mouse Y");
			Vector3 lookHere = new Vector3 (0, mouseInput * rotationSpeed * Time.deltaTime, 0);
			transform.Rotate (lookHere);
		}
		if (Input.GetAxis ("Fire2") != 0) { //resettaa kameran rotaation
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationSpeed);
		}
	}
}
