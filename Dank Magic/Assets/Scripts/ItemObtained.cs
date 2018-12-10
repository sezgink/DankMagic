using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObtained : MonoBehaviour {

    [SerializeField] GameObject itemObtainedTextObj;
    [SerializeField] GameObject itemParticle;

    float textWaitTime = 2f;

    public void ShowItem()
    {
        StartCoroutine(ShowTextThenClose());
    }

    IEnumerator ShowTextThenClose()
    {
        itemObtainedTextObj.SetActive(true);
        yield return new WaitForSeconds(textWaitTime);
        itemObtainedTextObj.SetActive(false);
    }
}
