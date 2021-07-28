using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectPool : MonoBehaviour
{
    [SerializeField]
    private ObjectPool rainObjectPool;

    public float height;
    public Vector2 constrainPosX;
    public Vector2 constrainPosZ;
    public float timeToSpawn = 0.1f;

    private void Start()
    {
        InvokeRepeating("SpawnRainCollider",0,timeToSpawn);
    }

    private void SpawnRainCollider() {
        GameObject go = rainObjectPool.GetObject();
        float randomPosX = Random.Range(constrainPosX.x,constrainPosX.y);
        float randomPosZ = Random.Range(constrainPosZ.x,constrainPosZ.y);
        if (go)
            go.transform.localPosition = new Vector3(randomPosX, height, randomPosZ);

    }
}
