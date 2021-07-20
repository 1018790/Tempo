using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextFading : MonoBehaviour
{
    Animator anim;
    public float preDisplayTime = 2f;
    public float displayTime = 3f;
    public float fadeOutTime = 1f;
    public UnityEvent OnCompleted;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PreDisplay());
    }

    IEnumerator PreDisplay() {
        yield return new WaitForSeconds(preDisplayTime);
        anim.Play("FadeIn");
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        yield return new WaitForSeconds(displayTime);
        anim.Play("FadeOut");
        yield return new WaitForSeconds(fadeOutTime);
        OnCompleted?.Invoke();
        gameObject.SetActive(false);
    }


   
}
