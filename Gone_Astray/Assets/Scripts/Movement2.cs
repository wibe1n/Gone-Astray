using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour {

    public float speed;
	public float rotationSpeed;
    public GameObject camera;

    CharacterController characterController;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 horizontalMovement = new Vector3(0, 0, 0);
        Vector3 verticalMovement = new Vector3(0, 0, 0);
        //var forward = camera.transform.forward;
        //var right = camera.transform.right;

        //Vector3 moveDir = new Vector3(0, 0, 0);

        //if (Input.GetAxis("Horizontal") != 0)
        //{
        //    moveDir.x += right.x * Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //    //transform.Translate(forward * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        //}
        //if (Input.GetAxis("Vertical") != 0)
        //{
        //    moveDir.z += forward.z * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //    //transform.Translate(forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        //}

        //transform.Translate(moveDir);

        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    if (Input.GetAxis("Horizontal") != 0)
        //    {
        //        Vector3 temp = new Vector3(-(transform.position.z - camera.transform.position.z), 0f, 0f);
        //        horizontalMovement = temp.normalized * Input.GetAxis("Horizontal");
        //        //transform.Translate((transform.position - camera.transform.position).normalized * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        //    }
        //    if (Input.GetAxis("Vertical") != 0)
        //    {
        //        Vector3 temp = new Vector3(0f, 0f, transform.position.z - camera.transform.position.z);
        //        verticalMovement = temp.normalized * Input.GetAxis("Vertical");
        //        //transform.Translate(temp.normalized * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        //    }

        //    transform.Translate((horizontalMovement + verticalMovement) * Time.deltaTime * speed);
        //}

        Vector3 dir = new Vector3(0, 0, 0);
        if (Input.GetAxis("Horizontal") != 0)
        {
            dir += camera.transform.right * Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            dir += camera.transform.forward * Input.GetAxis("Vertical");
        }
        dir.y = 0f;
        dir.Normalize();
        //transform.Translate(dir * Time.deltaTime * speed);
        characterController.Move(dir * Time.deltaTime * speed);

        //transform.Translate(moveDir * Time.deltaTime * speed);
        //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);
        //transform.Translate(camera.transform.right.x * Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, camera.transform.right.x * Input.GetAxis("Vertical") * Time.deltaTime * speed);

        //transform.rotation.eulerAngles.y = camera.transform.rotation.eulerAmgles.y;

        //hahmon rotaatio kävelysuuntaan
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 direction = dir;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
