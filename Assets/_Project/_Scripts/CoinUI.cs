using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    private void OnEnable()
    {
        GameManager.Instance.OnCoinsChanged += UpdateUI;
    }
    void Start()
    {
        UpdateUI(GameManager.Instance.Coins);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCoinsChanged -= UpdateUI;
    }

    private void UpdateUI(int coins)
    {
        coinsText.text = coins.ToString();
    }
}
