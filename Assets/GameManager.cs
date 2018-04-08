using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Level level;
    private int index;

    //Players
    private Player player;
    private Baby baby;

    //UI Elements
    public GameObject pauseButton;
    public GameObject playerWinPanel;
    public GameObject babyWinPanel;

    //Game Logic Elements
    public bool countDown;
    public bool gameOver;
    public bool paused;

    //Game Audio
    private bool music;
    private bool sfx;

    public AudioSource audioBG;
    public AudioClip soundBG;
    private static GameManager manager;

    public static GameManager Manager
    {
        get { return manager; }
    }

    void Awake()
    {
        GetThisGameManager();
        soundCheck();
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
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        Reset();
        print("START again");
        print("Level name: " + level.getName());
        print("Level index: " + index);
    }

    void Update()
    {

        if (music)
        {
            //if (!audioBG.isPlaying) { audioBG.PlayOneShot(soundBG); }
        }
        else if (!music)
        {
            //audioBG.Stop();
        }

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
        string m = PlayerPrefs.GetString("Music");
        string s = PlayerPrefs.GetString("Sfx");

        if (m == null || m == "on") { music = true; } else { music = false; }
        if (s == null || s == "on") { sfx = true; } else { sfx = false; }

    }

    public void soundOn(string x)
    {
        if (x == "music") { music = true; }
        if (x == "sfx") { sfx = true; }
    }

    public void soundOff(string x)
    {
        if (x == "music") { music = false; }
        if (x == "sfx") { sfx = false; }
    }

    //GAME OVER METHODS
    //Called upon gameover, disable Player/HUD and display game over panel with final score/play again button/main menu button
    void PlayerWin()
    {
        gameOver = true;
        paused = true;
        playerWinPanel.SetActive(true);
        ActivateButtons();

    }

    void BabyWin()
    {
        gameOver = true;
        paused = true;
        babyWinPanel.SetActive(true);
        ActivateButtons();
        
    }

    void ActivateButtons()
    {
        //playagainButton.onClick.AddListener(delegate { Reset(); });
        //mainmenuButton.onClick.AddListener(delegate { MainMenu(); });
    }

    //Public bool to check for gameover condition
    public bool isGameOver()
    {
        return gameOver;
    }

    //Resets the game loop upon pressing play again
    public void Reset()
    {
        level = GameObject.Find("Level").GetComponent<Level>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<Baby>();
        countDown = false;
        gameOver = false;
        paused = false;
        index = level.getIndex();
        playerWinPanel.SetActive(false);
        babyWinPanel.SetActive(false);

        //audioBG = gameObject.GetComponent<AudioSource>();
        //soundBG = (AudioClip)Instantiate(Resources.Load(level.getMusic()));   
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Returns to main menu upon pressing main menu
    public void MainMenu()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }
}
