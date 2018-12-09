using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour {

    UnityEvent m_MyEvent = new UnityEvent();
    UnityEvent m_SecondEvent = new UnityEvent();
    public SpeechBubbleCreator speechCreator;
    public int id;
    public GameObject Canvas; // drag from hierarchy
    public int currentSpeechInstance, maxSpeechInstance;
    public bool talking = false;
    public bool walkedAway = false;
	public KeyCode talkKey = KeyCode.None;
    public KeyCode talkBackKey = KeyCode.None;
    private Character player;

    private void Start() {
        currentSpeechInstance = 1;
        if(id == 6) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
        


        if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			Undying_Object undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.talkKey == KeyCode.None)
				talkKey = KeyCode.O;
			else
				talkKey = undyObj.talkKey;
		}else
			talkKey = KeyCode.O;
        talkBackKey = KeyCode.P;
    }

    void OnTriggerEnter(Collider player) {
        walkedAway = false;
        if (player.gameObject.GetComponent<Character>() != null && !speechCreator.StillTalking()) {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(TalkEvent);
            m_SecondEvent.AddListener(Backwards);
        }

    }
    void OnTriggerExit(Collider player) {
        walkedAway = true;
        if (player.gameObject.GetComponent<Character>() != null) {
            if (!speechCreator.StillTalking() || currentSpeechInstance == maxSpeechInstance)
            {
                Canvas.SetActive(false);
                talking = false;
                if (id != 6) {
                    Debug.Log("mennään täällä");
                    currentSpeechInstance = 1;
                } 
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
            if (id == 6 && gameObject.GetComponent<MoveToWaypoints>().proceed) { }
            else{
                m_MyEvent.Invoke();
            }
        }
        else if (Input.GetKeyDown(talkBackKey) && m_MyEvent != null) {
            m_SecondEvent.Invoke();
        }
    }

    public void TalkEvent() {
        if (talking == true) {
            if (currentSpeechInstance == maxSpeechInstance) {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                if (!walkedAway)
                    Canvas.SetActive(true);
                if(id == 6) {
                    if (maxSpeechInstance == 11) {
                        gameObject.GetComponent<MoveToWaypoints>().DisableHovering();
                        currentSpeechInstance = 12;
                        maxSpeechInstance = 14;
                    }
                    else {
                        player.AddFirefly();
                        Canvas.SetActive(false);
                        Destroy(gameObject);
                    }
                    
                }
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

    public void Backwards() {
        speechCreator.BackWards(this);
    }

    IEnumerator CloseWhineBox(float time) {
        yield return new WaitForSeconds(time);

        if (walkedAway) {
            speechCreator.CloseSpeechBubble(this);
            talking = false;
            m_MyEvent.RemoveListener(TalkEvent);
        }
        else {
            Canvas.SetActive(true);
            speechCreator.CloseSpeechBubble(this);
            talking = false;
        }
    }
}
