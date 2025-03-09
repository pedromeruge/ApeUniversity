using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents2 : MonoBehaviour
{
    public VisualTreeAsset mainMenuUXML;
    public VisualTreeAsset creditsUXML;

    private UIDocument uiDocument;
    private VisualElement root;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        ShowMainMenu();  // load main menu by default
    }

    void ShowMainMenu() {
        root = mainMenuUXML.CloneTree();
        ApplyFullScreenStyle(root);
        uiDocument.rootVisualElement.Clear();
        uiDocument.rootVisualElement.Add(root);

        Button buttonPlay = root.Q<Button>("playButton");
        buttonPlay.clicked += onPressPlay;
        Button buttonCredits = root.Q<Button>("creditsButton");
        buttonCredits.clicked += ShowCredits;
        Button buttonExit = root.Q<Button>("exitButton");
        buttonExit.clicked += onPressExit;
    }

    void ShowCredits() {
        root = creditsUXML.CloneTree();
        ApplyFullScreenStyle(root);
        uiDocument.rootVisualElement.Clear();
        uiDocument.rootVisualElement.Add(root);

        Button buttonBack = root.Q<Button>("backButton");
        buttonBack.clicked += ShowMainMenu;
    }

    // force uxml to fill the whole screen, else the screens appear squished at
    void ApplyFullScreenStyle(VisualElement element) {
        element.style.flexGrow = 1;
        element.style.width = Length.Percent(100);
        element.style.height = Length.Percent(100);
    }

    private void onPressPlay()
    {
        Debug.Log("Play button pressed");
    }

    private void onPressExit()
    {
        Debug.Log("Exit button pressed");
        Application.Quit();
    }
}