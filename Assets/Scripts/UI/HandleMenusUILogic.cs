using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HandleMenusUILogic : MonoBehaviour
{
    public static HandleMenusUILogic instance;
    [SerializeField] private VisualTreeAsset pauseMenuUXML;
    [SerializeField] private VisualTreeAsset gameOverMenuUXML;
    [SerializeField] private VisualTreeAsset victoryMenuUXML;
    private UIDocument uiDocument;
    private VisualElement pauseMenu;
    private VisualElement gameOverMenu;
    private VisualElement victoryMenu;

    void Awake()
    {
        instance = this;
        uiDocument = GetComponent<UIDocument>();
        setupPauseScreen();
        setupGameOverScreen();
        setupVictoryScreen();
    }

    void setupPauseScreen() {
        pauseMenu = pauseMenuUXML.CloneTree();
        ApplyFullScreenStyle(pauseMenu);
        pauseMenu.style.display = DisplayStyle.None;  // hide pause menu initially
        uiDocument.rootVisualElement.Add(pauseMenu);

        Button resumeButton = pauseMenu.Q<Button>("resumeButton");
        resumeButton.clicked += onResume;
        Button resetButton = pauseMenu.Q<Button>("restartButton");
        resetButton.clicked += onRestart;
        Button quitButton = pauseMenu.Q<Button>("quitButton");
        quitButton.clicked += onQuitGame;
    }

    void setupGameOverScreen() {
        gameOverMenu = gameOverMenuUXML.CloneTree();
        ApplyFullScreenStyle(gameOverMenu);
        gameOverMenu.style.display = DisplayStyle.None;  // hide game over menu initially
        uiDocument.rootVisualElement.Add(gameOverMenu);

        Button resetButton = gameOverMenu.Q<Button>("restartButton");
        resetButton.clicked += onRestart;
        Button quitButton = gameOverMenu.Q<Button>("quitButton");
        quitButton.clicked += onQuitGame;
    }

    void setupVictoryScreen() {
        victoryMenu = victoryMenuUXML.CloneTree();
        ApplyFullScreenStyle(victoryMenu);
        victoryMenu.style.display = DisplayStyle.None;  // hide victory menu initially
        uiDocument.rootVisualElement.Add(victoryMenu);

        Button resetButton = victoryMenu.Q<Button>("restartButton");
        resetButton.clicked += onRestart;
        Button quitButton = victoryMenu.Q<Button>("quitButton");
        quitButton.clicked += onQuitGame;
    }

    void onResume() {
        GameStateManager.instance.togglePauseGame();
        onPause(false);
    }

    void onRestart()
    {
        GameStateManager.instance.togglePauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // repeats the current scene, effectively restarting the game
    }

    void onQuitGame()
    {
        GameStateManager.instance.togglePauseGame();
        SceneManager.LoadScene("MainMenuScene");
    }

    // stretch the pause menu to fill the whole screen, or it appears squished
    void ApplyFullScreenStyle(VisualElement element)
    {
        element.style.flexGrow = 1;
        element.style.width = Length.Percent(100);
        element.style.height = Length.Percent(100);
        element.style.position = Position.Absolute;
        element.style.top = 0;
        element.style.left = 0;
    }

    public void onPause(bool isPaused) {
        Debug.Log("Pause");
        if (isPaused) {
            pauseMenu.style.display = DisplayStyle.Flex;
        }
        else {
            pauseMenu.style.display = DisplayStyle.None;
        }
    }
    public void onGameOverScreen()
    {
        Debug.Log("Game Over");
        pauseMenu.style.display = DisplayStyle.None;
        victoryMenu.style.display = DisplayStyle.None;
        gameOverMenu.style.display = DisplayStyle.Flex;
    }

    public void onVictoryScreen()
    {
        pauseMenu.style.display = DisplayStyle.None;
        gameOverMenu.style.display = DisplayStyle.None;
        victoryMenu.style.display = DisplayStyle.Flex;
    }
}
