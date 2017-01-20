using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    //public Text timeText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private float score;

    // Use this for initialization
    void Start ()
    {
        gameOver = restart = false;
        restartText.text = gameOverText.text = "";
        score = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        //AddScore(Time.time);
    }

    /*public void AddScore(float ScoreValue)
    {
        score += ScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        timeText.text = "Score: " + score;
    }*/

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
