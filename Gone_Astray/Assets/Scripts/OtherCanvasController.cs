using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCanvasController : MonoBehaviour {

    public GameObject shooBirdsCanvas;
    //nyt helvetti päivityt

	// Use this for initialization
	void Start () {
        shooBirdsCanvas.SetActive(false);
	}

    public void ThrowRock()
    {
        shooBirdsCanvas.SetActive(false);

        //Tekemättä:
        //Play animation
        //Varikset puhuvat ilkeämmin jatkossa
    }

    public void MakeNoise()
    {
        shooBirdsCanvas.SetActive(false);

        //Tekemättä:
        //Play animation
        //Varikset puhuvat kiltimmin jatkossa
    }
}
