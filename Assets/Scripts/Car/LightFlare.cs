using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightFlare : MonoBehaviour
{

    private LensFlare[] flares;
    public Transform[] children;
    private Vector3[] originalPoses;
    private int count;
    private float positionMultiply=2.5f;

    private bool opened = false;

    public float sizeMultiply = 20f;
    void Start()
    {
        flares = gameObject.GetComponentsInChildren<LensFlare>();
        count = flares.Length;
        originalPoses=new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            originalPoses[i] = children[i].position;
        }
        gameObject.SetActive(false);
        EventCenter.UIEvent.LightFlareEvent += LightFlareEvent;
        EventCenter.UIEvent.ExplodeEvent += ExplodeEvent;    
        SetBeginState();      
    }

    void OnDestroy()
    {
        EventCenter.UIEvent.LightFlareEvent -= LightFlareEvent;
        EventCenter.UIEvent.ExplodeEvent -= ExplodeEvent;
    }

    private void ExplodeEvent(bool on)
    {
        if (on)
        {
            if (opened)
            {
                TurnOff();
            }
        }
    }


    private void LightFlareEvent(bool @on)
    {
        if (on)
        {
           TurnOn(); 
        }
        else
        {
            TurnOff();
        }
    }

    private void SetBeginState()
    {
        for (int i = 0; i < count; i++)
        {
            children[i].position = originalPoses[i]*positionMultiply;
        }
    }

    private void TurnOn()
    {
        opened = true;
        gameObject.SetActive(true);
        float duration = 0.7f;
        float delay;
        float flareDuration = 0.2f;
        Vector3 targetPos;
        for (int i = 0; i < count; i++)
        {
            delay = 0.05f*i;
            targetPos = originalPoses[i];
            children[i].DOMoveX(targetPos.x, duration).SetDelay(delay).SetEase(Ease.InCubic);
            children[i].DOMoveY(targetPos.y,duration).SetDelay(delay).SetEase(Ease.OutSine);
            children[i].DOMoveZ(targetPos.z, duration).SetDelay(delay).SetEase(Ease.InCubic);
            flares[i].DoBrightness(1f, flareDuration).SetDelay(delay + duration - 0.1f);
        }
    }

    private void TurnOff()
    {
        float duration = 0.7f;
        float delay;
        float flareDuration = 0.2f;
        Vector3 targetPos;

        for (int i = 0; i < count; i++)
        {
            delay = 0.05f*i + 0.2f;
            targetPos = originalPoses[i]*positionMultiply;
            children[i].DOMoveX(targetPos.x, duration).SetDelay(delay).SetEase(Ease.OutSine);            
            children[i].DOMoveZ(targetPos.z, duration).SetDelay(delay).SetEase(Ease.OutSine);
            if (i==count-1)
            {
                children[i].DOMoveY(targetPos.y, duration).SetDelay(delay).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    opened = false;
                    gameObject.SetActive(false);
                });
            }
            else
            {
                children[i].DOMoveY(targetPos.y, duration).SetDelay(delay).SetEase(Ease.InCubic);
            }
            flares[i].DoBrightness(1f, flareDuration).SetDelay(delay -0.2f);
            flares[i].DoBrightness(0f, flareDuration).SetDelay(delay + duration);
        }
    }

    void Update()
    {
        if (opened)
        {
            for (int i = 0; i < count; i++)
            {
                float size=sizeMultiply*-1f/Global.Instance.mainCameraTranfom.localPosition.z;
                flares[i].brightness = Mathf.Lerp(flares[i].brightness, size, Time.fixedDeltaTime*10);
            }
        }
    }
}
