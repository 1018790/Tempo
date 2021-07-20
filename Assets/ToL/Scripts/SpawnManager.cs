using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Object;
    public Vector2 xPos;
    public Vector2 zPos;
    public int ObjectCount;
    public GameObject [] arrayList;

    public void SpawnObject()
    {
        for (int i = 0; i < arrayList.Length; i++)
        {
            GameObject o = Instantiate(Object, arrayList[i].transform.position, Quaternion.Euler(0, 180, 0));
            o.transform.SetParent(transform,true);
        }
    }
}
