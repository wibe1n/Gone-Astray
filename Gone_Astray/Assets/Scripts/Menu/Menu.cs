using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	//public Character chara;
	// Use this for initialization
	public GameObject mainMenu;
	public GameObject options;

	public void OnStartGame(){
		Debug.Log ("start press");
        Game_Manager.StartLevel1();
	}

	public void OnQuit(){
		//ei toimi inspectorissa, toimii buildissa
		Debug.Log ("quit");
		Application.Quit();
	}
	public void OnLoadGame(){
		Debug.Log ("load press");
		SaveGame.Load ();
		Game_Manager.LoadGame();
	}
	public void OnSettings(){
		options.SetActive (true);
		mainMenu.SetActive (false);
		//tässä laita äänisliderit ja keybinding napit päälle ja muut napit pois
	}
	public void OnReturn(){
		options.SetActive (false);
		mainMenu.SetActive (true);
		//tässä laita äänisliderit ja keybinding napit pois ja muut napit päälle
	}
}
