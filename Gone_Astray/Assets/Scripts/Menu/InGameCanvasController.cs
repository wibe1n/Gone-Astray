using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvasController : MonoBehaviour {

    public GameObject fireflies;
    public bool disabled;
    public Animator animator;
    //public bool toggleCutscenes = true;

    private void Start()
    {
        disabled = false;
        animator = fireflies.GetComponent<Animator>();

        /*if (toggleCutscenes)
        {
            ToggleInGameCanvas(false);
        }
        else
        {
            ToggleInGameCanvas(true);
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowCanvas();
        }
    }

    public void ToggleInGameCanvas(bool usable)
    {
        if (usable)
            disabled = false;
        else
            disabled = true;
    }

    public void UpdateFlyAmount(int amount)
    {
        if (!disabled)
        {
            disabled = true;
            fireflies.GetComponentInChildren<Text>().text = amount.ToString();
            animator.SetBool("new", true);
            StartCoroutine(FireflyFoundRoutine());
        }
    }

    public void ShowCanvas()
    {
        if (!disabled)
        {
            if (animator != null)
            {
                if (!animator.GetBool("open"))
                {
                    animator.SetBool("open", true);
                    StartCoroutine(ShowCanvasRoutine());
                }
            }
        }
    }

    IEnumerator ShowCanvasRoutine()
    {
        yield return new WaitForSeconds(4);
        animator.SetBool("open", false);
    }

    IEnumerator FireflyFoundRoutine()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("new", false);
        disabled = false;
    }
}
