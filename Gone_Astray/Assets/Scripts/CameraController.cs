using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//public Camera myCam;
	public float rotationSpeed; // pelaajaan käännön nopeus
	public Quaternion originalRotationValue;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		originalRotationValue = transform.rotation; // save the initial rotation
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Fire1") !=0) {
			// Käännetään pelaajaa hiiren liikkeeellä
			float mouseInputX = Input.GetAxis ("Mouse X");
			float mouseInputY = Input.GetAxis ("Mouse Y");
			Vector3 lookHere = new Vector3 (-1 * mouseInputY * rotationSpeed * Time.deltaTime, mouseInputX * rotationSpeed * Time.deltaTime, 0);
			transform.Rotate (lookHere);
		}
		if (Input.GetAxis ("Fire2") != 0) {
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationSpeed);
		}
	}
}
