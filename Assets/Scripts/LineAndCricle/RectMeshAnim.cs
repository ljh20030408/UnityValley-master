using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectMeshAnim : MeshAnimBase
{
    private RectMesh[] rectMeshes;
    private float[] lengths;
    private int count;

    void Start()
    {
        rectMeshes = gameObject.GetComponentsInChildren<RectMesh>();
        count = rectMeshes.Length;
        lengths=new float[count];
        for (int i = 0; i < count; i++)
        {
            lengths[i] = rectMeshes[i].length;
        }
        SetBeginState();
    }

    public override void StartAnimation(bool forward)
    {
        if (forward)
        {
            PlayForwardAnimation();
        }
        else
        {
            PlayBackwardAnimation();
        }
    }

    private void SetBeginState()
    {
        for (int i = 0; i < count; i++)
        {
            rectMeshes[i].length = 0;
        }
    }

    private void PlayForwardAnimation()
    {
        float duration;
        float delay;
        for (int i = 0; i < count; i++)
        {
            duration = Random.Range(minDuration, maxDuration);
            delay = Random.Range(minDelay, maxDelay);
            rectMeshes[i].DoLength(lengths[i], duration, delay);
        }
    }

    private void PlayBackwardAnimation()
    {
        float duration;
        float delay;
        for (int i = 0; i < count; i++)
        {
            duration = Random.Range(minDuration, maxDuration);
            delay = Random.Range(minDelay, maxDelay);
            rectMeshes[i].DoLength(0, duration, delay);
        }
    }
}
