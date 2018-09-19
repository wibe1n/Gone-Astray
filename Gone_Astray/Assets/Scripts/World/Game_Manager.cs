using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_Manager  {

    public  static void StartLevel1() {
        GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>().Level1();
    }

    public static void StartTutorial() {
        GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>().Tutorial();
    }

    public static void StartMenu() {
        GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>().Menu();
    }
}
