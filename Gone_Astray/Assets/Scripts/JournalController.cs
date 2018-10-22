using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour {

    int leftPageNumber;
    int rightPageNumber;

    public GameObject forwardButton;
    public GameObject backButton;

    public GameObject frontPagePanel;
    public GameObject tableOfContentsPanel;
    public GameObject leftPageFrame;
    public GameObject rightPageFrame;

    // Use this for initialization
    public void OpenJournal () {
        leftPageNumber = 1;
        rightPageNumber = 2;
        backButton.gameObject.SetActive(false);
        leftPageFrame.gameObject.SetActive(false);
        rightPageFrame.gameObject.SetActive(false);
	}

    // Update is called once per frame
    public void Forward()
    {
        if (leftPageNumber == 1)
        {
            //poistutaan ekalta aukeamalta, sisällysluettelo jne kiinni ja sivut auki

            frontPagePanel.gameObject.SetActive(false);
            tableOfContentsPanel.gameObject.SetActive(false);

            leftPageFrame.gameObject.SetActive(true);
            rightPageFrame.gameObject.SetActive(true);
        }
        leftPageNumber += 2;
        rightPageNumber += 2;

        backButton.gameObject.SetActive(true);
        UpdatePageContents();
    }

    public void Back()
    {
        leftPageNumber -= 2;
        rightPageNumber -= 2;

        if (leftPageNumber == 1)
        {
            //palataan ekalle aukeamalle, sisällysluettelo jne auki ja sivut kiinni

            frontPagePanel.gameObject.SetActive(true);
            tableOfContentsPanel.gameObject.SetActive(true);

            leftPageFrame.gameObject.SetActive(false);
            rightPageFrame.gameObject.SetActive(false);

            backButton.gameObject.SetActive(false);
        }
        else
        {
            UpdatePageContents();
        }
    }

    public void UpdatePageContents()
    {
        //leftPageFrameen sivunumeron perusteella kuva

        //rightPageFrameen sivunumeron perusteella teksti ja otsikko
    }
}
