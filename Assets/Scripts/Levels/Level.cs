using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{

    public string levelName;
    public int levelIndex;
    public string levelMusic;

    public string getName() { return levelName; }
    public int getIndex() { return levelIndex; }

    public string getMusic()
    {
        if (levelMusic != null)
        {
            return levelMusic;
        }
        else
        {
            string defaultMusic = "";
            return defaultMusic;
        }
    }
}