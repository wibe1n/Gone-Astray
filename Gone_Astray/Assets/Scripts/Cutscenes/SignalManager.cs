﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    int signal;
    public NPC FiaNpcScript;

    void Start()
    {
        signal = 1;
    }

    private void Update()
    {
    }

    public void NextSpeechSignal()
    {
        if (FiaNpcScript.TutorialCutsceneScript.cutsceneFinished != true)
        {
            switch (signal)
            {
                case 1:
                    FiaNpcScript.NextSpeechFromCutscene(40, 40);
                    FiaNpcScript.nextSpeechButton.SetActive(false);
                    break;
                case 2:
                case 4:
                    FiaNpcScript.TalkEvent();
                    break;
                case 3:
                    FiaNpcScript.NextSpeechFromCutscene(41, 41);
                    break;
                case 5:
                    FiaNpcScript.NextSpeechFromCutscene(42, 43);
                    FiaNpcScript.nextSpeechButton.SetActive(true);
                    break;
            }

            signal++;
        }
    }
}