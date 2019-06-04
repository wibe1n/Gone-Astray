using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int disturbLimit;
    public int disturbTreshold;
    public GameObject checkpoint;
    public GameObject destination;
	public bool isBoss;
    public bool isTutorial;
    public bool hasEyes;
    public List<GameObject> lightPath = new List<GameObject> { };
    private PencilContourEffect screenEffects;
    private List<Firefly> availableFireflies = new List<Firefly> { };
    public GameObject eye1, eye2;

    float currenAmount = 0.001F, endAmount = 0.005f;

    float duration = 2;

    private void Start() {
        screenEffects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PencilContourEffect>();
    }

    //Pelaaja pysähtyy ja aloitetaan encounter
    void OnTriggerEnter(Collider player){
        if (player.gameObject.GetComponent<Character>() != null) {
            player.gameObject.GetComponent<MovementControls>().stop = true;
            player.gameObject.GetComponent<MovementControls>().destination = destination.transform;
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

    //Laita silmät palamaan 
    private IEnumerator StartEncounterIenum(Enemy enemy) {
        if (hasEyes) {
            eye1.SetActive(true);
            eye2.SetActive(true);
        }
        EncounterController enCon = GameObject.FindGameObjectWithTag("EncounterController").GetComponent<EncounterController>();
        float timeRemaining = duration;
        //screenefektien väläytys
        //while (timeRemaining > 0) {
        //    timeRemaining -= Time.deltaTime;
        //    screenEffects.m_ErrorRange = Mathf.Lerp(currenAmount, endAmount, Mathf.InverseLerp(duration, 0, timeRemaining));
        //    yield return null;
        //}
        //screenEffects.m_NoiseAmount = endAmount;
        while (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            screenEffects.m_ErrorRange = Mathf.Lerp(endAmount, currenAmount, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;
        }
        screenEffects.m_NoiseAmount = 0;
        //lähetetään vihollinen ja pelaajan käytettävissä olevat tulikärpäset encounter controlleriin
        enCon.StartEncounter(enemy, availableFireflies);
        yield return new WaitForSeconds(1);
    }
}
