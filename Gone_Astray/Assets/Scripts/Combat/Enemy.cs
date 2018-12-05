﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int disturbTreshold;
    public GameObject checkpoint;
	public bool isBoss;
    public bool isTutorial;
    private PencilContourEffect screenEffects;
    private List<Firefly> availableFireflies = new List<Firefly> { };
    public GameObject eye1, eye2;

    float currenAmount = 0.01F, endAmount = 0.04f;

    float duration = 2;

    private void Start() {
        screenEffects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PencilContourEffect>();
    }

    void OnTriggerEnter(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<MovementControls>().stop = true;
            availableFireflies = player.gameObject.GetComponent<Character>().myFireflies;
            Encounter();
        }
        
	}
	void OnTriggerExit(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<MovementControls>().stop = false;
        }
    }

    private void Encounter() {
        StartCoroutine(StartEncounterIenum(this));
    }

    private IEnumerator StartEncounterIenum(Enemy enemy) {
        eye1.SetActive(true);
        eye2.SetActive(true);
        EncounterController enCon = GameObject.FindGameObjectWithTag("EncounterController").GetComponent<EncounterController>();
        float timeRemaining = duration;
        while (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            screenEffects.m_NoiseAmount = Mathf.Lerp(currenAmount, endAmount, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;
        }
        screenEffects.m_NoiseAmount = endAmount;
        while (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            screenEffects.m_NoiseAmount = Mathf.Lerp(endAmount, currenAmount, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;
        }
        screenEffects.m_NoiseAmount = currenAmount;
        enCon.StartEncounter(enemy, availableFireflies);
        yield return new WaitForSeconds(1);
    }
}
