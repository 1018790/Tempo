using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateDebug : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;
    private float fps;

    public TextMeshProUGUI frameRateText;
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
        frameRateText.text = fps.ToString("f0");
    }
}
