using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolList : MonoBehaviour
{
    private ObjectPool pool;
    public float secToReturrn = 1f;

    private void OnEnable()
    {
        pool = GetComponentInParent<ObjectPool>();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(ReturnToList());
    }

    IEnumerator ReturnToList() {
        yield return new WaitForSeconds(secToReturrn);
        pool.ReturnToList(this.gameObject);
    }
}
