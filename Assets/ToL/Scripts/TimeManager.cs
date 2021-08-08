using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

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

[System.Serializable]
public class BreatheingPhase {
    public Phase phase;
    public float inhaleTime = 4f;
    public float holdTime = 7f;
    public float exhaleTime = 8f;


    public float currentInhaleTime;
    public float currentHoldTime;
    public float currentExhaleTime;

    public float timeTransitionSpeed = 0.1f;
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
    private bool canGoNextPhase;
    [SerializeField]
    public int currentPhaseIndex { get; private set; }

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
                    InhaleTimeControl(currentPhaseIndex);
                    //switch (phase) {
                    //    case Phase.Phase1:
                    //        InhaleTimeControl(0);
                    //        break;
                    //    case Phase.Phase2:
                    //        InhaleTimeControl(1);
                    //        break;
                    //    case Phase.Phase3:
                    //        InhaleTimeControl(2);
                    //        break;
                    //}
                    break;
                case BreathingState.Hold:
                    HoldTimeControl(currentPhaseIndex);
                    //switch (phase)
                    //{
                    //    case Phase.Phase1:
                    //        HoldTimeControl(0);
                    //        break;
                    //    case Phase.Phase2:
                    //        HoldTimeControl(1);
                    //        break;
                    //    case Phase.Phase3:
                    //        HoldTimeControl(2);
                    //        break;
                    //}
                    break;
                case BreathingState.Exhale:
                    ExhaleTimeControl(currentPhaseIndex);
                    //switch (phase)
                    //{
                    //    case Phase.Phase1:
                    //        ExhaleTimeControl(0);
                    //        break;
                    //    case Phase.Phase2:
                    //        ExhaleTimeControl(1);
                    //        break;
                    //    case Phase.Phase3:
                    //        ExhaleTimeControl(2);
                    //        break;
                    //}
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

    private void InhaleTimeControl(int _index) {
        if (Time.realtimeSinceStartup > breathingPhase[_index].currentInhaleTime)
        {
            //Completed
            isPlaying = false;
           
            if (canHold)
            {
                breathingPhase[_index].currentHoldTime = Time.realtimeSinceStartup + breathingPhase[_index].holdTime;
                state = BreathingState.Hold;
            }
            else { 
                breathingPhase[_index].currentExhaleTime = Time.realtimeSinceStartup + breathingPhase[_index].exhaleTime;
                state = BreathingState.Exhale;
            }
        }
        else {
            //Inhaling
            if (Time.timeScale > inhaleTimeScale) {
                Time.timeScale -= Time.unscaledDeltaTime * breathingPhase[_index].timeTransitionSpeed;
            }
           

            if (!isPlaying)
            {
                Inhale_playOnce?.Invoke();
                isPlaying = true;
            }
        }
    }

    private void ExhaleTimeControl(int _index)
    {
        if (Time.realtimeSinceStartup > breathingPhase[_index].currentExhaleTime)
        {
            //Completed
            isPlaying = false;
            if (canGoNextPhase)
            {
                currentPhaseIndex += 1;
                phase = (Phase)Enum.GetValues(phase.GetType()).GetValue(currentPhaseIndex);
                breathingPhase[currentPhaseIndex].currentInhaleTime = Time.realtimeSinceStartup + breathingPhase[currentPhaseIndex].inhaleTime;
                state = BreathingState.Inhale;
                canGoNextPhase = false;
            }
            else {
                breathingPhase[_index].currentInhaleTime = Time.realtimeSinceStartup + breathingPhase[_index].inhaleTime;
                state = BreathingState.Inhale;
            }
        }
        else
        {
            //Exhaling
            if (Time.timeScale < exhaleTimeScale)
            {
                Time.timeScale += Time.unscaledDeltaTime * breathingPhase[_index].timeTransitionSpeed;
            }
            if (!isPlaying)
            {
                Exhale_playOnce?.Invoke();
                isPlaying = true;
            }
        }
    }

    private void HoldTimeControl(int _index) {

        if (Time.realtimeSinceStartup > breathingPhase[_index].currentHoldTime)
        {
            //Completed
            breathingPhase[_index].currentExhaleTime = Time.realtimeSinceStartup + breathingPhase[_index].exhaleTime;
            state = BreathingState.Exhale;
        }
        else
        {
            //Holding
            //if (Time.timeScale < exhaleTimeScale)
            //{
            //    Time.timeScale += Time.unscaledDeltaTime * breathingPhase[_index].timeTransitionSpeed;
            //}
            //if (!isPlaying)
            //{
            //    Exhale_playOnce?.Invoke();
            //    isPlaying = true;
            //}
        }
    }

    public void GoNextPhase() {
        canGoNextPhase = true;
    }
 
}
