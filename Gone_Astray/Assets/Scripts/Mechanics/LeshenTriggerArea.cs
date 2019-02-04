using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeshenTriggerArea : MonoBehaviour {

    //tämä scripti sieniympyrälle johon leshen kuuluu istuttaa
    UnityEvent m_MyEvent = new UnityEvent();
	public GameObject spawnPosition;
    public GameObject lastLeshen, vegetation;
    //private bool vegetationGrown;
	private Undying_Object undyObj;
	public KeyCode leshenKey = KeyCode.None;

	void Start(){
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.leshenKey == KeyCode.None)
				leshenKey = KeyCode.L;
			else
				leshenKey = undyObj.leshenKey;
		}else
			leshenKey = KeyCode.L;
	}

	void OnTriggerEnter(Collider player){
		if(player.GetComponent<Character>().hasLeshen)
        	m_MyEvent.AddListener(ActivateLeshen);
        //Kerrotaan pelaajalle että olet leshenin lähellä
        player.GetComponent<Character>().leshenObjectNear = true;
    }
	void OnTriggerExit(Collider player){
		if(player.GetComponent<Character>().hasLeshen)
        	m_MyEvent.RemoveListener(ActivateLeshen);
        //Kerrotaan pelaajalle ettei hän enää ole leshenin lähellä
        player.GetComponent<Character>().leshenObjectNear = false;
    }

    private void Update() {
        //Kun nappia painetaan ja jos kuuntelija on olemassa luodaan leshen
		if (Input.GetKeyDown(leshenKey) && m_MyEvent != null) {
			if (!(lastLeshen == null)) { 

				Destroy(lastLeshen);
			}
            m_MyEvent.Invoke();
        }
    }

    public void ActivateLeshen() {
		lastLeshen = Instantiate(vegetation, spawnPosition.transform.position, vegetation.transform.rotation);
        //vegetationGrown = true;
    }
}
