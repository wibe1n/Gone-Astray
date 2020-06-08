using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringObject : MonoBehaviour {

    public float verticalSpeed;
    public float amplitude;

    public Vector3 tempPosition;
    public Vector3 startingPosition;


	void Start () {
        tempPosition = transform.position;
        startingPosition = transform.position;
	}

    private void Update()
    {
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void FixedUpdate () {
        GetPosition();
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude + startingPosition.y;
        transform.position = tempPosition;
	}

    public void GetPosition() {
        tempPosition = transform.position;
        //startingPosition = transform.position;
    }

    public void ResetPos()
    {
        startingPosition = this.transform.parent.gameObject.transform.position;
    }
}
