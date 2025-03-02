using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    public static CameraShake Instance;
    private float shakeTimer = 0f;
    private float shakeTimerTotal;
    private float startingIntensity;
    void Awake() {
        Instance = this;
    }

    void onShake(float duration, float strength) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            (CinemachineBasicMultiChannelPerlin)cam.GetCinemachineComponent(CinemachineCore.Stage.Noise);
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = strength;
        shakeTimer = duration;
        shakeTimerTotal = duration;
        startingIntensity = strength;
    }

    public static void Shake(float duration, float strength) {
        Instance.onShake(duration, strength);
    }

    private void Update()
    {
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
                (CinemachineBasicMultiChannelPerlin) cam.GetCinemachineComponent(CinemachineCore.Stage.Noise);
            cinemachineBasicMultiChannelPerlin.AmplitudeGain = 
                Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));

        }
    }
}
