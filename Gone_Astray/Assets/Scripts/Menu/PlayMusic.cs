using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PlayMusic : MonoBehaviour
{



    private FMOD.Studio.EventInstance Music;



    private void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gone_Astray_theme_demo_3");

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Music, GetComponent<Transform>(), GetComponent<Rigidbody>());

        Music.start();


    }



    private void OnDestroy()
    {

        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    }

}
