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

	void OnTriggerEnter(Collider player){
        m_MyEvent.AddListener(ActivateLeshen);
        player.GetComponent<Character>().leshenObjectNear = true;
    }
	void OnTriggerExit(Collider player){
        m_MyEvent.RemoveListener(ActivateLeshen);
        player.GetComponent<Character>().leshenObjectNear = false;
    }

    private void Update() {
		if (Input.GetAxis("Fire3") != 0 && m_MyEvent != null) {
			if (!(lastLeshen == null)) { 

				Destroy(lastLeshen);
			}
            m_MyEvent.Invoke();
        }
    }

    public void ActivateLeshen() {
		lastLeshen = Instantiate(vegetation, spawnPosition.transform.position, gameObject.transform.rotation);
        //vegetationGrown = true;
    }
}
