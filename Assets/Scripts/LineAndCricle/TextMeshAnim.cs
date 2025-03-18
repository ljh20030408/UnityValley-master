using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TextMeshAnim : MeshAnimBase
{
    private Transform[] textTf;
    private Vector3[] scales;
    private int count;

    void Start()
    {
        TextMesh[] textMeshes = gameObject.GetComponentsInChildren<TextMesh>();
        count = textMeshes.Length;
        textTf=new Transform[count];
        scales=new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            textTf[i] = textMeshes[i].transform;
            scales[i] = textTf[i].localScale;
        }
        SetBeginState();
    }

    private void SetBeginState()
    {
        for (int i = 0; i < count; i++)
        {
            textTf[i].localScale=Vector3.zero;
        }
    }

    public override void StartAnimation(bool forward)
    {
        if (forward)
        {
            PlayForward();
        }
        else
        {
            PlayBackward();
        }
    }

    private void PlayForward()
    {
        float duration;
        float delay;
        for (int i = 0; i < count; i++)
        {
            duration = Random.Range(minDuration, maxDuration);
            delay = Random.Range(minDelay, maxDelay);
            textTf[i].DOScale(scales[i], duration).SetDelay(delay);
        }
    }

    private void PlayBackward()
    {
        float duration;
        float delay;
        for (int i = 0; i < count; i++)
        {
            duration = Random.Range(minDuration, maxDuration);
            delay = Random.Range(minDelay, maxDelay);
            textTf[i].DOScale(Vector3.zero, duration).SetDelay(delay);
        }
    }

}
