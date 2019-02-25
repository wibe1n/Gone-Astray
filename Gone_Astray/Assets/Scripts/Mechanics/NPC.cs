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
				talkKey = KeyCode.E;
			else
				talkKey = undyObj.talkKey;
		}else
			talkKey = KeyCode.E;
        talkBackKey = KeyCode.P;
    }

    void OnTriggerEnter(Collider player) {
        //Lisätään kuuntelija ja avataan kyssäri-ikoni. 
        walkedAway = false;
        if (player.gameObject.GetComponent<Character>() != null && !speechCreator.StillTalking()) {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(TalkEvent);
            m_SecondEvent.AddListener(Backwards);
            //Tietyt NPC voivat alkaa älisee heti
            if (id == 6 && currentSpeechInstance == 1) {
                m_MyEvent.Invoke();
            }
        }

    }
    void OnTriggerExit(Collider player) {
        walkedAway = true;
        if (player.gameObject.GetComponent<Character>() != null) {
            //Jos lähtee pois kesken keskustelun niin keskusteluketju resetoituu ensimmäiseen
            if (!speechCreator.StillTalking() || currentSpeechInstance == maxSpeechInstance)
            {
                Canvas.SetActive(false);
                talking = false;
                //Jotkut NPC ei vät välttämättä resetoidu?
                if (id != 6) {
                    currentSpeechInstance = 1;
                } 
                speechCreator.CloseSpeechBubble(this);
                m_MyEvent.RemoveListener(TalkEvent);
            }
            //Jos pelaaja kävelee liian kauas puhujasta puhumisen aikana, niin npc ruikuttaa
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
		//tallennukselle välitetään pelaajan kautta missä kohtaa keskustelua mennään
		player.NPCspeechInstance = currentSpeechInstance;
        if (talking == true) {
            //Jos ollaan vikassa puhekerrassa niin suljetaan puhekupla
            if (currentSpeechInstance == maxSpeechInstance) {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                if (!walkedAway)
                    Canvas.SetActive(true);
                //Jotkut npc:t voivat lähteä kävelemään tms.
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
            //Jos kävelee kesken pois suljetaan puhekupla ja poistetaan kuuntelija
            else if (walkedAway == true)
            {
                speechCreator.CloseSpeechBubble(this);
                talking = false;
                m_MyEvent.RemoveListener(TalkEvent);
            }
            //Muussa tapauksessa päivitetään puhekupla
            else {
                speechCreator.UpdateSpeechBubble(this);
            }
        }
        //Jos keskustelu aloitetaan, luodaan enismmäinen puhekupla
        else {
            speechCreator.GenerateSpeechBubble(this);
            talking = true;
        }
    }

    public void Backwards() {
        speechCreator.BackWards(this);
    }
    //Ruikutuskupla pysyy tietyn aikaa
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
