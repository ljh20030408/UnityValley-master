using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateYoyo : MonoBehaviour
{

    private Transform cacheTransform;
    public float duration = 4.0f;
    public Vector3 targetAngle;

    void Start()
    {
        cacheTransform = this.transform;
        Rotate();
    }

    private void Rotate()
    {
        cacheTransform.DOLocalRotate(targetAngle, duration, RotateMode.Fast)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(delegate() { Rotate(); });
    }
}
