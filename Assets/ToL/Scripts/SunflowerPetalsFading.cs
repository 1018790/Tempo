using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SunflowerPetalsFading : MonoBehaviour
{
    public TimeManager timeManager;
    [SerializeField]
    private Transform petalParent;
    [SerializeField]
    private Transform petalGlowParent;
    [SerializeField]
    private GameObject[] petals;
    [SerializeField]
    private GameObject[] petalGlows;
    private int petalIndex;
    private int petalGlowIndex;
    private bool isFading;

    void Start()
    {
        petals = new GameObject[petalParent.childCount];
        petalGlows = new GameObject[petalGlowParent.childCount];
        for (int i = 0; i < petals.Length; i++)
        {
            petals[i] = petalParent.GetChild(i).gameObject;
            petals[i].GetComponent<Image>().CrossFadeAlpha(0,0.1f,false);
            petalGlows[i] = petalGlowParent.GetChild(i).gameObject;
            petalGlows[i].GetComponent<Image>().CrossFadeAlpha(0,0.1f,false);
        }

        Array.Reverse(petals);
        Array.Reverse(petalGlows);
    }

    public void AddIndex(int _type) {
        switch (_type) {
            case 0:
                if (petalIndex < petals.Length - 1)
                {
                    petalIndex++;
                    petals[petalIndex].GetComponent<Petal>().FadeInFunction();
                }
                break;
            case 1:
                if (petalGlowIndex < petals.Length - 1)
                {
                    petalGlowIndex++;
                    petalGlows[petalGlowIndex].GetComponent<Petal>().FadeInFunction();
                }
                break;
        }
    }

    public void DecreaseIndex(int _type) {
        switch (_type) { 
            case 0:
                if (petalIndex > 0)
                {
                    petalIndex--;
                    petals[petalIndex].GetComponent<Petal>().FadeOutFunction();
                }
                break;

            case 1:
                if (petalGlowIndex > 0)
                {
                    petalGlowIndex--;
                    petalGlows[petalGlowIndex].GetComponent<Petal>().FadeOutFunction();
                }
                break;
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
                        petalGlows[petalGlowIndex].GetComponent<Petal>().FadeInFunction();
                        isFading = true;
                    }

                    break;
                case BreathingState.Exhale:
                    if (isFading)
                    {
                        petals[petalIndex].GetComponent<Petal>().FadeOutFunction();
                        petalGlows[petalGlowIndex].GetComponent<Petal>().FadeOutFunction();
                        isFading = false;
                    }
                    break;
            }
        }

    }

}
