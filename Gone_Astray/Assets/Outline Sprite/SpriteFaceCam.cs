using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFaceCam : MonoBehaviour
{
	public Transform camera;
	
    void LateUpdate()
    {
		if (camera != null)
		{
		transform.rotation = camera.rotation;
		}
    }
}
