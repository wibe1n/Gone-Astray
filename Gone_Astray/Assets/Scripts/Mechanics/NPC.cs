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
    public bool walkedAway = false;
	public Undying_Object undyObj;
	public KeyCode talkKey = KeyCode.None;

    private void Start() {
        id = 6;
        currentSpeechInstance = 1;
        maxSpeechInstance = 3;

		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.talkKey == KeyCode.None)
				talkKey = KeyCode.O;
			else
				talkKey = undyObj.talkKey;
		}else
			talkKey = KeyCode.O;
    }

    void OnTriggerEnter(Collider player) {
        walkedAway = false;
        if (player.gameObject.GetComponent<Character>() != null && !speechCreator.StillTalking()) {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(TalkEvent);
        }

    }
    void OnTriggerExit(Collider player) {
        walkedAway = true;
        if (player.gameObject.GetComponent<Character>() != null) {
            if (!speechCreator.StillTalking() || currentSpeechInstance == maxSpeechInstance)
            {
                Canvas.SetActive(false);
                talking = false;
                currentSpeechInstance = 1;
                speechCreator.CloseSpeechBubble(this);
                m_MyEvent.RemoveListener(TalkEvent);
            }
            //Jos pelaaja kävelee liian kauas puhujasta
            else if (!(currentSpeechInstance == maxSpeechInstance))
            {
                speechCreator.WentTooFar(this);
                StartCoroutine(CloseWhineBox(20));
            }
        }
    }

    private void Update() {
		if (Input.GetKeyDown(talkKey) && m_MyEvent != null) {
            m_MyEvent.Invoke();
        }
    }

    public void TalkEvent() {
        if (talking == true) {
            if (currentSpeechInstance == maxSpeechInstance)
            {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                if (!walkedAway)
                    Canvas.SetActive(true);
            }
            else if (walkedAway == true)
            {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                m_MyEvent.RemoveListener(TalkEvent);
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

    IEnumerator CloseWhineBox(float time)
    {
        yield return new WaitForSeconds(time);

        if (walkedAway)
        {
            speechCreator.CloseSpeechBubble(this);
            talking = false;
            m_MyEvent.RemoveListener(TalkEvent);
        }
        else
        {
            Canvas.SetActive(true);
            speechCreator.CloseSpeechBubble(this);
            talking = false;
        }
    }
}
