using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public KeyCode timeScaleKey = KeyCode.Z;
    [Space,Header("TimeScale")]
    public float inhaleTimeScale = 0.5f;
    public float holdTimeScale = 0.15f;

    public BreathingState state;

    [Space,Header("TimeController")]
    public bool canBreathe;
    public float inhaleTime = 4f;
    [SerializeField]
    private float currentInhaleTime;

    public bool canHold;
    public float holdTime = 7f;
    [SerializeField]
    private float currentHoldTime;

    public float exhaleTime = 8f;
    [SerializeField]
    private float currentExhaleTime;

    public float timeTransitionSpeed = 0.1f;


    [Space, Header("PlayOnceAsset")]
    public UnityEvent Inhale_playOnce;
    public UnityEvent Exhale_playOnce;
    private bool isPlaying;

    [Space, Header("KeepUpdating")]
    public UnityEvent Inhale_playUpdated;
    public UnityEvent Exhale_playUpdated;

    [Space,Header("FlowerGrowSpeed")]
    public float flowerGrowSpeed = 1f;
    public float flowerDeGrowSpeed = -1f;

    [Space,Header("TestValue"),SerializeField]
    private float testTimeScale = 5f;

    
    void Update()
    {
        if (canBreathe) {
            switch (state) {
                case BreathingState.Inhale:
                    if (currentInhaleTime < inhaleTime)
                    {
                        currentInhaleTime += Time.unscaledDeltaTime;
                        InhaleTimeControl();
                     
                    }
                    else {
                        isPlaying = false;
                        if (canHold)
                            state = BreathingState.Hold;
                        else
                            state = BreathingState.Exhale;
                        currentInhaleTime = 0;
                    }
                    break;
                case BreathingState.Hold:
                    if (currentHoldTime < holdTime)
                    {
                        currentHoldTime += Time.unscaledDeltaTime;
                        HoldTimeControl();
                    }
                    else {
                        state = BreathingState.Exhale;
                        currentHoldTime = 0;
                    }
                    break;
                case BreathingState.Exhale:
                    if (currentExhaleTime < exhaleTime)
                    {
                        currentExhaleTime += Time.unscaledDeltaTime;
                        ExhaleTimeControl();
                  
                    }
                    else {
                        isPlaying = false;
                        state = BreathingState.Inhale;
                        currentExhaleTime = 0;
                    }
                    break;
            }
        }
        if (Input.GetKeyUp(timeScaleKey))
        {
            Time.timeScale = testTimeScale;
            canBreathe = false;
        }
    }

    public void BreatheJoin() {
        canBreathe = true;
    }

    public void ExhaleTimeControl() {
        if (Time.timeScale < 1) {
            Time.timeScale += Time.unscaledDeltaTime * timeTransitionSpeed;
            if (!isPlaying)
            {
                Exhale_playOnce?.Invoke();
                Debug.Log("Exhale_Once");
                isPlaying = true;
            }
            Exhale_playUpdated?.Invoke();
        }
    }

    public void InhaleTimeControl() {
        if (Time.timeScale > inhaleTimeScale) {
            Time.timeScale -= Time.unscaledDeltaTime * timeTransitionSpeed;
            if (!isPlaying)
            {
                Inhale_playOnce?.Invoke();
                Debug.Log("Inhale_Once");
                isPlaying = true;
            }
            Inhale_playUpdated?.Invoke();
        }
    }

    public void HoldTimeControl() {
        if (Time.timeScale > holdTimeScale)
        {
            Time.timeScale -= Time.unscaledDeltaTime * timeTransitionSpeed;
        }
    }
}
