using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour {

    UnityEvent m_MyEvent = new UnityEvent();
    public SpeechBubbleCreator speechCreator;
    public int id;
    public GameObject Canvas; // drag from hierarchy
    public int currentSpeechInstance, maxSpeechInstance;
    public bool talking = false;
    private void Start() {
        id = 5;
        currentSpeechInstance = 1;
        maxSpeechInstance = 3;
    }

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(TalkEvent);
        }

    }
    void OnTriggerExit(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
            talking = false;
            currentSpeechInstance = 1;
            speechCreator.CloseSpeechBubble(this);
            m_MyEvent.RemoveListener(TalkEvent);
        }

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.O) && m_MyEvent != null) {
            m_MyEvent.Invoke();
        }
    }

    public void TalkEvent() {
        if (talking == true) {
            if (currentSpeechInstance == maxSpeechInstance) {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                Canvas.SetActive(true);
            }
            else {
                speechCreator.UpdateSpeechBubble(this);
            }
        }
        else {
            speechCreator.GenerateSpeechBubble(this);
            talking = true;
        }
        
    }
}
