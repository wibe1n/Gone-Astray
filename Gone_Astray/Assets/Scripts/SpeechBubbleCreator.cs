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
        npc.currentSpeechBubble.GetComponent<SpeechBubble>().text = NameDescContainer.GetSpeechBubble("part" + npc.currentSpeechInstance, npcID);
        
    }

    public void UpdateSpeechBubble(NPC npc) {


    }

    public void CloseSpeechBubble(NPC npc) {


    }

	
}
