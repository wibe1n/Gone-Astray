using UnityEngine;
using System.Collections;

public class BillboardSprite : MonoBehaviour
{

    public Transform MyCameraTransform;
    private Transform MyTransform;
    public bool alignNotLook = true; 
    void Start()
    {
        MyTransform = this.transform;
        MyCameraTransform = Camera.main.transform;
    }
    void LateUpdate()
    {
        if (alignNotLook)
            MyTransform.forward = MyCameraTransform.forward;
        else
            MyTransform.LookAt(MyCameraTransform, Vector3.up);
    }
}