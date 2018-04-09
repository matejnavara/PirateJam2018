using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

    public Canvas settingsMenu;
    public Canvas quitMenu;

    public Button startButton;
    public Button settingsButton;
    public Button quitButton;

    public Button noButton;

	// Use this for initialization
	void Start () {

        settingsMenu = settingsMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();

        startButton = startButton.GetComponent<Button>();
        settingsButton = settingsButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();

        settingsMenu.enabled = false;
        quitMenu.enabled = false;
	
	}

    //LEVEL SELECT MENU
    public void PlayPressed()
    {
        startButton.enabled = false;
        settingsButton.enabled = false;
        quitButton.enabled = false;
    }

    //return to main menu
    public void BackPressed()
    {
        startButton.enabled = true;
        settingsButton.enabled = true;
        quitButton.enabled = true;
    }

    public void Play()
    {
        int level = (int)Random.Range(1, 1);
        Debug.Log("Loading level " + level);
        SceneManager.LoadScene(level);
    }

    //SETTINGS MENU
    public void SettingsPressed()
    {
        settingsMenu.enabled = true;
        startButton.enabled = false;
        settingsButton.enabled = false;
        quitButton.enabled = false;
    }

    //return to main menu
    public void SettingsClosePressed()
    {
        settingsMenu.enabled = false;
        startButton.enabled = true;
        settingsButton.enabled = true;
        quitButton.enabled = true;
    }


    //QUIT MENU
    public void ExitPressed()
    {
        quitMenu.enabled = true;
        startButton.enabled = false;
        settingsButton.enabled = false;
        quitButton.enabled = false;
        noButton.Select();
    }

    //return to main menu
    public void NoPressed()
    {
        quitMenu.enabled = false;
        startButton.enabled = true;
        settingsButton.enabled = true;
        quitButton.enabled = true;
        startButton.Select();
    }

    //exit application
    public void YesPressed()
    {
        Application.Quit();
    }


}
