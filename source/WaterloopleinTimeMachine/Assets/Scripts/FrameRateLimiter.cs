using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
#if UNITY_EDITOR
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
#endif
}
