using Unity.Cinemachine;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 20f;
    [SerializeField] private CinemachineCamera cam;

    private CinemachineOrbitalFollow orbital;

    void Awake()
    {
            orbital = cam.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachineOrbitalFollow;
    }

    void Update()
    {
        if (orbital == null) return;
        
            orbital.HorizontalAxis.Value += orbitSpeed * Time.unscaledDeltaTime;
        
    }
}
