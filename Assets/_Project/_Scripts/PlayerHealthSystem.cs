using System;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource hurtSound;
    public bool IsDead { get; private set; }

    public event Action OnDeath;


    public void TakeDamage(int damage)
    {
       if(IsDead || GameManager.Instance.IsStunned)
           return;

        GameManager.Instance.TakeDamage(damage);

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (hurtSound != null)
            hurtSound.Play();

        if (GameManager.Instance.CurrentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (IsDead) return;

        IsDead = true;
        animator.SetBool("Dead", true);
        OnDeath?.Invoke();
    }



}
