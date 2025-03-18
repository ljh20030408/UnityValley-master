using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorWheelAnim : MonoBehaviour {

    public void SetBeginState(Transform[] targetTfs)
    {
        int count = targetTfs.Length;
        for (int i = 0; i < count; i++)
        {
            targetTfs[i].localScale = Vector3.zero;
        }
    }


    public void PlayForward(Transform[] targetTfs)
    {
        float duration = 0.08f;
        float delay;
        int count = targetTfs.Length;
        for (int i = 0; i < count; i++)
        {
            delay = i * 0.04f;
            targetTfs[i].DOScale(Vector3.one, duration).SetDelay(delay);
        }
    }

    public void PlayBackward(Transform[] targetTfs,TweenCallback callback)
    {
        float duration = 0.08f;
        float delay;
        int count = targetTfs.Length;
        for (int i = 0; i < count; i++)
        {
            delay = i * 0.04f;
            if (i==count-1)
            {
                targetTfs[i].DOScale(Vector3.zero, duration).SetDelay(delay).OnComplete(callback);
            }
            else
            {
                targetTfs[i].DOScale(Vector3.zero, duration).SetDelay(delay);
            }
        }
    }
}
