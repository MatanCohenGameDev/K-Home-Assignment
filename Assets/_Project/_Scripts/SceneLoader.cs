using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToLoad;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad.sceneName))
        {
            SceneManager.LoadScene(sceneToLoad.sceneName);
        }
        else
        {
            Debug.LogError("SceneLoader: No scene name provided to load.");
        }
    }
}
