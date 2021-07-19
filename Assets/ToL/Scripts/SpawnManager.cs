using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject TargetObject;
    public GameObject Object;
    public Vector2 xPos;
    public Vector2 zPos;
    public int ObjectCount;
    public GameObject [] arrayList;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnObject()
    {
        for (int i = 0; i < arrayList.Length; i++)
        {
            Instantiate(Object, arrayList[i].transform.position, Quaternion.Euler(0, 180, 0));
        }
    }

    bool CheckSpawn(int count)
    {
        if (count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator spawnObject()
    {
        Vector3 SpawnPos = new Vector3(Random.Range(xPos.x, xPos.y), 0, Random.Range(zPos.x, zPos.y));
        ObjectCount -= 1;
        yield return new WaitForSeconds(1f);
    }
}
