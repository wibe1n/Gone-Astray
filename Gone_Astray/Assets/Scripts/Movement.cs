using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	//public muuttujien alkuarvot asetetaan inspectorissa
    public float speed; //liikkumisnopeus
	public float rotationSpeed; //kääntymisnopeus
	Rigidbody rigbod; //rigidbody
	public float jumpforce;
	bool onGround = true;
	bool falling = false;

	// Use this for initialization
	void Start () {
		rigbod = gameObject.GetComponent<Rigidbody> ();


	}
	
	// Update is called once per frame
	void Update () { //Axis vertical/horizontal tarkoittaa wasd ja nuolinäppäimiä
        transform.Translate(0f, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed); //hahmo liikkuu eteen ja taakse
		transform.Rotate(0f, Input.GetAxis("Horizontal") * rotationSpeed, 0f); //hahmo kääntyy vasemmalla ja oikealle
		if (Input.GetAxis("Jump") !=0 && onGround){ //jos space painettu ja yn suuntainen nopeus on nolla
			rigbod.AddForce (Vector3.up * jumpforce, ForceMode.Impulse); //niin hyppää
			onGround = false;
			falling = false;
		}
		if (rigbod.velocity.y < 0)
			falling = true;
		if (rigbod.velocity.y == 0 && falling)
			onGround = true;
    }
}
