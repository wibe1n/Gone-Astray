using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeshenLerp : MonoBehaviour {

	float timeStartedLerping;
	float lerpTime = 2.0f;

	Vector3 startScale = new Vector3(0.1f,0.1f,0.1f);
	Vector3 endScale = new Vector3(1.0f,1.0f,1.0f);
	Vector3 currentScale;

	float timeSinceStarted;
	float percentageComplete;

	bool first = true;
	//bool last = true;
	//bool despawning = false;

	void Update(){
		if (gameObject.activeSelf) {
			if (first) {
				timeStartedLerping = Time.time;
				first = false;
			}
			LerpScale ();
		}
		gameObject.transform.localScale = currentScale;
		/*
		if (despawning) {
			if (last) {
				timeStartedLerping = Time.time;
				last = false;
			}
			DespawnLerp();
		}
		*/
	}

	public void LerpScale(){
		timeSinceStarted = Time.time - timeStartedLerping;
		percentageComplete = timeSinceStarted / lerpTime;
		currentScale = Vector3.Lerp (startScale,endScale, percentageComplete);
	}

	void OnDisable(){
		first = true;
		gameObject.transform.localScale = startScale;
	}
	/*
	public void DespawnLerp(){
		despawning = true;
		timeSinceStarted = Time.time - timeStartedLerping;
		percentageComplete = timeSinceStarted / lerpTime;
		currentScale = Vector3.Lerp (endScale,startScale, percentageComplete);
		if (percentageComplete > 0.9f) {
			first = true;
			last = true;
			despawning = false;
			gameObject.SetActive (false);
		}
	}
	*/
}
