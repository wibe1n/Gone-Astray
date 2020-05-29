using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTriggerArea : MonoBehaviour
{
    public int triggerId;
    public TutoLvl tutorialCutsceneScript;
    public NPC FiaNpcScript;
    public GameObject QuestionMarkCanvas;

    private Undying_Object undyObj;
    public KeyCode pickKey = KeyCode.None;

    //Haetaan keybindingit
    void Start()
    {

        if (GameObject.FindGameObjectWithTag("UndyingObject") != null)
        {
            undyObj = GameObject.FindGameObjectWithTag("UndyingObject").GetComponent<Undying_Object>();
            if (undyObj.talkKey == KeyCode.None)
                pickKey = KeyCode.E;
            else
                pickKey = undyObj.talkKey;
        }
        else
            pickKey = KeyCode.E;
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.GetComponent<Character>() != null)
        {
            if (triggerId == 3 || triggerId == 7)
            {
                QuestionMarkCanvas.SetActive(true);
            }
            else if (triggerId == 4 || triggerId == 5 || triggerId == 6 || triggerId == 8)
            {
                FiaNpcScript.FiaChild.SetActive(false);
                tutorialCutsceneScript.PlayNextCutscene(triggerId);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if (player.gameObject.GetComponent<Character>() != null)
        {
            if (Input.GetKeyDown(pickKey))
            {
                QuestionMarkCanvas.SetActive(false);
                switch (triggerId)
                {
                    case 3:
                        FiaNpcScript.FiaChild.SetActive(false);
                        tutorialCutsceneScript.PlayNextCutscene(triggerId);
                        break;
                    case 7:
                        FiaNpcScript.NextSpeechFromCutscene(31, 32);
                        break;
                    default:
                        //Error id
                        break;
                }
                Destroy(gameObject);
            }
        }

    }

    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.GetComponent<Character>() != null)
        {
            if (QuestionMarkCanvas.activeSelf == true)
                QuestionMarkCanvas.SetActive(false);
        }

    }
}
