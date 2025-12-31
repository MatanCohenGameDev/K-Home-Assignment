using UnityEngine;

public class OnTriggerDmg : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private bool teleportToLastGroundedPos;

    [Header("Knockback")]
    [SerializeField] private bool doKnockback = true;
    [SerializeField] private float knockbackForce = 8f;
    [SerializeField] private float upwardForce = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!other.TryGetComponent(out PlayerHealthSystem health))
            return;

        if (!other.TryGetComponent(out PlayerMovement movement))
            return;

        health.TakeDamage(damageAmount);

        if (doKnockback)
        {
            Vector3 force = CalculateKnockbackForce(other.transform);
            movement.ApplyKnockback(force);
        }


        if (teleportToLastGroundedPos)
            movement.ResetToLastGroundedPosition();
    }

    private Vector3 CalculateKnockbackForce(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f; // 🚫 prevents spin / torque
        direction.Normalize();

        return direction * knockbackForce + Vector3.up * upwardForce;
    }
}
