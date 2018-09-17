using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	//public muuttujien alkuarvot asetetaan inspectorissa
    public float speed; //liikkumisnopeus
	public float rotationSpeed; //kääntymisnopeus
	Rigidbody rigbod; //rigidbody
	public float jumpforce;
	Vector3 jump;
	// Use this for initialization
	void Start () {
		rigbod = gameObject.GetComponent<Rigidbody> ();
		jump = new Vector3 (0, jumpforce, 0);

	}
	
	// Update is called once per frame
	void Update () { //Axis vertical/horizontal tarkoittaa wasd ja nuolinäppäimiä
        transform.Translate(0f, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed); //hahmo liikkuu eteen ja taakse
		transform.Rotate(0f, Input.GetAxis("Horizontal") * rotationSpeed, 0f); //hahmo kääntyy vasemmalla ja oikealle
		if (Input.GetAxis("Jump") != 0 && rigbod.velocity.y == 0){ //jos space painettu ja yn suuntainen nopeus on nolla
			rigbod.AddForce (jump); //niin hyppää
		}
    }
}
