using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvasController : MonoBehaviour {

    public GameObject fireflies;
    public bool disabled;

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
        fireflies.GetComponentInChildren<Text>().text = amount.ToString();
    }

    public void ShowCanvas()
    {
        if (!disabled)
        {
            Animator animator = fireflies.GetComponent<Animator>();
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
        Animator animator = fireflies.GetComponent<Animator>();

        yield return new WaitForSeconds(4);
        animator.SetBool("open", false);
    }
}
