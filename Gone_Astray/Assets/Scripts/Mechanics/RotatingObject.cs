using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    public GameObject target;
    
    void Update()
    {
        target.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        //target.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }
}
