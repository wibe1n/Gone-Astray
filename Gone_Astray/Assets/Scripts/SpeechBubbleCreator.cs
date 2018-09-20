using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleCreator : MonoBehaviour {

    public GameObject speechbubble, continueImage, askCanvas;
    public Text bubbleText;

    public void GenerateSpeechBubble(NPC npc) {
        askCanvas.SetActive(false);
        NameType npcID = (NameType)npc.id;
        bubbleText.text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
        speechbubble.SetActive(true);
    }

    public void UpdateSpeechBubble(NPC npc) {
        npc.currentSpeechInstance += 1;
        NameType npcID = (NameType)npc.id;
        bubbleText.text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
    }

    public void CloseSpeechBubble(NPC npc) {
        speechbubble.SetActive(false);
    }

    public void SetSpeechInstance(NPC npc, int setInstance) {
        npc.currentSpeechInstance = setInstance;
    }

	
}
