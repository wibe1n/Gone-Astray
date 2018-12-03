using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireflyAmount : MonoBehaviour {

	public Character chara;

	void Update () {
		gameObject.GetComponent<Text> ().text = "Fireflies: " + chara.myFireflies.Count;
	}
}
