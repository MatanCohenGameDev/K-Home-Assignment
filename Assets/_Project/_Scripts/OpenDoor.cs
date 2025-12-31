using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private AudioSource doorOpenSound;
    [SerializeField] private Animator doorAnimator;
    public void OpenTheDoor()
    {
        if (!GameManager.Instance.CheckIfHasKey())
            return;

        doorAnimator.SetTrigger("OpenDoor");
        doorOpenSound.Play();

    }
}
