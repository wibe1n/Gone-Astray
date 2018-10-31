using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : MonoBehaviour
{
    public int itemIndex;
    private bool open;
    UnityEvent m_MyEvent = new UnityEvent();
    public SpeechBubbleCreator speechCreator;
    public GameObject Canvas; // drag from hierarchy
	public bool isCollectable;
	public bool isFirefly;

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(LookObject);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.O) && m_MyEvent != null) {
            m_MyEvent.Invoke();
        }

    }

    public void LookObject() {
        if (open) {
            speechCreator.CloseInfoBox();
            open = false;
        }
        else {
            speechCreator.GenerateInfoBox(this);
            open = true;
        }
    }

    private void OnTriggerStay(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            if (Input.GetKeyDown("4") && isCollectable) {
                if (isFirefly) {
                    Firefly firefly = new Firefly(0);
                    player.GetComponent<Character>().myFireflies.Add(firefly);
                }
                else {
                    player.GetComponent<Character>().items[itemIndex] = true;
                }
                Destroy(gameObject);
            }
        }
        
    }

    void OnTriggerExit(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
            m_MyEvent.RemoveListener(LookObject);
        }
        
    }
}
