using UnityEngine;
using System.Collections;

public class SpikeMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float upHeight = 1.2f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Timing")]
    [SerializeField] private float delayBeforeUp = 1f;
    [SerializeField] private float stayUpTime = 1f;

    [Header("Shake Warning")]
    [SerializeField] private float shakeDuration = 0.4f;
    [SerializeField] private float shakeAmount = 0.05f;

    private Vector3 downPosition;
    private Vector3 upPosition;
    private void Awake()
    {
        downPosition = transform.position;
        upPosition = downPosition + Vector3.up * upHeight;

        StartCoroutine(SpikeRoutine());
    }

    private IEnumerator SpikeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBeforeUp);

            yield return StartCoroutine(Shake());

            yield return StartCoroutine(MoveTo(upPosition));

            yield return new WaitForSeconds(stayUpTime);

            yield return StartCoroutine(MoveTo(downPosition));
        }
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offset = Random.Range(-shakeAmount, shakeAmount);
            transform.position = downPosition + new Vector3(offset, 0f, offset);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = downPosition;
    }
}
