using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWaypoints : MonoBehaviour {

    public GameObject[] waypoints;
    public Vector3 tempPosition;
    int current = 0;
    float rotSpeed;
    public float speed;
    float Wpradius;
    public bool proceed = true;

    private void FixedUpdate() {
        if(Vector3.Distance(waypoints[current].transform.position, transform.position) < Wpradius) {
            current++;
            if (current >= waypoints.Length) {
                proceed = false;
            }
        }
        if (proceed) {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        }
        
    }
}
