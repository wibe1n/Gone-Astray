using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    public void StartLevel1() {
        GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>().Level1();
    }

    public void StartTutorial() {
        GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>().Tutorial();
    }

    public void StartMenu() {
        GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>().Menu();
    }
}
