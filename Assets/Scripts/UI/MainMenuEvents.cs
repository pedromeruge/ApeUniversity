using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    public VisualTreeAsset mainMenuUXML;
    public VisualTreeAsset creditsUXML;
    public VisualTreeAsset controlsUXML;

    private UIDocument uiDocument;
    private VisualElement root;

    void Start() {
        uiDocument = GetComponent<UIDocument>();
        ShowMainMenu();
    }

    void ShowMainMenu() {
        root = mainMenuUXML.CloneTree();
        ApplyFullScreenStyle(root);
        uiDocument.rootVisualElement.Clear();
        uiDocument.rootVisualElement.Add(root);

        Button buttonPlay = root.Q<Button>("playButton");
        buttonPlay.clicked += onPressPlay;
        Button buttonControls = root.Q<Button>("controlsButton");
        buttonControls.clicked += ShowControls;
        Button buttonCredits = root.Q<Button>("creditsButton");
        buttonCredits.clicked += ShowCredits;
        Button buttonExit = root.Q<Button>("exitButton");
        buttonExit.clicked += onPressExit;

        root.Focus();
    }

    void ShowCredits() {
        root = creditsUXML.CloneTree();
        ApplyFullScreenStyle(root);
        uiDocument.rootVisualElement.Clear();
        uiDocument.rootVisualElement.Add(root);

        Button buttonBack = root.Q<Button>("backButton");
        buttonBack.clicked += ShowMainMenu;
    }

    void ShowControls() {
        root = controlsUXML.CloneTree();
        ApplyFullScreenStyle(root);
        uiDocument.rootVisualElement.Clear();
        uiDocument.rootVisualElement.Add(root);

        Button buttonBack = root.Q<Button>("backButton");
        buttonBack.clicked += ShowMainMenu;
    }

    // force uxml to fill the whole screen, else the screens appear squished at the top
    void ApplyFullScreenStyle(VisualElement element) {
        element.style.flexGrow = 1;
        element.style.width = Length.Percent(100);
        element.style.height = Length.Percent(100);
    }

    private void onPressPlay()
    {
        SceneManager.LoadScene("IntroScene");
    }

    private void onPressExit()
    {
        Application.Quit();
    }
}