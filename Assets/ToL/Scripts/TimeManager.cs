using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.5f;
    public float slowdownLength = 2f;
    public TimeManager timeManager;

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("SLOW TIME");
            timeManager.DoSlowMotion();
        }
    }
}
