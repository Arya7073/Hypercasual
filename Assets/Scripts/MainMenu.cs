using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button quitButton;

    void Start()
    {
        // Add listener to the quit button
        quitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        // Quit the application
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
