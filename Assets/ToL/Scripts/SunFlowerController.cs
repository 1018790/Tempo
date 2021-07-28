using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SunFlowerController : MonoBehaviour
{
    Animator anim;
    public Vector2 growingTimeRange;
    public UnityEvent OnCompletedGrowing;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartGrowing());
    }

    IEnumerator StartGrowing() {
        yield return new WaitForSeconds(GetRandomTime());
        anim.SetFloat("Speed",1);
    }

    private float GetRandomTime() {
        return Random.Range(growingTimeRange.x,growingTimeRange.y);
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            Debug.Log("ss");
            OnCompletedGrowing?.Invoke();
        }
    }
}
