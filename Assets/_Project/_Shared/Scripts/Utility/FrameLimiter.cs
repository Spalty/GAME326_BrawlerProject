using UnityEngine;

public class FrameLimiter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60; 

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // Disable VSync to allow manual frame rate control
        Application.targetFrameRate = targetFrameRate; // Cap the frame rate to 60 FPS
    }
}
