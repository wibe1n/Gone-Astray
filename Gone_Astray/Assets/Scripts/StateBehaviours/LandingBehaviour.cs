using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingBehaviour : StateMachineBehaviour {


    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Debug.Log("maassa");
        FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Movement/Land");
        e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(animator.gameObject.transform.position));
        e.start();
        e.release();

    }


}
