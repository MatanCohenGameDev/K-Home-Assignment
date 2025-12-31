using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField] private CollectableType collectableType;

    [SerializeField] private int coinsAmount = 1;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        

        if(collectableType == CollectableType.Coin)
        {

            GameManager.Instance.AddCoin(coinsAmount);
        }
        else if(collectableType == CollectableType.Key)
        {
            GameManager.Instance.SetKey(true);
        }

        GameManager.Instance.PlayCollectableSound();
        gameObject.SetActive(false);
        
    }
}

public enum CollectableType
{
    Coin,
    Key
}