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
    private GameObject[] petals;
    private int petalIndex;
    private bool isFading;

    void Start()
    {
        petals = new GameObject[petalParent.childCount];

        for (int i = 0; i < petals.Length; i++)
        {
            petals[i] = petalParent.GetChild(i).gameObject;
            petals[i].GetComponent<Image>().CrossFadeAlpha(0,0.1f,false);
        }
        Array.Reverse(petals);
    }

    public void AddIndex() {
        if (petalIndex < petals.Length-1) {
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
                        petals[petalIndex].GetComponent<Petal>().FadeOutFunction();
                        isFading = false;
                    }
                    break;
            }
        }

    }

}
