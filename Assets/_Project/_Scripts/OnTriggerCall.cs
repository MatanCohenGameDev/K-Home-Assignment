using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCall : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEvent;
    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player"))
       {

            onTriggerEvent?.Invoke();
           
        }
    }
}
