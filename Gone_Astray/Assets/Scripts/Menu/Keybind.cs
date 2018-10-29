using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybind : MonoBehaviour {

	//lisää muuttuja public [scriptin nimi] jokaiselle scriptille jonka keycodeja ollaan bindaamassa
	//tarkista että kyseisten scriptien kaikki bindattavat arvot ovat public keycode muuttujia
	//liitä tämä scripti tyhjälle objectille scenessä
	//laita jokaiselle napille tähän scriptiin oma public GameObject ja liitä nappi siihen inspectorissa(muista laittaa napin navigaatio noneksi)
	//tee jokaiselle napille oma functio joka asettaa whichButtonille oman arvon ja vaihtaa kys. napin textin napin GameObjectin kautta
	//laita jokaiselle napille OnClick functioiksi onButtonPress ja napin oma functio
	//Lisää switch caseen napin whichButton arvon alle napin asettama keybindi ja napin texti

	public KeyCode keyCode;
	public Undying_Object undyObj;
	public GameObject crouchButton;
	public GameObject leshenButton;
	public GameObject pauseButton;
	public GameObject altPauseButton;
	public GameObject journalButton;
	bool pressing = false;
	int whichButton = 0;

	void Start(){
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
		}else
			Debug.Log("undying object ei löydy");
	}

	public void onButtonPress(){
		if (!pressing) {
			pressing = true;
		}
	}
	void OnGUI()
	{
		if (pressing) {
			Event e = Event.current;
			if (e.isKey) {
				keyCode = e.keyCode;
				switch (whichButton) {
				case 1:
					undyObj.crouchKey = keyCode;
					crouchButton.GetComponentInChildren<Text> ().text = keyCode.ToString ();
					break;
				case 2:
					undyObj.leshenKey = keyCode;
					leshenButton.GetComponentInChildren<Text> ().text = keyCode.ToString ();
					break;
				case 3:
					undyObj.pauseKey = keyCode;
					pauseButton.GetComponentInChildren<Text> ().text = keyCode.ToString ();
					break;
				case 4:
					undyObj.altPauseKey = keyCode;
					altPauseButton.GetComponentInChildren<Text> ().text = keyCode.ToString ();
					break;
				case 5:
					undyObj.journalKey = keyCode;
					journalButton.GetComponentInChildren<Text> ().text = keyCode.ToString ();
					break;
				default:
					Debug.Log("switchi unohtui");
					break;
				}

				pressing = false;
			}
		}
	}
	public void crouchPress(){
		whichButton = 1;
		crouchButton.GetComponentInChildren<Text> ().text = "press key to bind";
	}
	public void leshenPress(){
		whichButton = 2;
		leshenButton.GetComponentInChildren<Text> ().text = "press key to bind";
	}
	public void pausePress(){
		whichButton = 3;
		pauseButton.GetComponentInChildren<Text> ().text = "press key to bind";
	}
	public void altPausePress(){
		whichButton = 4;
		altPauseButton.GetComponentInChildren<Text> ().text = "press key to bind";
	}
	public void journalPress(){
		whichButton = 5;
		journalButton.GetComponentInChildren<Text> ().text = "press key to bind";
	}
}
