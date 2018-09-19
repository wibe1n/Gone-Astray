using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Undying_Object : MonoBehaviour {

	
	void Start () {
        DontDestroyOnLoad(this);
        StartCoroutine(StartMenuScreen());
        DataManager.ReadDataString("nonexistent");
        string text1 = NameDescContainer.GetChapterPart("part1", NameType.chapter1);
        Debug.Log(text1);
        string text2 = NameDescContainer.GetChapterPart("part2", NameType.chapter1);
        Debug.Log(text2);
        string text3 = NameDescContainer.GetSpeechBubble("part1", NameType.npc1);
        Debug.Log(text3);
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
}
