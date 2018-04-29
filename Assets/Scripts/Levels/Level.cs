using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{

    public string levelName;
    public int levelIndex;
    private AudioSource audioSource;

    public string getName() { return levelName; }
    public int getIndex() { return levelIndex; }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}