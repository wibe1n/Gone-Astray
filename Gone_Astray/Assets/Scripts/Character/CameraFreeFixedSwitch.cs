using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeFixedSwitch : MonoBehaviour
{
    //goes to camera rig. manages the two camera modes
    public bool isFreeCamera = false;
    public CameraController2 fixCam;
    public FreeCam freeCam;

    void Start()
    {
        if (freeCam == null || fixCam == null)
            Debug.Log("Camera script not set to free/fix switch");
        CameraStateUpdate();
    }
    public void CameraStateUpdate(){
        if(isFreeCamera){
            fixCam.enabled = false;
            freeCam.enabled = true;
        }else
        {
            fixCam.enabled = true;
            freeCam.enabled = false;
        }
    }
}
