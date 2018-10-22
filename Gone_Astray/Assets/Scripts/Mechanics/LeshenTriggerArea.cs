using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeshenTriggerArea : MonoBehaviour {

    //tämä scripti sieniympyrälle johon leshen kuuluu istuttaa
    UnityEvent m_MyEvent = new UnityEvent();
	public GameObject spawnPosition;
    public GameObject lastSapling, vegetation;
    private bool vegetationGrown;

	void OnTriggerEnter(Collider player){
        m_MyEvent.AddListener(ActivateLeshen);
        player.GetComponent<Character>().leshenObjectNear = true;
    }
	void OnTriggerExit(Collider player){
        m_MyEvent.RemoveListener(ActivateLeshen);
        player.GetComponent<Character>().leshenObjectNear = false;
    }

    private void Update() {
        if (Input.GetKeyDown("7") && m_MyEvent != null) {
            m_MyEvent.Invoke();
        }
    }

    public void ActivateLeshen() {
        
        if (!(lastSapling == null)) {           
            Destroy(lastSapling);
        }
        lastSapling = Instantiate(vegetation, spawnPosition.transform.position, gameObject.transform.rotation);
        vegetationGrown = true;
    }
}
