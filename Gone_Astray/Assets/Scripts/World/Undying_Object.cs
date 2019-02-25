using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Undying_Object : MonoBehaviour {

    //sisältää keybindingit
	
	public GameObject loadingScreen;
	public int whichScene = 0;
	[HideInInspector]
	public KeyCode pauseKey;
	public KeyCode journalKey;
	public KeyCode altPauseKey;
	public KeyCode leshenKey;
	public KeyCode crouchKey;
	public KeyCode jumpKey;
	public KeyCode talkKey;
	
    //Alussa aloitetaan valittu (yleensä menu) skene, ladataan data ja asetetaan default näppäimet
	void Start () {
        DontDestroyOnLoad(this);
        StartCoroutine(StartMenuScreen());
        DataManager.ReadDataString("nonexistent");
		setDefaultKeys ();
    }

    public void Level1() {
        StartCoroutine(ToTheWorld());
    }

	public void setDefaultKeys(){
		pauseKey = KeyCode.P;
		journalKey = KeyCode.J;
		altPauseKey = KeyCode.Escape;
		leshenKey = KeyCode.L;
		crouchKey = KeyCode.C;
		jumpKey = KeyCode.Space;
		talkKey = KeyCode.E;
	}

	private IEnumerator ToTheWorld() {
		switch (whichScene) {
		case 1:
			yield return SceneManager.LoadSceneAsync ("tiinatest");
			break;
		case 2:
			yield return SceneManager.LoadSceneAsync ("TutorialLevel");
			break;
		default:
			yield return SceneManager.LoadSceneAsync ("DemoTestLevel1");
			break;
		}

        //Testivaiheen jälkeen ladataan ensiksi varsinainen taso, mutta itse scene on tyhjä

        //string loadPath = "Levels/tutorial";
        //ResourceRequest levelRequest;
        //GameObject nextLevel;
        //levelRequest = Resources.LoadAsync(loadPath);
        //yield return levelRequest;
        //nextLevel = (GameObject)levelRequest.asset;
        //Debug.Log(nextLevel);
        //Instantiate(nextLevel);
    }

    public void Menu() {
        StartCoroutine(StartMenuScreen());
    }

    private IEnumerator StartMenuScreen() {
        yield return SceneManager.LoadSceneAsync("Menu");
    }

    public void Tutorial() {
        StartCoroutine(StartTutorial());
    }

    private IEnumerator StartTutorial() {
        yield return SceneManager.LoadSceneAsync("Tutorial");
		loadingScreen.SetActive (false);
    }


    
    public void LoadGame() {
        StartCoroutine(LoadGameInstance());
    }

    //Ladataan peli ja haetaan siitä tiedot, jotka asetetaan pelaajalle ja maailmalle
    private IEnumerator LoadGameInstance() {
		//loadingScreen.SetActive (true);
        yield return SceneManager.LoadSceneAsync("VillenWorldTest1");
        Debug.Log(SaveGame.Instance.playerPosition);
        string loadPath = "Levels/level" + SaveGame.Instance.level.ToString();
        ResourceRequest levelRequest;
        GameObject nextLevel;
        levelRequest = Resources.LoadAsync(loadPath);
        yield return levelRequest;
        nextLevel = (GameObject)levelRequest.asset;
        Debug.Log(nextLevel);
        Instantiate(nextLevel);
        GameObject chara = GameObject.FindGameObjectWithTag("Player");
        GameObject camera = GameObject.FindGameObjectWithTag("CameraRig");
        camera.transform.position = SaveGame.Instance.cameraPosition;
        chara.GetComponent<Character>().transform.position = SaveGame.Instance.playerPosition;
        chara.GetComponent<Character>().myFireflies = SaveGame.Instance.fireflies;
        chara.GetComponent<Character>().fiaFamily = SaveGame.Instance.fiaFamily;
		loadingScreen.SetActive (false);
    }
}
