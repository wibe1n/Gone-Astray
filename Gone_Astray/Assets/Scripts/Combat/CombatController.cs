using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {


    public MenuController menuController;
    public Enemy myEnemy;
    public List<int> cardList = new List<int>() { };
    

    public void StartBlackJack(Enemy enemy, List<Firefly> fireflyList) {


    }

    void GenerateBlackJackDeck() {
        for (int i = 0; i < 4; i++) {
            cardList.Add(2);
            cardList.Add(3);
            cardList.Add(4);
            cardList.Add(5);
            cardList.Add(6);
            cardList.Add(7);
            cardList.Add(8);
            cardList.Add(9);
            cardList.Add(11);
        }
        for(int i = 0; i < 16; i++) {
            cardList.Add(10);
        }
    }

    void ShuffleDeck() {
        for (int i = cardList.Count - 1; i > 0; --i) {
            int k = Random.Range(0, i);
            int temp = cardList[i];
            cardList[i] = cardList[k];
            cardList[k] = temp;
        }
    }
}
