using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenuScript : MonoBehaviour {

    public bool music;
    public bool sfx;


    // Use this for initialization
    void Start () {

        PlayerPrefs.SetString("music", "on");
        music = true; 
        PlayerPrefs.SetString("sfx", "on");
        sfx = true;
    }

    public void Toggle(string x)
    {
        if (x == "music") { if (music) { music = false; PlayerPrefs.SetString("music", "off"); } else { music = true; PlayerPrefs.SetString("music", "on"); } }
        if (x == "sfx") { if (sfx) { sfx = false; PlayerPrefs.SetString("sfx", "off"); } else { sfx = true; PlayerPrefs.SetString("sfx", "on"); } }

    }


}
