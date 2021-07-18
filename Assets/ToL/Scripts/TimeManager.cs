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

    void Start()
    {
        defaultTimeScale = Time.timeScale;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }
    void Update()
    {
        if (Input.GetKeyUp(timeScaleKey))
        {
            //Reverse the time toggle and change the timeScale
            timeToggle = !timeToggle;
            Time.timeScale = timeToggle ? timeScale : defaultTimeScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
    }

    public void ResetTimeScale() {
        Time.timeScale = 1;
    }

    public void TimeSlowDown() {
        Time.timeScale = timeScale;
    }
}
