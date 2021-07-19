using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerController : MonoBehaviour
{
    Animator anim;
    public Vector2 growingTimeRange;

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
}
