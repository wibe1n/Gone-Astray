using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWaypoints : MonoBehaviour {

    public GameObject[] waypoints;
    private HoveringObject hovering;
    public Vector3 tempPosition;
    int current = 0;
    float rotSpeed;
    public float speed;
    float Wpradius = 1;
    public bool proceed;

    private void Start() {
        hovering = gameObject.GetComponent<HoveringObject>();
        proceed = true;
    }

    private void FixedUpdate() {
        if(Vector3.Distance(waypoints[current].transform.position, transform.position) < Wpradius) {
            if(current == 1) {
                hovering.GetPosition();
                StartHovering();
            }
            current++;
            if (current >= waypoints.Length) {
                current = 0;
                proceed = false;
            }
        }
        if (proceed) {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        }
        
    }

    public void DisableHovering() {
        hovering.enabled = false;
    }

    public void StartHovering() {
        hovering.enabled = true;
    }
}
