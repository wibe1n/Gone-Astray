using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{

    public int id;
    public GameObject fixedCamPos, ShooBirdsCanvas, bird, camera;
    public OtherCanvasController canvasController;
    public CameraController2 cameraController;

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {
        if (camera.GetComponentInParent<CameraController2>() != null)
        {
            camera.GetComponentInParent<CameraController2>().fixedCamMode = true;
        }

        switch (id)
        {
            case 1:
                //lintujen hätistely -eventti

                if (ShooBirdsCanvas.activeInHierarchy == false)
                {
                    ShooBirdsCanvas.SetActive(true);
                }
                Debug.Log(camera.transform.position);
                Debug.Log(fixedCamPos.transform.position);

                camera.transform.position = fixedCamPos.transform.position;
                camera.transform.LookAt(bird.transform.position);
                break;
            case 2:
                //jotain muuta
                break;
            default:
                break;
        }

    }

    void OnTriggerExit(Collider player)
    {
        if (camera.GetComponentInParent<CameraController2>() != null)
        {
            //Camera.main.transform.position = cameraController.cameraPos.transform.position;
            //Camera.main.transform.LookAt(cameraController.target.transform);
            cameraController.ResetCameraPos();
            camera.GetComponentInParent<CameraController2>().fixedCamMode = false;
        }
        if (ShooBirdsCanvas.activeInHierarchy == true)
            ShooBirdsCanvas.SetActive(false);
    }
}