using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TweenText : MonoBehaviour
{

    public Text uiText;
    public float duration = 2f;

    void Start()
    {
        if (uiText==null)
        {
            uiText = GetComponent<Text>();
        }
    }

    public void TweenTo(string text)
    {
        float halfDuration = duration*0.5f;
        uiText.DOFade(0f, halfDuration).OnComplete(() => { uiText.text = text; });
        uiText.DOFade(1f, halfDuration).SetDelay(halfDuration);
    }
}
