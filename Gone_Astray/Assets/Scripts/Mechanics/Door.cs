using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {
	//tämä scripti teleporttaa pelaajan määrättyyn sijaintiin scenessä jos pelaajalla on avain/raskovnik

	public KeyCode talkKey;
	public GameObject otherDoor;
    public GameObject blackScreen;
    public GameObject camera;
    public GameObject canvas;


    // Use this for initialization
    void Start () {
		//säädetään keybindit undying objectilla
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			Undying_Object undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.talkKey == KeyCode.None)
				talkKey = KeyCode.E;
			else
				talkKey = undyObj.talkKey;
		}else
			talkKey = KeyCode.E;
        Debug.Log(camera);     
    }

    private void OnTriggerEnter(Collider player) {
        canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        canvas.SetActive(false);
    }

    void OnTriggerStay(Collider player) {
		//jos ovella on pelaaja
		if (player.gameObject.GetComponent<Character>() != null) {
			//ja pelaajalla on avain
			if (player.gameObject.GetComponent<Character> ().hasRaskovnik) {
				//ja pelaaja avaa oven
				if (Input.GetKeyDown(talkKey)){
                    //ovenavaus cutscene ja äänet tähän
                    TeleportIn(player.gameObject);
				}
			}
		}
	}

    

    public void TeleportIn(GameObject player)
    {
        StartCoroutine(TeleportRoutine(player));
    }

    IEnumerator TeleportRoutine(GameObject player)
    {
        blackScreen.SetActive(true);
        blackScreen.GetComponentInChildren<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
        player.transform.position = otherDoor.transform.position;
        camera.transform.position = otherDoor.transform.position;
        yield return new WaitForSeconds(1f);
        blackScreen.GetComponentInChildren<Image>().CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(3f);
        blackScreen.SetActive(false);
    }
}
