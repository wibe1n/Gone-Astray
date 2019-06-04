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
        proceed = false;
    }

    private void FixedUpdate()
    {
        //Jos ollaan päästy etapin luokse niin
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < Wpradius)
        {
            //jos ollaan perillä aletaan leijumaan
            if (current == waypoints.Length)
            {
                hovering.GetPosition();
                StartHovering();
            }
            current++;
            //Jos ei olla perillä niin otetaan seuraava etappi
            if (current >= waypoints.Length)
            {
                current = 0;
                proceed = false;
            }
        }
        //Muuten mennään kohti etappi
        if (proceed)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
            transform.LookAt(waypoints[current].transform);
            
        }

    }

    public void DisableHovering() {
        hovering.enabled = false;
        proceed = true;
    }

    public void StartHovering() {
        hovering.enabled = true;
    }
}
