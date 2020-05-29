using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public JournalController journalController;
    public InGameCanvasController igcController;
    public Character character;
	public Undying_Object undyObj;
	public KeyCode pauseKey = KeyCode.None;
	public KeyCode journalKey = KeyCode.None;
	public KeyCode altPauseKey = KeyCode.None;
	public GameObject pauseMenuCanvas, journalCanvas, mainPage, helpPage, QuitPopup, fireflyAmountTextPause, fireflyAmountTextJournal;
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

        if (pauseMenuCanvas.activeSelf == true)
        {
            pauseMenuCanvas.SetActive(false);
        }
        if (journalCanvas.activeSelf == true)
        {
            journalCanvas.SetActive(false);
        }

        journalShortcut = false;
    }

    void Update ()
    {
        //Jos painetaan pausenappia, niin aktivoidaan pausemenu
		if (Input.GetKeyDown(pauseKey) || Input.GetKeyDown(altPauseKey))
        {
            if (pauseMenuCanvas.activeSelf == false && journalCanvas.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                ActivatePauseMenu();
            }
            else if (journalCanvas.activeSelf == false)
            {
                ClosePauseMenu();
            }
        }
        //Jos painetaan journal nappia niin aktivoidaan journal
		if (Input.GetKeyDown(journalKey) && character.items[1])
        {
            if (pauseMenuCanvas.activeSelf == false && journalCanvas.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                //Katsotaan mistä journaliin on menty
                JournalShortcut();
                ActivateJournal();
            }
            else if (pauseMenuCanvas.activeSelf == false)
            {
                CloseJournal();
            }
        }
    }

    public void ActivatePauseMenu()
    {
        if (pauseMenuCanvas.activeSelf == true)
        {
            return;
        }
        //Pysäytetään peli
        Time.timeScale = 0;
        igcController.ToggleInGameCanvas(false);
        fireflyAmountTextPause.GetComponentInChildren<Text>().text = character.myFireflies.Count.ToString();
        pauseMenuCanvas.SetActive(true);

        //        inGameCanvas.enabled = false;
    }

    public void ClosePauseMenu()
    {
        if (pauseMenuCanvas.activeSelf == false)
        {
            return;
        }
        //Peli jatkuu
        Time.timeScale = 1;
        igcController.ToggleInGameCanvas(true);
        pauseMenuCanvas.SetActive(false);
        //        inGameCanvas.enabled = true;
    }

    public void ActivateJournal()
    {
        //avataan journal ja pysäytetään aika
		if (journalCanvas.activeSelf == true || !character.items[1])
        {
            return;
        }

        if (pauseMenuCanvas.activeSelf == true)
            pauseMenuCanvas.SetActive(false);

        Time.timeScale = 0;
        igcController.ToggleInGameCanvas(false);
        fireflyAmountTextJournal.GetComponentInChildren<Text>().text = character.myFireflies.Count.ToString();
        journalCanvas.SetActive(true);
        journalController.OpenJournal();
		//päivitetään tulikärpäset
		fireflies.UpdateFireflies ();
    }

    public void CloseJournal()
    {
        //Suljetaan journal ja jatketaan peliä
        if (journalCanvas.activeSelf == false)
        {
            return;
        }

        if (journalShortcut)
        {
            journalShortcut = false;
            Time.timeScale = 1;
            igcController.ToggleInGameCanvas(true);

            if (pauseMenuCanvas.activeSelf == true)
                pauseMenuCanvas.SetActive(false);
            journalCanvas.SetActive(false);
            //            inGameCanvas.enabled = true;
        }
        else
        {
            journalCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(true);
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
