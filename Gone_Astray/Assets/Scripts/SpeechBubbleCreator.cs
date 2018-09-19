using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SpeechBubbleCreator {

    

    public static void GenerateSpeechBubble(NPC npc) {
        NameType npcID = (NameType)npc.id;
        Debug.Log(npc.Canvas.GetComponentInChildren<SpeechBubble>());
        npc.Canvas.GetComponentInChildren<Text>().text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
    }

    public static void UpdateSpeechBubble(NPC npc) {
        npc.currentSpeechInstance += 1;
        NameType npcID = (NameType)npc.id;
        npc.Canvas.GetComponent<Text>().text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
    }

    public static void CloseSpeechBubble(NPC npc) {
        npc.Canvas.SetActive(false);
    }

    public static void SetSpeechInstance(NPC npc, int setInstance) {
        npc.currentSpeechInstance = setInstance;
    }

	
}
