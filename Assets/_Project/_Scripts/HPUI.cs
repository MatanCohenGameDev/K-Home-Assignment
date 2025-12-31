using TMPro;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;

    private void OnEnable()
    {
        GameManager.Instance.OnHPChanged += UpdateUI;
    }

    void Start()
    {
        UpdateUI(GameManager.Instance.CurrentHP);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnHPChanged -= UpdateUI;
    }

    private void UpdateUI(int hp)
    {
        hpText.text = hp.ToString();
    }

}
