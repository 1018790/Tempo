using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    public List<GameObject> textOrder;
    [SerializeField]
    private Image beginningImage;
    [SerializeField]
    private Image flowerCentral;
    public float timeBetween = 2f;
    private int orderIndex;

    private void Start()
    {
        flowerCentral.CrossFadeAlpha(0,0.1f,false);
    }

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

    public void FadeOutBeginningImage() {
        beginningImage.CrossFadeAlpha(0,3f,false);
        flowerCentral.CrossFadeAlpha(1, 2f, false);
    }
}
