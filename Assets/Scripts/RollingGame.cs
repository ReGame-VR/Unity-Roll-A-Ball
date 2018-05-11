using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollingGame : MonoBehaviour {

    [Tooltip("The number of items to pick up to win game.")]
    [SerializeField]
    private int numPickups = 5;

    [Tooltip("The player ball")]
    [SerializeField]
    private GameObject player;

    [Tooltip("The canvas giving the player feedback")]
    [SerializeField]
    private FeedbackCanvas feedbackCanvas;

    // The number of pickups collected so far in the game.
    private int numPickupsCollected = 0;

    // An enum denoting whether the game is starting, in progress, or over
    public enum GameState { PRE_GAME, GAME, POST_GAME }

    // The current state of the game
    private GameState curGameState;

    // The duration of the countdown, and the current countdown number
    private float countdown = 3f;

    // The duration of the game, and the time left in the current game
    private float timeLeft = GlobalControl.Instance.timeLimit;

    // The current score of the player in the game
    private float gameScore = 0f;

	// Use this for initialization
	void Start () {
        curGameState = GameState.PRE_GAME;
        player.GetComponent<Player>().FreezePlayer();
        feedbackCanvas.UpdateScoreText(gameScore);
		
	}
	
	// Update is called once per frame
	void Update () {

        if (curGameState == GameState.PRE_GAME)
        {
            // Countdown to 0
            countdown -= Time.deltaTime;
            feedbackCanvas.UpdateCountText(countdown);

            if (countdown < 0)
            {
                // Begin Game!
                player.GetComponent<Player>().UnfreezePlayer();
                curGameState = GameState.GAME;
                feedbackCanvas.HideCountText();
            }
        }
        else if (curGameState == GameState.GAME)
        {
            timeLeft -= Time.deltaTime;
            feedbackCanvas.UpdateTimeText(timeLeft);

            if (numPickupsCollected >= numPickups)
            {
                // Game won
                GameEnded(true);
            }
            if (timeLeft < 0)
            {
                // Game lost
                GameEnded(false);
            }
        }
        else
        {
            // The game is over
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Menu");
            }
        }
	}

    // You got a pickup, increase score!
    public void PickupCollected()
    {
        numPickupsCollected++;
        gameScore = gameScore + 100f;
        feedbackCanvas.UpdateScoreText(gameScore);
        GetComponent<SoundEffectPlayer>().PlayHappySound();
    }

    // You got a BAD pickup, decrease score.
    public void BadPickupCollected()
    {
        gameScore = gameScore - 10f;
        feedbackCanvas.UpdateScoreText(gameScore);
        GetComponent<SoundEffectPlayer>().PlayBadSound();
    }

    // The ball fell out of bounds. Decrease score.
    public void BallOutOfBounds()
    {
        gameScore = gameScore - 10f;
        feedbackCanvas.UpdateScoreText(gameScore);
        GetComponent<SoundEffectPlayer>().PlayResetSound();
    }

    // The game just ended. won = Did the player win?
    private void GameEnded(bool won)
    {
        player.GetComponent<Player>().FreezePlayer();
        curGameState = GameState.POST_GAME;
        feedbackCanvas.DisplayWinText(gameScore);
        GetComponent<DataHandler>().recordTrial(gameScore, numPickupsCollected, timeLeft, won);
    }
}
