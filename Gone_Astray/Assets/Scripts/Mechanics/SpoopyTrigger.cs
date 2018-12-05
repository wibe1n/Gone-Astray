using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoopyTrigger : MonoBehaviour {

    public FMOD.Studio.EventInstance spoopySounds;
    public FMOD.Studio.ParameterInstance spoopyParameter;
    public GameObject ambienceSounds;
    public PencilContourEffect pencilEffects;
    float currentDarkness, endDarkness;

    float duration;
    

    void Start () {
        ambienceSounds = GameObject.FindGameObjectWithTag("CameraRig");
        spoopySounds = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/AmbientMusic");
        spoopySounds.getParameter("Progression", out spoopyParameter);
        currentDarkness = 0;
        endDarkness = 0.6f;
        duration = 10f;

    }

    private void OnTriggerEnter(Collider player) {
        if (player.GetComponent<Character>() && player.GetComponent<Character>().spooped == false) {
            player.GetComponent<Character>().spooped = true;
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(spoopySounds, player.GetComponent<Transform>(), player.GetComponent<Rigidbody>());
            spoopySounds.start();
            spoopyParameter.setValue(0.2f);
            ambienceSounds.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
            StartCoroutine(TurnLightsSpoopy());
        }
    }

    public IEnumerator TurnLightsSpoopy() {
        float timeRemaining = duration;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            pencilEffects.m_EdgesOnly = Mathf.Lerp(currentDarkness, endDarkness, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;
        }
        pencilEffects.m_EdgesOnly = endDarkness;
    }


}
