using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public GameObject prefabToSpawn;
    public int poolSize = 5;
    [SerializeField]
    private bool canInit;
    [SerializeField]
    private List<GameObject> poolList = new List<GameObject>();

    public DynamicBone[] bones;
    
    void Start()
    {
        if (canInit) {
            for (int i = 0; i < poolSize; i++)
            {
                GenerateObject();
            }
        }
    }

    public GameObject GetObject() {
        if (poolList.Count > 0) {
            GameObject currentGo = poolList[poolList.Count - 1];
            currentGo.SetActive(true);
            poolList.Remove(currentGo);
            return currentGo;
        } else if (poolList.Count == 0) {
            GenerateObject();
        }
        return null;
    }

    public void ReturnToList(GameObject _go) {
        _go.SetActive(false);
        poolList.Add(_go);
    }

    public void GenerateObject() {
        GameObject go = Instantiate(prefabToSpawn);
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].m_Colliders.Add(go.GetComponent<DynamicBoneCollider>());
        }
        //bone.m_Colliders.Add(go.GetComponent<DynamicBoneCollider>());
        go.transform.parent = transform;
        go.SetActive(false);
        poolList.Add(go);
    }
}
