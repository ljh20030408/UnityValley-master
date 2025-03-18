using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class LensFlareExtension
{

    public static Tweener DoBrightness(this LensFlare flare, float endValue, float duration)
    {
        return DOTween.To(() => flare.brightness, x => flare.brightness = x, endValue, duration);
    }
}
