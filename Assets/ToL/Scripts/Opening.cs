using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    public List<GameObject> textOrder;
    public float timeBetween = 2f;
    private int orderIndex;

    private void Update()
    {
        if (orderIndex < textOrder.Count - 1)
        {
            if (!textOrder[orderIndex].gameObject.activeInHierarchy)
            {
                orderIndex++;
                textOrder[orderIndex].SetActive(true);
            }
        }
    }
}
