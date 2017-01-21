﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour {

    private int team1Percent, team2Percent;
    private bool team1Capping, team2Capping;
    private IEnumerator captureCalc;
	// Use this for initialization
	void Start () {
        StartCoroutine(Capture());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Team1"))
            team1Capping = true;
        if (col.CompareTag("Team2"))
            team2Capping = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Team1"))
            team1Capping = false;
        if (col.CompareTag("Team2"))
            team2Capping = false;
    }

    IEnumerator Capture()
    {
        //team1Capping && !team2Capping || team2Capping && !team1Capping
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (team1Capping && !team2Capping)
            {
                Debug.Log("Team1 is capping");
                if (team2Percent > 0)
                    team2Percent -= 10;
                else if(team1Percent <= 100)
                    team1Percent += 10;

                Debug.Log("team1: " + team1Percent + " team2:" + team2Percent);
                if (team1Percent == 100)
                    Debug.Log("team1 capped the point"); // call function to do radar
                continue;
            }
            if(team2Capping && !team1Capping)
            {
                if (team1Percent > 0)
                    team1Percent -= 10;
                else if(team2Percent <= 100)
                    team2Percent += 10;
                Debug.Log("team1: " + team1Percent + " team2:" + team2Percent);
                if (team2Percent == 100)
                    Debug.Log("team2 capped the point");// call function to do radar
                continue;
            }
            
        }
    }
}
