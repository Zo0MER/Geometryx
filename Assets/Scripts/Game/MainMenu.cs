using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public Canvas StartCanvas;
    public Canvas LevelSelectionCanvas;

	// Use this for initialization
	void Start ()
	{
	    ShowStartMenu();
	}


    public void OpenLevel(int n)
    {
        Application.LoadLevel(n);
    }

    public void ShowLevelSelection()
    {
        LevelSelectionCanvas.enabled = true;
        StartCanvas.enabled = false;
    }

    public void ShowStartMenu()
    {
        LevelSelectionCanvas.enabled = false;
        StartCanvas.enabled = true;
    }
}
