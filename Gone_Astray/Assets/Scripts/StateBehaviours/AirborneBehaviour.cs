using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneBehaviour : StateMachineBehaviour {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Movement/Jump");
        e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(animator.gameObject.transform.position));
        e.start();
        e.release();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Movement/Land");
        e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(animator.gameObject.transform.position));
        e.start();
        e.release();

    }


}
