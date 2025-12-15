#if UNITY_EDITOR
using UnityEditor;
#endif

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    public void ToggleCredits ()
    {
        creditsPanel?.SetActive(!creditsPanel.activeSelf);
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Exit triggered");
    }
}
