using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BreathingState
{
    Inhale,
    Hold,
    Exhale
}

public enum Phase { 
    Phase1,
    Phase2,
    Phase3
}

public enum testState { 
    InhaleToSlowDownTime,
    ExhaleToSpeedUpTime,
}

[System.Serializable]
public class BreatheingPhase {
    public Phase phase;
    public float inhaleTime = 4f;
    [SerializeField]
    private float currentInhaleTime;

    public float holdTime = 7f;
    [SerializeField]
    private float currentHoldTime;

    public float exhaleTime = 8f;
    [SerializeField]
    private float currentExhaleTime;

    public float timeTransitionSpeed = 0.1f;

    public bool CompleteInhaling() {
        if (currentInhaleTime < inhaleTime)
        {
            currentInhaleTime += Time.unscaledDeltaTime;
            return false;
        }
        else {
            return true;
        }
    }

 

    public bool CompleteHolding() {
        if (currentHoldTime < holdTime)
        {
            currentHoldTime += Time.unscaledDeltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CompleteExhaling()
    {
        if (currentExhaleTime < exhaleTime)
        {
            currentExhaleTime += Time.unscaledDeltaTime;
            return false;
        }
        else {
            return true;
        }
    }
    public void ResetInhaleTime()
    {
        currentInhaleTime = 0;
    }

    public void ResetHoldTime()
    {
        currentHoldTime = 0;
    }

    public void ResetExhaleTime()
    {
        currentExhaleTime = 0;
    }

}

public class TimeManager : MonoBehaviour
{
    public KeyCode timeScaleKey = KeyCode.Z;
    [Space,Header("TimeScale")]
    public float inhaleTimeScale = 0.5f;
    [SerializeField, Tooltip("This is for exhaling speed up time")]
    private float exhaleTimeScale = 2f;
    public float holdTimeScale = 0.15f;

    public BreathingState state;
    public Phase phase;

    [Space, Header("TimeController")]
    public bool canBreathe;
    public bool canHold;


    public List<BreatheingPhase> breathingPhase = new List<BreatheingPhase>();

    [Space, Header("PlayOnceAsset")]
    public UnityEvent Inhale_playOnce;
    public UnityEvent Exhale_playOnce;
    private bool isPlaying;

    [Space,Header("FlowerGrowSpeed")]
    public float flowerGrowSpeed = 1f;
    public float flowerDeGrowSpeed = -1f;

    [Space,Header("TestValue"),SerializeField]
    private float testTimeScale = 5f;
    public testState testBreathingState;

    
    void Update()
    {
        if (canBreathe) {
            switch (state) {
                case BreathingState.Inhale:
                    switch (phase) {
                        case Phase.Phase1:
                            if (breathingPhase[0].CompleteInhaling())
                            {
                                isPlaying = false;
                                if (canHold)
                                    state = BreathingState.Hold;
                                else
                                    state = BreathingState.Exhale;
                                breathingPhase[0].ResetInhaleTime();
                            }
                            else {
                                InhaleTimeControl();
                            }
                            break;
                        case Phase.Phase2:
                            if (breathingPhase[1].CompleteInhaling())
                            {
                                isPlaying = false;
                                if (canHold)
                                    state = BreathingState.Hold;
                                else
                                    state = BreathingState.Exhale;
                                breathingPhase[1].ResetInhaleTime();
                            }
                            else {
                                InhaleTimeControl();
                            }
                            break;
                        case Phase.Phase3:
                            if (breathingPhase[2].CompleteInhaling())
                            {
                                isPlaying = false;
                                if (canHold)
                                    state = BreathingState.Hold;
                                else
                                    state = BreathingState.Exhale;
                                breathingPhase[2].ResetInhaleTime();
                            }
                            else {
                                InhaleTimeControl();
                            }
                            break;
                    }
                    break;
                case BreathingState.Hold:
                    switch (phase)
                    {
                        case Phase.Phase1:
                            if (breathingPhase[0].CompleteHolding())
                            {
                                state = BreathingState.Exhale;
                                breathingPhase[0].ResetHoldTime();
                            }
                            else {
                                HoldTimeControl();
                            }
                            break;
                        case Phase.Phase2:
                            if (breathingPhase[1].CompleteHolding())
                            {
                                state = BreathingState.Exhale;
                                breathingPhase[1].ResetHoldTime();
                            }
                            else {
                                HoldTimeControl();
                            }
                            break;
                        case Phase.Phase3:
                            if (breathingPhase[2].CompleteHolding())
                            {
                                state = BreathingState.Exhale;
                                breathingPhase[2].ResetHoldTime();
                            }
                            else {
                                HoldTimeControl();
                            }
                            break;
                    }
                    break;
                case BreathingState.Exhale:
                    switch (phase)
                    {
                        case Phase.Phase1:
                            if (breathingPhase[0].CompleteExhaling())
                            {
                                isPlaying = false;
                                state = BreathingState.Inhale;
                                breathingPhase[0].ResetExhaleTime();
                            }
                            else {
                                ExhaleTimeControl();
                            }
                            break;
                        case Phase.Phase2:
                            if (breathingPhase[1].CompleteExhaling())
                            {
                                isPlaying = false;
                                state = BreathingState.Inhale;
                                breathingPhase[1].ResetExhaleTime();
                            }
                            else {
                                ExhaleTimeControl();
                            }
                            break;
                        case Phase.Phase3:
                            if (breathingPhase[2].CompleteExhaling())
                            {
                                isPlaying = false;
                                state = BreathingState.Inhale;
                                breathingPhase[2].ResetExhaleTime();
                            }
                            else {
                                ExhaleTimeControl();
                            }
                            break;
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
        switch (testBreathingState) {
            case testState.ExhaleToSpeedUpTime:
                if (Time.timeScale < exhaleTimeScale)
                {
                    switch (phase)
                    {
                        case Phase.Phase1:
                            if (!breathingPhase[0].CompleteExhaling())
                            {
                                Time.timeScale += Time.unscaledDeltaTime * breathingPhase[0].timeTransitionSpeed;
                                if (!isPlaying)
                                {
                                    Exhale_playOnce?.Invoke();
                                    Debug.Log("Exhale_Once");
                                    isPlaying = true;
                                }
                            }
                            break;
                        case Phase.Phase2:
                            if (!breathingPhase[1].CompleteExhaling())
                            {
                                Time.timeScale += Time.unscaledDeltaTime * breathingPhase[1].timeTransitionSpeed;
                                if (!isPlaying)
                                {
                                    Exhale_playOnce?.Invoke();
                                    Debug.Log("Exhale_Once");
                                    isPlaying = true;
                                }
                            }
                            break;
                        case Phase.Phase3:
                            if (!breathingPhase[2].CompleteExhaling())
                            {
                                Time.timeScale += Time.unscaledDeltaTime * breathingPhase[2].timeTransitionSpeed;
                                if (!isPlaying)
                                {
                                    Exhale_playOnce?.Invoke();
                                    Debug.Log("Exhale_Once");
                                    isPlaying = true;
                                }
                            }
                            break;
                    }
                }
                break;
            case testState.InhaleToSlowDownTime:
                if (Time.timeScale < 1)
                {
                    switch (phase)
                    {
                        case Phase.Phase1:
                            if (!breathingPhase[0].CompleteExhaling())
                            {
                                Time.timeScale += Time.unscaledDeltaTime * breathingPhase[0].timeTransitionSpeed;
                                if (!isPlaying)
                                {
                                    Exhale_playOnce?.Invoke();
                                    Debug.Log("Exhale_Once");
                                    isPlaying = true;
                                }
                            }
                            break;
                        case Phase.Phase2:
                            if (!breathingPhase[1].CompleteExhaling())
                            {
                                Time.timeScale += Time.unscaledDeltaTime * breathingPhase[1].timeTransitionSpeed;
                                if (!isPlaying)
                                {
                                    Exhale_playOnce?.Invoke();
                                    Debug.Log("Exhale_Once");
                                    isPlaying = true;
                                }
                            }
                            break;
                        case Phase.Phase3:
                            if (!breathingPhase[2].CompleteExhaling())
                            {
                                Time.timeScale += Time.unscaledDeltaTime * breathingPhase[2].timeTransitionSpeed;
                                if (!isPlaying)
                                {
                                    Exhale_playOnce?.Invoke();
                                    Debug.Log("Exhale_Once");
                                    isPlaying = true;
                                }
                            }
                            break;
                    }
                }
                break;
        }
       
    }

    public void InhaleTimeControl() {
        if (Time.timeScale > inhaleTimeScale)
        {
            switch (phase)
            {
                case Phase.Phase1:
                    if (!breathingPhase[0].CompleteExhaling())
                    {
                        Time.timeScale -= Time.unscaledDeltaTime * breathingPhase[0].timeTransitionSpeed;

                        if (!isPlaying)
                        {
                            Inhale_playOnce?.Invoke();
                            Debug.Log("Exhale_Once");
                            isPlaying = true;
                        }
                    }
                    break;
                case Phase.Phase2:
                    if (!breathingPhase[1].CompleteExhaling())
                    {
                        Time.timeScale -= Time.unscaledDeltaTime * breathingPhase[1].timeTransitionSpeed;
                        if (!isPlaying)
                        {
                            Inhale_playOnce?.Invoke();
                            Debug.Log("Exhale_Once");
                            isPlaying = true;
                        }
                    }
                    break;
                case Phase.Phase3:
                    if (!breathingPhase[2].CompleteExhaling())
                    {
                        Time.timeScale -= Time.unscaledDeltaTime * breathingPhase[2].timeTransitionSpeed;
                        if (!isPlaying)
                        {
                            Inhale_playOnce?.Invoke();
                            Debug.Log("Exhale_Once");
                            isPlaying = true;
                        }
                    }
                    break;
            }
        }
    }

    public void HoldTimeControl() {
        if (Time.timeScale > holdTimeScale)
        {
            switch (phase)
            {
                case Phase.Phase1:
                    if (!breathingPhase[0].CompleteExhaling())
                    {
                        Time.timeScale -= Time.unscaledDeltaTime * breathingPhase[0].timeTransitionSpeed;
                    }
                    break;
                case Phase.Phase2:
                    if (!breathingPhase[1].CompleteExhaling())
                    {
                        Time.timeScale -= Time.unscaledDeltaTime * breathingPhase[1].timeTransitionSpeed;
                    }
                    break;
                case Phase.Phase3:
                    if (!breathingPhase[2].CompleteExhaling())
                    {
                        Time.timeScale -= Time.unscaledDeltaTime * breathingPhase[2].timeTransitionSpeed;
                    }
                    break;
            }
        }
    }
}
