using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public CombatController combatController;
    public bool proceed;
    public GameObject enemyHand, myHand;
    public int myHandNumber, enemyHandNumber;

    public void PlayersTurn() {
        myHandNumber = 0;
        enemyHandNumber = 0;
        StartCoroutine(PlayerTurn());
    }


    //Players turn routine
    IEnumerator PlayerTurn() {
        for (int i = 0; i < combatController.myHand.Count; i++) {
            myHandNumber += combatController.myHand[i];
        }
        myHand.GetComponent<Text>().text = myHandNumber.ToString();
        //TODO: animation for adding the hand
        yield return new WaitForSeconds(1);
        for (int i = 0; i < combatController.enemyHand.Count; i++) {
            enemyHandNumber += combatController.enemyHand[i];
        }
        enemyHand.GetComponent<Text>().text = enemyHandNumber.ToString();
        //TODO: animation for adding the hand
        yield return new WaitForSeconds(1);
    }

    void AddFirefly() {

    }
}
