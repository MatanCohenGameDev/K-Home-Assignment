using UnityEngine;

public class SpinAroundPoint : MonoBehaviour
{
    [Header("Orbit")]
    [SerializeField] private bool orbitEnabled = true;
    [SerializeField] private Transform orbitPoint;
    [SerializeField] private float orbitSpeed = 10f;
    [SerializeField] private Vector3 orbitAxis = Vector3.up;

    [Header("Self Rotation")]
    [SerializeField] private bool selfRotate = true;
    [SerializeField] private float selfRotationSpeed = 20f;
    [SerializeField] private Vector3 selfRotationAxis = Vector3.up;



    private void LateUpdate()
    {
        if (orbitEnabled && orbitPoint != null)
        {
            transform.RotateAround(orbitPoint.position, orbitAxis.normalized,orbitSpeed * Time.deltaTime);
        }

        if (selfRotate)
        {
            transform.Rotate(selfRotationAxis.normalized,selfRotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
