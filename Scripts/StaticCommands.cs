#if UNITY_EDITOR
using UnityEditor;
#endif
using QFSW.QC;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StaticCommands
{
    [Command]
    public static void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stops play mode
#else
        Application.Quit(); // Exits build
#endif
        Debug.Log("Exit triggered");
    }

    [Command]
    public static void LoadScene(Scenes scene)
    {
        string sceneName = scene.ToString();
        SceneManager.LoadScene(sceneName);
    }
}

public enum Scenes
{
    MainMenu,
    GameScene
}