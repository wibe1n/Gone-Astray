using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public string loadPath;
    private ResourceRequest levelRequest;
    private GameObject nextLevel;

    private void OnTriggerEnter(Collider col) {
        AsyncLoadLevel();
    }

    public void AsyncLoadLevel() {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad() {
        levelRequest = Resources.LoadAsync(loadPath);
        yield return levelRequest; 
        nextLevel = (GameObject)levelRequest.asset;
        Instantiate(nextLevel);
    }

}
