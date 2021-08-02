using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateDebug : MonoBehaviour
{
    int fps = 0;
    public TextMeshProUGUI frameRateText;
    void Update()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        frameRateText.text = fps.ToString();

    }
}
