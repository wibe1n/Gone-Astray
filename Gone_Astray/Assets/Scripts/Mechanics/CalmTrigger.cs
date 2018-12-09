using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmTrigger : MonoBehaviour {

    public SpoopyTrigger spoopyTrigger;
    private GameObject ambienceSounds;
    public PencilContourEffect pencilEffects;
    float currentLight, endLight;

    float duration;


    void Start() {
        ambienceSounds = GameObject.FindGameObjectWithTag("CameraRig");
        currentLight = 0.6f;
        endLight = 0f;
        duration = 10f;

    }

    private void OnTriggerEnter(Collider player) {
        if (player.GetComponent<Character>() && player.GetComponent<Character>().spooped) {
            spoopyTrigger.spoopySounds.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambienceSounds.GetComponent<FMODUnity.StudioEventEmitter>().Play();
            StartCoroutine(TurnLightsBack());
            player.GetComponent<Character>().spooped = false;
        }
    }

    public IEnumerator TurnLightsBack() {
        float timeRemaining = duration;
        while (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            pencilEffects.m_EdgesOnly = Mathf.Lerp(currentLight, endLight, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;
        }
        pencilEffects.m_EdgesOnly = endLight;
    }
}
