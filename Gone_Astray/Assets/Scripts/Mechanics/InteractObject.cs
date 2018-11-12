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
	public Undying_Object undyObj;
	public KeyCode actionKey = KeyCode.None;
	public KeyCode collectKey = KeyCode.None;

	void Start () {
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.actionKey == KeyCode.None)
				actionKey = KeyCode.O;
			else
				actionKey = undyObj.actionKey;
			if (undyObj.collectKey == KeyCode.None)
				collectKey = KeyCode.K;
			else
				collectKey = undyObj.collectKey;
		} else
			collectKey = KeyCode.K;
			actionKey = KeyCode.O;
	}

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(true);
            m_MyEvent.AddListener(LookObject);
        }
    }

    private void Update() {
		if (Input.GetKeyDown(actionKey) && m_MyEvent != null) {
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
			if (Input.GetKeyDown(collectKey) && isCollectable) {
                if (isFirefly) {
                    Firefly firefly = new Firefly(0);
                    player.GetComponent<Character>().myFireflies.Add(firefly);
                }
                else {
                    player.GetComponent<Character>().items[itemIndex] = true;
                }
                Destroy(gameObject);
            }
            //else if (itemIndex == se_rashkovnikin_näköinen_kasvi_joka_ei_sitten_olekaan_se) {
            //  JOTAIN huomataan että eipäs otetakkaan mukaan
            //}
        }
        
    }

    void OnTriggerExit(Collider player) {
        if(player.gameObject.GetComponent<Character>() != null) {
            Canvas.SetActive(false);
            m_MyEvent.RemoveListener(LookObject);
        }
        
    }
}
