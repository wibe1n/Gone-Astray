using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//public Camera myCam;
	public float rotationSpeed; // pelaajaan käännön nopeus

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Käännetään pelaajaa hiiren x-liikkeeellä
		float mouseInputX = Input.GetAxis("Mouse X");
		Vector3 lookHereX = new Vector3(0, mouseInputX * rotationSpeed * Time.deltaTime, 0);
		transform.Rotate(lookHereX);

		// Käännetään asetta hiiren y-liikkeellä
		float mouseInputY = Input.GetAxis("Mouse Y");
		Vector3 lookHereY = new Vector3(-1 * mouseInputY * rotationSpeed * Time.deltaTime, 0 , 0);
		transform.Rotate(lookHereY);
	}
}
