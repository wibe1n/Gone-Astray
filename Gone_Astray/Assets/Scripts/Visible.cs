using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour {

    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        
    }

    void Update()
        {
        if (renderer.isVisible) { }
        else Destroy(gameObject);
        }
    
}
