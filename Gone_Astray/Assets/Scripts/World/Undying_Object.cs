using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Undying_Object : MonoBehaviour {

	
	void Start () {
        DontDestroyOnLoad(this);
        StartCoroutine(StartMenuScreen());
        DataManager.ReadDataString("nonexistent");
        Debug.Log(Application.persistentDataPath);
    }

    public void Level1() {
        StartCoroutine(ToTheWorld());
    }
	
	private IEnumerator ToTheWorld() {
        yield return SceneManager.LoadSceneAsync("VillenWorldTest1");
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
    }

    public void LoadGame() {
        StartCoroutine(LoadGameInstance());
    }

    private IEnumerator LoadGameInstance() {
        yield return SceneManager.LoadSceneAsync("VillenWorldTest1");
        string loadPath = "Levels/" + SaveGame.Instance.level.ToString();
        ResourceRequest levelRequest;
        GameObject nextLevel;
        levelRequest = Resources.LoadAsync(loadPath);
        yield return levelRequest;
        nextLevel = (GameObject)levelRequest.asset;
        Debug.Log(nextLevel);
        Instantiate(nextLevel);
        GameObject chara = GameObject.FindGameObjectWithTag("player");
        chara.GetComponent<Character>().transform.position = SaveGame.Instance.playerPosition;
        chara.GetComponent<Character>().myFireflies = SaveGame.Instance.fireflies;
        chara.GetComponent<Character>().fiaFamily = SaveGame.Instance.fiaFamily;
    }
}
