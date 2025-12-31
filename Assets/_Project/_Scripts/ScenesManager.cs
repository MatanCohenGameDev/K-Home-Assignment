#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneReference
{
    public string sceneName; // Store the scene name

#if UNITY_EDITOR
    public SceneAsset sceneAsset; // Reference for ease in the editor
#endif
}

public class ScenesManager : MonoBehaviour
{


    public void NewGame()
    {
        GameManager.Instance.ResetHP();
        GameManager.Instance.ResetCoin();
    }

    public void RestartScene()
    {
        GameManager.Instance.ResetHP();
        GameManager.Instance.ResetCoin();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}