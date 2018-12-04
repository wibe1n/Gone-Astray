using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCanvasController : MonoBehaviour {

    public Canvas shooBirdsCanvas;
    //nyt helvetti päivityt

	// Use this for initialization
	void Start () {
        shooBirdsCanvas.enabled = false;
	}

    public void ActivateShooBirdsCanvas()
    {
        shooBirdsCanvas.enabled = true;
    }

    public void ThrowRock()
    {
        shooBirdsCanvas.enabled = false;

        //Tekemättä:
        //Play animation
        //Varikset puhuvat ilkeämmin jatkossa
    }

    public void MakeNoise()
    {
        shooBirdsCanvas.enabled = false;

        //Tekemättä:
        //Play animation
        //Varikset puhuvat kiltimmin jatkossa
    }
}
