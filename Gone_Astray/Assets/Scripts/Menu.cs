using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Character chara;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
		chara.transform.position = SaveGame.Instance.playerPosition;
		chara.myFireflies = SaveGame.Instance.fireflies;
		chara.fiaFamily = SaveGame.Instance.fiaFamily;
		Game_Manager.StartLevel1();
	}
}
