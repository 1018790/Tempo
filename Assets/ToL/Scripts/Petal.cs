using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Petal : MonoBehaviour
{
    [SerializeField]
    private SunflowerPetalsFading petalMaster;
    public float fadeInTime = 0.19f;
    public float fadeOutTime = 0.28f;
    Image thisImage;
    private void Awake()
    {
        thisImage = GetComponent<Image>();
    }

    public void FadeInFunction() {
        StartCoroutine(FadeIn());
    }

    public void FadeOutFunction() {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn() { 
        thisImage.CrossFadeAlpha(1,fadeInTime,false);
        yield return new WaitForSeconds(fadeInTime);
        petalMaster.AddIndex();
        //OnFadeInCompleted?.Invoke();
    }

    IEnumerator FadeOut() {
        thisImage.CrossFadeAlpha(0, fadeOutTime, false);
        yield return new WaitForSeconds(fadeOutTime);
        petalMaster.DecreaseIndex();
    }

}
