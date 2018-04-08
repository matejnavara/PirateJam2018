using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Progress : MonoBehaviour {

    public bool timerStarted;
    public bool complete;
    float startTime;
    float totalTime;
    float cutoff;
    private Material progressMat;

    private void Start()
    {
        progressMat = GetComponent<SpriteRenderer>().material;
        progressMat.SetFloat("_Cutoff", 0);
    }

    public void StartTimer(float amount)
    {
        timerStarted = true;
        complete = false;
        startTime = Time.time;
        totalTime = amount;
    }

    public void StopTimer()
    {
        cutoff = 0;
        timerStarted = false;
        progressMat.SetFloat("_Cutoff", cutoff);

    }
	
	// Update is called once per frame
	void Update () {
		if(timerStarted)
        {
            float elapsedTime = Time.time - startTime;
            float scaleTime = elapsedTime / totalTime;
            
            if(scaleTime < 1)
            {
                complete = false;
                cutoff = Mathf.Lerp(0, 1, scaleTime);
                progressMat.SetFloat("_Cutoff", cutoff);
            }
            else
            {
                Debug.Log("PROGRESS COMPLETE");
                complete = true;
                StopTimer();
            }         
        }
	}
}
