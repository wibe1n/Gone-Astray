using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


    public float speed;
    public float force;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       // if (photonView.isMine)
        {

            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f,
            Input.GetAxis("Vertical") * Time.deltaTime * speed);
            
            

        } 
    }
}
