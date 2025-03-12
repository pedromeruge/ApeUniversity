using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    private bool gamePaused = false;
    private bool gameFinished = false;
    void Awake() {
        instance = this;
    }

    public void PauseGame(InputAction.CallbackContext context) {
        if (context.performed) {
            togglePauseGame();
        }
    }

    public bool togglePauseGame() {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0 : 1;
        HandleMenusUILogic.instance.onPause(gamePaused);
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
        HandleMenusUILogic.instance.onVictoryScreen();
    }

    public void restartGame() {
        gameFinished = false;
        gamePaused = false;
        Time.timeScale = 1;
    }
}
