using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public Canvas pauseMenuCanvas;
    public Canvas journalCanvas;
    public Canvas inGameCanvas;
    public JournalController journalController;

    private bool journalShortcut = false;

	// Use this for initialization
	void Start () {
        if (pauseMenuCanvas.enabled == true)
        {
            pauseMenuCanvas.enabled = false;
        }
        if (journalCanvas.enabled == true)
        {
            journalCanvas.enabled = false;
        }

        journalShortcut = false;
    }

    public void ActivatePauseMenu()
    {
        if (pauseMenuCanvas.enabled == true)
        {
            return;
        }

        Time.timeScale = 0;
        pauseMenuCanvas.enabled = true;
        inGameCanvas.enabled = false;
    }

    public void ClosePauseMenu()
    {
        if (pauseMenuCanvas.enabled == false)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pauseMenuCanvas.enabled = false;
        inGameCanvas.enabled = true;
    }

    public void ActivateJournal()
    {
        if (journalCanvas.enabled == true)
        {
            return;
        }

        if (pauseMenuCanvas.enabled == true)
            pauseMenuCanvas.enabled = false;

        if (pauseMenuCanvas.enabled == true)
            inGameCanvas.enabled = false;

        Time.timeScale = 0;
        journalCanvas.enabled = true;
        journalController.OpenJournal();
    }

    public void CloseJournal()
    {
        if (journalCanvas.enabled == false)
        {
            return;
        }

        if (journalShortcut)
        {
            journalShortcut = false;

            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

            if (pauseMenuCanvas.enabled == true)
                pauseMenuCanvas.enabled = false;
            journalCanvas.enabled = false;
            inGameCanvas.enabled = true;
        }
        else
        {
            journalCanvas.enabled = false;
            pauseMenuCanvas.enabled = true;
        }
    }

    public void JournalShortcut()
    {
        journalShortcut = true;
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
