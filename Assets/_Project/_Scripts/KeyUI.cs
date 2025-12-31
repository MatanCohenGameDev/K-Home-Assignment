using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [SerializeField] private Image keyImage;

    private void OnEnable()
    {
        GameManager.Instance.OnKeyChanged += UpdateUI;
    }

    void Start()
    {
        UpdateUI(GameManager.Instance.HasKey);
    }


    private void OnDisable()
    {
        GameManager.Instance.OnKeyChanged -= UpdateUI;
    }

    private void UpdateUI(bool hasKey)
    {
        keyImage.enabled = hasKey;
    }
}
