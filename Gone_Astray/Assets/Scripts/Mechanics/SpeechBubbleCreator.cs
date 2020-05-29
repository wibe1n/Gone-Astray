using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleCreator : MonoBehaviour {

    public GameObject speechbubble, askCanvas;
    public Text bubbleText;

    //Tekstit haetaan teksticontainerista allaolevista funktioista
    public void GenerateSpeechBubble(NPC npc) {
        Debug.Log("current " + npc.currentSpeechInstance);
        askCanvas.SetActive(false);
        NameType npcID = (NameType)npc.id;
        bubbleText.text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
        speechbubble.SetActive(true);
        if (npc.currentSpeechInstance >= 31)
            Debug.Log(bubbleText.text);
    }

    public void UpdateSpeechBubble(NPC npc) {
        Debug.Log("current2 " + npc.currentSpeechInstance);
        npc.currentSpeechInstance += 1;
        NameType npcID = (NameType)npc.id;
        bubbleText.text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
    }

    public void BackWards(NPC npc) {
        if (npc.currentSpeechInstance == 1){ }
        else{
            npc.currentSpeechInstance -= 1;
            NameType npcID = (NameType)npc.id;
            bubbleText.text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
        }
        
    }

    public void GenerateInfoBox(InteractObject target) {
        int itemID = target.itemIndex;
        bubbleText.text = NameDescContainer.GetDescription(NameType.item, itemID);
        speechbubble.SetActive(true);
    }

    public void CloseInfoBox() {
        speechbubble.SetActive(false);
    }

    public void CloseSpeechBubble(NPC npc) {
        if(npc.id != 6) {
            npc.currentSpeechInstance = 1;
        }
        speechbubble.SetActive(false);
    }

    public void SetSpeechInstance(NPC npc, int setInstance) {
        npc.currentSpeechInstance = setInstance;
    }

    public bool StillTalking()
    {
        return speechbubble.activeSelf;
    }

    public void WentTooFar(NPC npc)
    {
        NameType npcID = (NameType)npc.id;
        bubbleText.text = NameDescContainer.GetSpeechBubble("part" + 0, npcID);
    }
}
