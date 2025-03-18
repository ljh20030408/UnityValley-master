using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    private Transform cacheTransform;
    private Transform[] targetTfs;
    private BillBoardItem[] billBoardItems;
    public GameObject itemObject;
    private int count;
    private bool showBillBoard = false;
    private Vector3 tempV3;
     
    void Awake()
    {
        cacheTransform = this.transform;
        EventCenter.BillBoardEvent.ShowBillBoardEvent += ShowBillBoard;
        EventCenter.BillBoardEvent.SetBillBoardTargetEvent += SetBillBoardTarget;
    }

    private void SetBillBoardTarget(Transform[] targettfs)
    {
        this.targetTfs = targettfs;
        count = targettfs.Length;
        billBoardItems=new BillBoardItem[count];
        GameObject tempGo;
        for (int i = 0; i < count; i++)
        {
            tempGo = Instantiate(itemObject);
            billBoardItems[i] = tempGo.GetComponent<BillBoardItem>();
            billBoardItems[i].uiText.text = targettfs[i].gameObject.name.Replace("Billboard_", "");
            billBoardItems[i].transform.SetParent(cacheTransform);
            billBoardItems[i].transform.localScale=Vector3.one;
        }
    }

    private void ShowBillBoard(bool show)
    {
        showBillBoard = show;
        for (int i = 0; i < count; i++)
        {
            billBoardItems[i].TweenAnim(show);
        }
    }

    private void SetPosition()
    {
        for (int i = 0; i < count; i++)
        {
            tempV3 = Global.Instance.mainCamera.WorldToViewportPoint(targetTfs[i].position);
            tempV3.x *= ScreenResize.Instance.activeWidth;
            tempV3.y *= ScreenResize.Instance.activeHeight;
            tempV3.z = 0f;
            billBoardItems[i].SetPosition(tempV3);
        }
        
    }

    void Update()
    {
        if (showBillBoard)
        {
            SetPosition();
        }
    }

    void OnDestroy()
    {
        EventCenter.BillBoardEvent.ShowBillBoardEvent -= ShowBillBoard;
        EventCenter.BillBoardEvent.SetBillBoardTargetEvent -= SetBillBoardTarget;
    }
}
