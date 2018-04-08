using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PortalIndicator : MonoBehaviour {

    private Baby baby;
    private Text indicatorText;


	// Use this for initialization
	void Start () {

        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<Baby>();
        indicatorText = GetComponentInChildren<Text>();
        indicatorText.text = baby.portalCount +"/4";
		
	}
	
	// Update is called once per frame
	void Update () {
        indicatorText.text = baby.portalCount + "/4";
    }
}
