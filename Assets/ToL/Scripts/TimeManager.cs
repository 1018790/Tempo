using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public KeyCode timeScaleKey = KeyCode.Z;
    public float timeScale;
    bool timeToggle;
    float defaultTimeScale;
    float defaultFixedDeltaTime;

    public bool canBreathe;
    public float inhaleTime = 4f;
    [SerializeField]
    private float currentInhaleTime;
    public float exhaleTime = 8f;
    [SerializeField]
    private float currentExhaleTime;

    void Start()
    {
        defaultTimeScale = Time.timeScale;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }
    void Update()
    {
        if (canBreathe) {
            if (currentInhaleTime < inhaleTime)
            {
                currentInhaleTime += Time.unscaledDeltaTime;
                TimeSlowDown();
            }
            else
            {
                if (currentExhaleTime < exhaleTime)
                {
                    currentExhaleTime += Time.unscaledDeltaTime;
                    ResetTimeScale();
                }
                else
                {
                    currentExhaleTime = 0;
                    currentInhaleTime = 0;
                }
            }
        }
      



        if (Input.GetKeyUp(timeScaleKey))
        {
            //Reverse the time toggle and change the timeScale
            timeToggle = !timeToggle;
            Time.timeScale = timeToggle ? timeScale : defaultTimeScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
    }

    public void YouCanBreathe() {
        canBreathe = true;
    }

    public void ResetTimeScale() {
        Time.timeScale = 1;
    }

    public void TimeSlowDown() {
        Time.timeScale = timeScale;
    }

    
}
