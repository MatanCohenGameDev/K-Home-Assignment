using UnityEngine;

public class PauseManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenuUI;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentHP <= 0)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameManager.Instance.CurrentHP <= 0)
            return;
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
