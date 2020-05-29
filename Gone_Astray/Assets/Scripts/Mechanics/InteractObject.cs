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
	public bool isLeshen;
	public bool isRaskovnik;

	private Undying_Object undyObj;
	public KeyCode pickKey = KeyCode.None;

    //Haetaan keybindingit
	void Start(){
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.talkKey == KeyCode.None)
				pickKey = KeyCode.E;
			else
				pickKey = undyObj.talkKey;
		}else
			pickKey = KeyCode.E;
	}

    //Lisätään eventinkuuntelija
    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(LookObject);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P) && m_MyEvent != null) {
            m_MyEvent.Invoke();
        }

    }

    //Vanhaa koodia? Infoboksin avaaminen
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

    //Otetaan objekti
    private void OnTriggerStay(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
			if (Input.GetKeyDown(pickKey) && isCollectable)
            {
                if (itemIndex == 4)
                {
                    player.GetComponent<Character>().hasJournal = true;
                }

                //tämän koodin voi varmaan joskus siistiä järkevämpään muotoon.
                //esim leshen ja raskovnik esinelistaan ja niitä käyttävät scriptit katsoo onko esine listan indexissä n boolean päällä
                if (isFirefly) {
                    player.GetComponent<Character>().AddFirefly();
				}else if (isLeshen) {
					player.GetComponent<Character>().hasLeshen = true;
				}else if (isRaskovnik) {
					player.GetComponent<Character>().hasRaskovnik = true;
				}
                else {
                    //player.GetComponent<Character>().items[itemIndex] = true;
                }
                Canvas.SetActive(false);
                Destroy(gameObject);
            }
        }
        
    }

    //poistetaan kuuntelija
    void OnTriggerExit(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
            m_MyEvent.RemoveListener(LookObject);
        }
        
    }
}
