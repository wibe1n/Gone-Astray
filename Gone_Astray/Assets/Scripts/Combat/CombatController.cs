using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour {

    public GameObject player;
    public Character character;
    public MenuController menuController;
    public Enemy myEnemy;
    public List<Firefly> myFireflies;
    public List<int> deck = new List<int>() { };
    public List<int> enemyHand;
    public List<int> myHand;
    public GameObject gameCanvas, textPanel, runButton, approachButton, fireflyIcon, darknessIcon;
    public GameObject runAwayScreen;
    public bool reached = false;
    
    public void StartEncounter(Enemy enemy, List<Firefly> fireflyList) {

        //TODO: Turn camera to make player feel small
        myEnemy = enemy;
        myFireflies = fireflyList;
        gameCanvas.SetActive(true);
    }

    public void StartBlackJack() {
        textPanel.SetActive(false);
        runButton.SetActive(false);
        approachButton.SetActive(false);
        fireflyIcon.SetActive(true);
        darknessIcon.SetActive(true);
        GenerateBlackJackDeck();
        ShuffleDeck();
        enemyHand.Add(deck[0]);
        deck.RemoveAt(0);
        myHand.Add(deck[0]);
        deck.RemoveAt(0);
        enemyHand.Add(deck[0]);
        deck.RemoveAt(0);
        myHand.Add(deck[0]);
        deck.RemoveAt(0);
        menuController.PlayersTurn();
    }

    public void RunAway() {
        StartCoroutine(RunAwayRoutine());
    }

    IEnumerator RunAwayRoutine() {
        runAwayScreen.SetActive(true);
        runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
        //TODO: fancy effects for running away
        yield return new WaitForSeconds(1f);
        player.transform.position = myEnemy.checkpoint.transform.position;
        character.inCombat = false;
        gameCanvas.SetActive(false);
        runAwayScreen.GetComponentInChildren<Image>().CrossFadeAlpha(0.0f, 3.0f, false);
    }

    void GenerateBlackJackDeck() {
        for (int i = 0; i < 4; i++) {
            deck.Add(2);
            deck.Add(3);
            deck.Add(4);
            deck.Add(5);
            deck.Add(6);
            deck.Add(7);
            deck.Add(8);
            deck.Add(9);
            deck.Add(11);
        }
        for(int i = 0; i < 16; i++) {
            deck.Add(10);
        }
    }

    void ShuffleDeck() {
        for (int i = deck.Count - 1; i > 0; --i) {
            int k = Random.Range(0, i);
            int temp = deck[i];
            deck[i] = deck[k];
            deck[k] = temp;
        }
    }
}
