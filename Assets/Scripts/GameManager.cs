using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Level level;
    private int index;

    //Players
    public Player player;
    public Baby baby;

    //UI Elements
    public GameObject playerWinPanel;
    public GameObject babyWinPanel;
    public GameObject replayButton;
    public GameObject mainmenuButton;

    //Game Logic Elements
    public bool countDown;
    public bool gameOver;
    public bool paused;

    //Game Audio
    public AudioSource audioSource;
    public AudioClip startSound;
    public AudioClip playerVictory;
    public AudioClip babyVictory;
    //private bool music;
    //private bool sfx;

    private static GameManager manager;

    public static GameManager Manager
    {
        get { return manager; }
    }

    void Start()
    {
        GetThisGameManager();
        Reset();
        soundCheck();
        audioSource.PlayOneShot(startSound);
    }

    void GetThisGameManager()
    {
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            manager = this;
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        //if (music)
        //{
        //    if (!audioBG.isPlaying) { audioBG.PlayOneShot(soundBG); }
        //}
        //else if (!music)
        //{
        //    audioBG.Stop();
        //}

        if (!gameOver)
        {
            if (!player.IsAlive())
            {
                BabyWin();
            }

            if (!baby.IsAlive())
            {
                PlayerWin();
            }
        }

    }

    //Sound methods
    void soundCheck()
    {
        audioSource = GetComponent<AudioSource>();
        //string m = PlayerPrefs.GetString("music");
        //string s = PlayerPrefs.GetString("sfx");

        //if (m == null || m == "on") { music = true; } else { music = false; }
        //if (s == null || s == "on") { sfx = true; } else { sfx = false; }
    }

    //public void soundOn(string x)
    //{
    //    if (x == "music") { music = true; }
    //    if (x == "sfx") { sfx = true; }
    //}

    //public void soundOff(string x)
    //{
    //    if (x == "music") { music = false; }
    //    if (x == "sfx") { sfx = false; }
    //}

    //GAME OVER METHODS
    //Called upon gameover, disable Player/HUD and display game over panel with final score/play again button/main menu button
    void PlayerWin()
    {
        Debug.Log("PLAYER WIN!");
        gameOver = true;
        paused = true;
        playerWinPanel.SetActive(true);
        PlaySoundWithCallback(playerVictory, ActivateButtons);
    }

    void BabyWin()
    {
        Debug.Log("BABY WIN!");
        gameOver = true;
        paused = true;
        babyWinPanel.SetActive(true);
        PlaySoundWithCallback(babyVictory, ActivateButtons);       
    }

    void ActivateButtons(bool foo)
    {
        replayButton.SetActive(foo);
        mainmenuButton.SetActive(foo);
        replayButton.GetComponent<Button>().Select();
    }

    void ActivateButtons()
    {
        replayButton.SetActive(true);
        mainmenuButton.SetActive(true);
        replayButton.GetComponent<Button>().Select();
    }

    //Public bool to check for gameover condition
    public bool isGameOver()
    {
        return gameOver;
    }

    //Resets the game loop upon pressing play again
    public void Reset()
    {
        Debug.Log("GM RESET");
        NullCheck();
        player.PlayerReset();
        baby.BabyReset();
        gameOver = false;
        paused = false;
        index = level.getIndex();
        playerWinPanel.SetActive(false);
        babyWinPanel.SetActive(false);
        ActivateButtons(false);
        //audioBG = gameObject.GetComponent<AudioSource>();
        //soundBG = (AudioClip)Instantiate(Resources.Load(level.getMusic()));   

        print("START again");
        print("Level name: " + level.getName());
        print("Level index: " + index);
    }

    public void NullCheck()
    {
        if (level == null) { level = GameObject.Find("Level").GetComponent<Level>(); }
        if (player == null) { player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); }
        if (baby == null) { baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<Baby>(); }
        if (playerWinPanel == null) { playerWinPanel = GameObject.Find("PlayerWinPanel"); }
        if (babyWinPanel == null) { babyWinPanel = GameObject.Find("BabyWinPanel"); }
        if (replayButton == null) { replayButton = GameObject.Find("ReplayButton"); }
        if (mainmenuButton == null) { mainmenuButton = GameObject.Find("MainMenuButton"); }
    }

    public void PlayAgain()
    {
        int level = (int)Random.Range(1, 1);
        Debug.Log("Loading level " + level);
        SceneManager.LoadScene(level);
    }

    //Returns to main menu upon pressing main menu
    public void MainMenu()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }

    public delegate void AudioCallback();

    public void PlaySoundWithCallback(AudioClip clip, AudioCallback callback)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(DelayedCallback(clip.length, callback));
    }
    private IEnumerator DelayedCallback(float time, AudioCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}
