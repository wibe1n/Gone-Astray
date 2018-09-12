using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0f, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
		transform.Rotate(0f, Input.GetAxis("Horizontal") * rotationSpeed, 0f);
    }
}
