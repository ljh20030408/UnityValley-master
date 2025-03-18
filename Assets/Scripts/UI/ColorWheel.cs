using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorWheel : MonoBehaviour
{

    public Transform[] colorTfs;
    public Transform rootTransform;
    private bool isOpend = false;
    public ColorWheelAnim anim;

    public Color[] colors;

    void Start()
    {
        InitColliderAddListeners();
        SetBeginState();
        EventCenter.ConfigEvent.ClickBodyEvent += ClickBodyEvent;
    }

    private void ClickBodyEvent(bool click)
    {
        if (click)
        {
            if (!isOpend)
            {
                SetPosition();
                ShowColorWheel(true);
                isOpend = true;
            }
            else
            {
                ShowColorWheel(false);
                isOpend = false;
            }
        }
        MouseLock.Instance.Lock(2f);
    }

    private void InitColliderAddListeners()
    {
        int count = colorTfs.Length;
        for (int i = 0; i < count; i++)
        {
            colorTfs[i].gameObject.AddComponent<MeshCollider>();
            EventTriggerListener.Get(colorTfs[i].gameObject).onClick += OnClickChild;
        }      
    }
   

    private void SetBeginState()
    {
        anim.SetBeginState(colorTfs);
        rootTransform.gameObject.SetActive(false);
    }
    private void OnClickChild(GameObject go, PointerEventData eventdata)
    {
        int colorIndex = int.Parse(go.name);
        EventCenter.ConfigEvent.RaiseClickColorWheel(colors[colorIndex],eventdata);
        ShowColorWheel(false);
        isOpend = false;
    }

    private void ShowColorWheel(bool show)
    {
        if (show)
        {
            anim.PlayForward(colorTfs);
            rootTransform.gameObject.SetActive(true);
            SoundManager.Instance.PlayColorWheelShow();
        }
        else
        {
            anim.PlayBackward(colorTfs,()=>rootTransform.gameObject.SetActive(show));
        }
    }

    private void SetPosition()
    {
        rootTransform.localPosition = Input.mousePosition;       
    }
}
