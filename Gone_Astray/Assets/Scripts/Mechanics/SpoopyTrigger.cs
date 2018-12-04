using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoopyTrigger : MonoBehaviour {

    private FMOD.Studio.EventInstance spoopySounds;
    private FMOD.Studio.ParameterInstance spoopyParameter;
    public PencilContourEffect pencilEffects;
	
	void Start () {
        spoopySounds = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/AmbientMusic");
        spoopySounds.getParameter("Progression", out spoopyParameter);
        Debug.Log(spoopyParameter);
    }

    private void OnTriggerEnter(Collider player) {
        if (player.GetComponent<Character>()) {
            Debug.Log("täällä");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(spoopySounds, player.GetComponent<Transform>(), player.GetComponent<Rigidbody>());
            spoopySounds.start();
            spoopyParameter.setValue(0.2f);
            
        }
    }
}
