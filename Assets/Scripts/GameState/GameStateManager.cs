using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private float averageTimeToCompleteLevel = 240.0f; // 4 minutes
    public static GameStateManager instance;

    private bool gamePaused = false;
    private bool gameFinished = false;
    private float timeElapsed = 0.0f;
    void Awake() {
        instance = this;
    }

    void Update()
    {
        incrementTime();
    }

    private void incrementTime() {
        timeElapsed += Time.deltaTime;
        if (!gamePaused) {
            OverlayUIUpdateLogic.Instance.changeTime(this.getTimeElapsed());
        }
    }

    public void PauseGame(InputAction.CallbackContext context) {
        if (context.performed) {
            togglePauseGame();
        }
    }

    public bool togglePauseGame(bool updateUi=true) {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0 : 1;

        if (updateUi) {
            HandleMenusUILogic.instance.onPause(gamePaused);
        }
        return gamePaused;
    }

    public void gameOver() {
        gamePaused = true;
        gameFinished = true;
        Time.timeScale = 0;
        HandleMenusUILogic.instance.onGameOverScreen();
    }
    public void victory() {
        gamePaused = true;
        gameFinished = true;
        Time.timeScale = 0;

        int playerMoney = PlayerStats.Instance.getMoney();
        int score = this.scoreCalculation(playerMoney, this.getTimeElapsedInSeconds());

        HandleMenusUILogic.instance.onVictoryScreen(playerMoney, this.getTimeElapsed(), score);
    }

    public void restartGame() {
        gameFinished = false;
        gamePaused = false;
        Time.timeScale = 1;
        timeElapsed = 0.0f;
    }

    public bool getGameFinished() {
        return gameFinished;
    }

    public string getTimeElapsed() {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        return String.Format("{0:00}:{1:00}", minutes, seconds); // substitutes 0 in place of minutes and seconds, with 2 placeholder 0s if needed
    }

    public float getTimeElapsedInSeconds() {
        return timeElapsed;
    }

    private int scoreCalculation(int playerMoney, float playerTime) {
        // NOTE: when player takes more than 2x the average time to complete the level, they get 0 points for time
        return 2 * playerMoney + 25 * Mathf.Max(0,Mathf.FloorToInt(2 * averageTimeToCompleteLevel - playerTime));
    }
}
