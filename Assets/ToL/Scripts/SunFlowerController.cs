using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SunFlowerController : MonoBehaviour
{
    public TimeManager timeManager;
    Animator anim;
    [SerializeField]
    private float timeToGrow;
    public UnityEvent OnCompletedGrowing;
    private bool isStopping;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartGrowing());
    }

    IEnumerator StartGrowing() {
        yield return new WaitForSeconds(timeToGrow);
        anim.SetFloat("Speed", 1);
    }

    public void StopPlayAnim() {
        isStopping = true;
    }

    public void CompletedGrowing() {
        OnCompletedGrowing?.Invoke();
    }

    private void Update()
    {
        //if (!timeManager.canBreathe)
        //{
        //    if (anim.GetFloat("Speed") != 1) {
        //        anim.SetFloat("Speed", 1);
        //        anim.updateMode = AnimatorUpdateMode.Normal;
        //    }
        //}
        //else
        //{
        //    anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        //    switch (timeManager.state)
        //    {
        //        case BreathingState.Inhale:
        //            isStopping = false;
        //            anim.SetFloat("Speed", timeManager.flowerDeGrowSpeed);
        //            break;
        //        case BreathingState.Hold:

        //            break;
        //        case BreathingState.Exhale:
        //            if (!isStopping)
        //            {
        //                anim.SetFloat("Speed", timeManager.flowerGrowSpeed);
        //            }
        //            else
        //            {
        //                anim.SetFloat("Speed", 0);
        //            }
        //            break;
        //    }
        //}
    }
}
