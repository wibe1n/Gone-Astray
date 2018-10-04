using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDestroyer : MonoBehaviour {

    public GameObject previousLevel;

    private void OnTriggerEnter(Collider other) {
        Destroy(previousLevel);
    }
}
