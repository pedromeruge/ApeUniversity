using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] private float waitTime = 94.0f;
    [SerializeField] private string sceneName = "GameScene";

    private void Start()
    {
        StartCoroutine("loadSceneWithDelay");
    }

    IEnumerator loadSceneWithDelay() {
        yield return new WaitForSeconds(waitTime);
        loadSceneInstantly();
    }

    public void loadSceneInstantly() {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void Skip(InputAction.CallbackContext context) {
        if (context.performed) {
            loadSceneInstantly();
        }
    }
}
