﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

public class SpoopyTrigger : MonoBehaviour {

    public FMOD.Studio.EventInstance spoopySounds;
    public FMOD.Studio.ParameterInstance spoopyParameter;
    public GameObject ambienceSounds;
    public PencilContourEffect pencilEffects;
    public VolumetricFog fogEffects;
    public float currentDarkness, endDarkness;

    float duration;
    
    //haetaan pelottava musiikki ja asetetaan valaistus parametrit
    void Start () {
        ambienceSounds = GameObject.FindGameObjectWithTag("CameraRig");
        spoopySounds = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/AmbientMusic");
        spoopySounds.getParameter("Progression", out spoopyParameter);
        currentDarkness = 0;
        duration = 10f;
        

    }

    //Pelaajan tullessa, muutetaan boolean, aloitetaan spoopy musiikki, pysäytetään ambienssi ja vaihdedtaan valaistus
    private void OnTriggerEnter(Collider player) {
        if (player.GetComponent<Character>() && player.GetComponent<Character>().spooped == false) {
            player.GetComponent<Character>().spooped = true;
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(spoopySounds, player.GetComponent<Transform>(), player.GetComponent<Rigidbody>());
            spoopySounds.start();
            spoopyParameter.setValue(0.2f);
            ambienceSounds.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
            pencilEffects.enabled = true;
            StartCoroutine(TurnLightsSpoopy());
        }
    }

    //valaistuksen lerppaus
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
