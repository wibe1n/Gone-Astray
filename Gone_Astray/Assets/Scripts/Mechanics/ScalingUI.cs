using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingUI : MonoBehaviour
{
    public float FixedSize = 0.05f;
    public Camera Camera;
    public float startScale = 0;

    void Update()
    {
        var distance = (Camera.transform.position - transform.position).magnitude;
        var size = distance * FixedSize * Camera.fieldOfView;
        //muokkaa alempana olevaa lukua 10 jos kokoa pitää muuttaa
        transform.localScale = Vector3.one * size /10;
        transform.forward = transform.position - Camera.transform.position;
        if (startScale < 1)
        {
            transform.localScale *= startScale;
            startScale += 0.05f;
        }
    }
}
