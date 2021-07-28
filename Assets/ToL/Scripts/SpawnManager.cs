using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public Vector2 xPos;
    public Vector2 zPos;
    public int ObjectCount;
    public GameObject [] arrayList;
    public GameObject rainSpawner;
    public GameObject flower;
    public GameObject rain;
    public Animator flowerAnim;

    public void SpawnObject()
    {
        for (int i = 0; i < arrayList.Length; i++)
        {
            GameObject f = Instantiate(flower, arrayList[i].transform.position, Quaternion.Euler(0, 180, 0));
            f.transform.SetParent(transform,true);
            if (flowerAnim.GetCurrentAnimatorStateInfo(0).IsName("Growing"))
            {
                GameObject r = Instantiate(rain, rainSpawner.transform.position, Quaternion.Euler(0, 0, 0));
                r.transform.SetParent(transform, true);
            }
        }
    }
}
