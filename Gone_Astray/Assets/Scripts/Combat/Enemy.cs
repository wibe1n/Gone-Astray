using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int disturbLimit;
    public int disturbTreshold;
    public GameObject checkpoint;
    public GameObject destination;
    public GameObject startPosition;
	public bool isBoss;
    public bool isTutorial;
    public bool hasEyes;
    public GameObject cameraPos;
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
            player.gameObject.GetComponent<MovementControls>().destination2 = player.gameObject.transform.position;
            availableFireflies = player.gameObject.GetComponent<Character>().myFireflies;
            cameraPos = player.GetComponent<Character>().cameraPosTarget;
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
        float i = 0;
        while(i < 1) {
            i += 0.01f;
            Vector3 endPosition = new Vector3(cameraPos.transform.position.x, cameraPos.transform.position.y - 0.01f, cameraPos.transform.position.z);
            cameraPos.transform.position = Vector3.Lerp(cameraPos.transform.position, endPosition, 1);
            yield return new WaitForSeconds(0.01f);
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
        yield return new WaitForSeconds(2);
    }
}
