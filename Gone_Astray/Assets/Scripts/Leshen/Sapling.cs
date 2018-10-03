using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour {

	void Update () {
		if (!gameObject.activeSelf) {
			Destroy (gameObject);
		}
	}
}
