using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameObject Canvas;

    void OnTriggerEnter() {
        Canvas.SetActive(true);
    }
    void OnTriggerExit()
    {
        Canvas.SetActive(false);
    }
}
