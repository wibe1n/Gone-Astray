using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public Canvas pauseMenuCanvas;
    public Canvas journalCanvas;
//    public Canvas inGameCanvas;
    public JournalController journalController;
	public Undying_Object undyObj;
	public KeyCode pauseKey = KeyCode.None;
	public KeyCode journalKey = KeyCode.None;
	public KeyCode altPauseKey = KeyCode.None;
	public GameObject mainPage;
	public GameObject helpPage;
	public GameObject QuitPopup;
	public HelpPage helpScript;
	public FireflyAmount fireflies;

    private bool journalShortcut = false;

	void Start () {
		if (GameObject.FindGameObjectWithTag ("UndyingObject") != null) {
			undyObj = GameObject.FindGameObjectWithTag ("UndyingObject").GetComponent<Undying_Object> ();
			if (undyObj.pauseKey == KeyCode.None)
				pauseKey = KeyCode.P;
			else
				pauseKey = undyObj.pauseKey;
			if (undyObj.altPauseKey == KeyCode.None)
				altPauseKey = KeyCode.Escape;
			else
				altPauseKey = undyObj.altPauseKey;
			if (undyObj.journalKey == KeyCode.None)
				journalKey = KeyCode.J;
			else
				journalKey = undyObj.journalKey;
		} else {
			journalKey = KeyCode.J;
			pauseKey = KeyCode.P;
			altPauseKey = KeyCode.Escape;
		}

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

    void Update ()
    {
        //Jos painetaan pausenappia, niin aktivoidaan pausemenu
		if (Input.GetKeyDown(pauseKey) || Input.GetKeyDown(altPauseKey))
        {
            if (pauseMenuCanvas.enabled == false && journalCanvas.enabled == false)
            {
                Cursor.lockState = CursorLockMode.None;
                ActivatePauseMenu();
            }
            else if (journalCanvas.enabled == false)
            {
                ClosePauseMenu();
            }
        }
        //Jos painetaan journal nappia niin aktivoidaan journal
		if (Input.GetKeyDown(journalKey))
        {
            if (pauseMenuCanvas.enabled == false && journalCanvas.enabled == false)
            {
                Cursor.lockState = CursorLockMode.None;
                //Katsotaan mistä journaliin on menty
                JournalShortcut();
                ActivateJournal();
            }
            else if (pauseMenuCanvas.enabled == false)
            {
                CloseJournal();
            }
        }
    }

    public void ActivatePauseMenu()
    {
        if (pauseMenuCanvas.enabled == true)
        {
            return;
        }
        //Pysäytetään peli
        Time.timeScale = 0;
        pauseMenuCanvas.enabled = true;
//        inGameCanvas.enabled = false;
    }

    public void ClosePauseMenu()
    {
        if (pauseMenuCanvas.enabled == false)
        {
            return;
        }
        //Peli jatkuu
        Time.timeScale = 1;
        pauseMenuCanvas.enabled = false;
//        inGameCanvas.enabled = true;
    }

    public void ActivateJournal()
    {
        //avataan journal ja pysäytetään aika
        if (journalCanvas.enabled == true)
        {
            return;
        }

        if (pauseMenuCanvas.enabled == true)
            pauseMenuCanvas.enabled = false;

        if (pauseMenuCanvas.enabled == true)
        {
//            inGameCanvas.enabled = false;
        }

        Time.timeScale = 0;
        journalCanvas.enabled = true;
        journalController.OpenJournal();
		//päivitetään tulikärpäset
		fireflies.UpdateFireflies ();
    }

    public void CloseJournal()
    {
        //Suljetaan journal ja jatketaan peliä
        if (journalCanvas.enabled == false)
        {
            return;
        }

        if (journalShortcut)
        {
            journalShortcut = false;
            Time.timeScale = 1;

            if (pauseMenuCanvas.enabled == true)
                pauseMenuCanvas.enabled = false;
            journalCanvas.enabled = false;
//            inGameCanvas.enabled = true;
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
	public void QuitOn()
	{
		QuitPopup.SetActive (true);
	}
	public void QuitOff()
	{
		QuitPopup.SetActive (false);
	}
    public void GotoMainMenu()
    {
        Time.timeScale = 1;
        Game_Manager.StartMenu();
    }
	public void HelpOn(){
		helpPage.SetActive (true);
		helpScript.HelpPageOn ();
		mainPage.SetActive (false);
	}
	public void HelpOff(){
		helpPage.SetActive (false);
		mainPage.SetActive (true);
	}
}
