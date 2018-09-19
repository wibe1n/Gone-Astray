using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleCreator : MonoBehaviour{

    

    public void GenerateSpeechBubble(NPC npc) {        
        GameObject newSpeechBubble = Instantiate 
            (Resources.Load("NpcResources/SpeechBubble"), transform.position, Quaternion.identity, npc.speechBubbleParent.transform) as GameObject;
        npc.currentSpeechBubble = newSpeechBubble;
        NameType npcID = (NameType)npc.id;
        npc.currentSpeechBubble.GetComponent<SpeechBubble>().imageText.text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
        
    }

    public void UpdateSpeechBubble(NPC npc) {
        npc.currentSpeechInstance += 1;
        NameType npcID = (NameType)npc.id;
        npc.currentSpeechBubble.GetComponent<SpeechBubble>().imageText.text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
    }

    public void CloseSpeechBubble(NPC npc) {
        Destroy(npc.currentSpeechBubble);
    }

	
}
