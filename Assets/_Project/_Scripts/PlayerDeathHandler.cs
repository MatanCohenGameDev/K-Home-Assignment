using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private CameraSwitcher cameraManager;
    [SerializeField]private PlayerHealthSystem health;

    private void OnEnable()
    {
        if (health != null)
            health.OnDeath += HandleDeath;
    }
    private void OnDisable()
    {
        if (health != null)
            health.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (movement != null)
            movement.enabled = false;

        if (rb != null)
            rb.constraints = RigidbodyConstraints.FreezeAll;

        if (cameraManager != null)
            cameraManager.SetState(CameraState.Death);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (deathScreen != null)
            deathScreen.SetActive(true);

    }

}
