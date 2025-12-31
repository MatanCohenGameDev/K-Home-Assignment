using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class CameraEntry
    {
        public CameraState state;
        public CinemachineCamera vcam;
    }


    [SerializeField] private List<CameraEntry> cameras;
    private int activePriority = 30;
    [SerializeField] private CameraOrbit deathCamEffect;

    public CameraState CurrentState { get; private set; }

    private void Awake()
    {
        SetState(CameraState.Gameplay);
    }

    public void SetState(CameraState newState)
    {
        if (newState == CameraState.Death)
        {
            Time.timeScale = 0.5f;
            deathCamEffect.enabled = true;
        }
        else
        {
            Time.timeScale = 1f;
            deathCamEffect.enabled = false;
        }
        if (cameras == null) return;
        for (int i = 0; i < cameras.Count; i++)
        {
            var entry = cameras[i];
            entry.vcam.Priority = entry.state == newState ? activePriority : 0;

        }
    }


}
public enum CameraState
{
    Gameplay,
    Death,
    StartLevel,
    OpenDoor,
}