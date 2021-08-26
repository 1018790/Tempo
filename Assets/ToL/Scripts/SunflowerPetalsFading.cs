using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class SunflowerPetalsFading : MonoBehaviour
{
    public TimeManager timeManager;
    [SerializeField]
    private Transform petalParent;
 
    [SerializeField]
    private GameObject[] petals;
    [SerializeField]
    private GameObject[] petalGlows;
    private int petalIndex;
    private int petalGlowIndex;
    private bool isFading;

    public int cycleCounter;
    public int cyclesToWind = 5;
    public int cyclesToRain = 20;
    public int cyclesToRainbow = 30;
    public UnityEvent OnWind;
    public UnityEvent OnRain;
    public UnityEvent OnRainbow;


    void Start()
    {
        petals = new GameObject[petalParent.childCount];
        for (int i = 0; i < petals.Length; i++)
        {
            petals[i] = petalParent.GetChild(i).gameObject;
            petals[i].GetComponent<Image>().CrossFadeAlpha(0,0.1f,false);
        }

        Array.Reverse(petals);
        Array.Reverse(petalGlows);
        cycleCounter = 0;
    }

    public void AddIndex() {
        if (petalIndex < petals.Length - 1)
        {
            petalIndex++;
            petals[petalIndex].GetComponent<Petal>().FadeInFunction();
        }
    }

    public void DecreaseIndex() {
        if (petalIndex > 0)
        {
            petalIndex--;
            petals[petalIndex].GetComponent<Petal>().FadeOutFunction();
        }
    }

    private void Update()
    {
        if (timeManager.canBreathe)
        {
            switch (timeManager.state)
            {
                case BreathingState.Inhale:
                    if (!isFading)
                    {
                        petals[petalIndex].GetComponent<Petal>().FadeInFunction();
                        isFading = true;
                    }

                    break;
                case BreathingState.Exhale:
                    if (isFading)
                    {
                        cycleCounter += 1;
                        if (cycleCounter == cyclesToWind) {
                            OnWind?.Invoke();
                        } else if (cycleCounter == cyclesToRain) {
                            OnRain?.Invoke();
                        } else if (cycleCounter == cyclesToRainbow) {
                            OnRainbow?.Invoke();
                        }
                        petals[petalIndex].GetComponent<Petal>().FadeOutFunction();
                        isFading = false;
                    }
                    break;
            }
        }

    }

}
