using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraModeButton : MonoBehaviour
{
    //Goes to the camera mode button in pause menu
    public CameraFreeFixedSwitch cam;   //attach the camera rig here
    public Text text;                   //button's text
    bool wasFix = true;

    public void SwitchCameraMode()
    {
        if (wasFix)
        {
            cam.isFreeCamera = true;
            text.text = "Free Camera";
        }
        else
        {
            cam.isFreeCamera = false;
            text.text = "Fixed Camera";
        }
        cam.CameraStateUpdate();
        wasFix = !wasFix;
    }
}
