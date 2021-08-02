using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float transitionSpeed = 0.1f;

    [SerializeField]
    private Animator[] flowerAnims;

    
    void Update()
    {
        if (canBreathe) {
            switch (state) {
                case BreathingState.Inhale:
                    if (currentInhaleTime < inhaleTime)
                    {
                        currentInhaleTime += Time.unscaledDeltaTime;
                        InhaleTimeScale();
                    }
                    else {
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
                        HoldTimeScale();
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
                        ResetTimeScale();
                    }
                    else {
                        state = BreathingState.Inhale;
                        currentExhaleTime = 0;
                    }
                    break;
            }
        }
        if (Input.GetKeyUp(timeScaleKey))
        {
            
        }
    }

    public void BreatheJoin() {
        canBreathe = true;
    }

    public void ResetTimeScale() {
        if (Time.timeScale < 1) {
            Time.timeScale += Time.unscaledDeltaTime * transitionSpeed;
            foreach (var anim in flowerAnims)
            {
                anim.SetFloat("Speed", 1);
            }
        }
    }

    public void InhaleTimeScale() {
        if (Time.timeScale > inhaleTimeScale) {
            Time.timeScale -= Time.unscaledDeltaTime * transitionSpeed;
            foreach (var anim in flowerAnims) {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f) {
                    anim.SetFloat("Speed",-1.5f);
                }
            }
        }
    }

    public void HoldTimeScale() {
        if (Time.timeScale > holdTimeScale)
        {
            Time.timeScale -= Time.unscaledDeltaTime * transitionSpeed;
        }
    }


    //public void TimeSlowDown() {
    //    Time.timeScale = timeScale;
    //}

    
}
