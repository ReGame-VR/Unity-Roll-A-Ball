using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackCanvas : MonoBehaviour {

    // Text boxes for the UI elements
    [SerializeField]
    private Text winText;
    [SerializeField]
    private Text countText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text scoreText;
    
    // Display and update countdown text
    public void UpdateCountText(float countdown)
    {
        string countString = Mathf.Round(countdown).ToString();

        // If the countdown is done, display "GO!"
        if (countString == "0")
        {
            countString = "GO!";
        }
        countText.text = countString;
    }

    // Hide the countdown text
    public void HideCountText()
    {
        countText.text = "";
    }

    // Update the display of time remaining
    public void UpdateTimeText(float timeLeft)
    {
        timeText.text = Mathf.Round(timeLeft).ToString();
    }

    // Display feedback after user has won
    public void DisplayWinText(float score)
    {
        winText.text = "Good job! Score: " + score.ToString() + " Press Space";
    }

    // Update the display of score
    public void UpdateScoreText(float score)
    {
        scoreText.text = Mathf.Round(score).ToString();
    }
}
