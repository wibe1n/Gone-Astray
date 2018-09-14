using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartGame(){
		Debug.Log ("start press");
		SceneManager.LoadScene("Level1");
		//must add scene to build from File>Build Settings before it can be loaded from here
	}

	public void OnQuit(){
		//ei toimi inspectorissa, toimii buildissa
		Debug.Log ("quit");
		Application.Quit();
	}
}
