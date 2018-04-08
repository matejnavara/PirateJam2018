using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ReadyMeter : MonoBehaviour {

    public bool timerStarted;
    public bool ready;
    float startTime;
    float totalTime;
    float fill;
    private Image meterImg;

    private void Start()
    {
        meterImg = GetComponent<Image>();
        meterImg.fillAmount = 1;
        ready = true;
    }

    public void StartTimer(float amount)
    {
        timerStarted = true;
        ready = false;
        startTime = Time.time;
        totalTime = amount;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(timerStarted)
        {
            float elapsedTime = Time.time - startTime;
            float scaleTime = elapsedTime / totalTime;
            
            if(scaleTime < 1)
            {
                ready = false;
                fill = Mathf.Lerp(0, 1, scaleTime);
                meterImg.fillAmount = fill;
            }
            else
            {
                Debug.Log("PROGRESS COMPLETE");
                ready = true;
                StopTimer();
            }         
        }
	}
}
