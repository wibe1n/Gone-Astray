using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPTalkToFiaScript : MonoBehaviour
{
    public NPC NPCScript;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider player)
    {
        NPCScript.Canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider player)
    {
        NPCScript.Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
