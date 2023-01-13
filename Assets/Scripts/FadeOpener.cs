using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeOpener : MonoBehaviour
{
    public void FadeIn()
    {
        GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }

    public void FadeOut()
    {
        GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(()=>gameObject.SetActive(false));
    }
}
