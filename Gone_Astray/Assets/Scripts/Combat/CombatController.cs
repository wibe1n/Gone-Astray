using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    public MovementControls playerControls;
    public MenuController menuController;
    public Enemy myEnemy;
    public List<Firefly> myFireflies;
    public List<int> deck = new List<int>() { };
    public List<int> enemyHand;
    public List<int> myHand;
    public Canvas gameCanvas;
    

    public void StartBlackJack(Enemy enemy, List<Firefly> fireflyList) {
        myEnemy = enemy;
        myFireflies = fireflyList;
        GenerateBlackJackDeck();
        ShuffleDeck();
        playerControls.enabled = false;
        //TODO: Turn camera to make player feel small
        enemyHand.Add(deck[0]);
        deck.RemoveAt(0);
        myHand.Add(deck[0]);
        deck.RemoveAt(0);
        enemyHand.Add(deck[0]);
        deck.RemoveAt(0);
        myHand.Add(deck[0]);
        deck.RemoveAt(0);
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
