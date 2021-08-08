using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialFlower : MonoBehaviour
{
    [SerializeField]
    private TimeManager timeManager;
    private Image thisImage;
    private bool hasResetted;
    private float currentInhaleTimer;
    private float currentExhaleTimer;

    private void Start()
    {
        thisImage = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (timeManager.canBreathe) {
            switch (timeManager.state)
            {
                case BreathingState.Inhale:
                    currentExhaleTimer = 0;
                    thisImage.fillAmount = (currentInhaleTimer += Time.fixedUnscaledDeltaTime) / timeManager.breathingPhase[timeManager.currentPhaseIndex].inhaleTime;

                    break;
                case BreathingState.Exhale:
                    currentInhaleTimer = 0;
                    thisImage.fillAmount = 1f - ((currentExhaleTimer += Time.fixedUnscaledDeltaTime) / timeManager.breathingPhase[timeManager.currentPhaseIndex].exhaleTime);

                    break;
            }
        }
    }
}
