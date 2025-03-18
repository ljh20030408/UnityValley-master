using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BillBoardItem : MonoBehaviour
{


    public CanvasGroup canvasGroup;
    public Text uiText;
    public Transform rearrangeTransform;
    public Transform cacheTransform;
    private int count;
    private float locakPositionY = 60f;
    private float delayRange = 0.5f;
    private float duration = 0.5f;
    void Start()
    {
        if (cacheTransform==null)
        {
            cacheTransform = this.transform;
        }
        if (rearrangeTransform==null)
        {
            rearrangeTransform = cacheTransform.GetChild(0);
        }
        canvasGroup.alpha = 0f;
        rearrangeTransform.localPosition=new Vector3(0f,locakPositionY,0f);
        gameObject.SetActive(false);
    }

    public void TweenAnim(bool forward)
    {
        float delay = Random.Range(0f, delayRange);
        if (forward)
        {
            gameObject.SetActive(true);
            canvasGroup.DOFade(1f, duration).SetDelay(delay);
            rearrangeTransform.DOLocalMoveY(0f, duration).SetDelay(delay);
        }
        else
        {
            canvasGroup.DOFade(0f, duration).SetDelay(delay).OnComplete(() => { gameObject.SetActive(false); });
            rearrangeTransform.DOLocalMoveY(locakPositionY, duration).SetDelay(delay);
        }
    }

    public void SetPosition(Vector3 localposition)
    {
        cacheTransform.localPosition = localposition;
    }
}
