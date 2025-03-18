using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnimManager : MonoBehaviour
{
    public GameObject leftRoot;
    public GameObject rightRoot;

    private MeshAnimBase[] leftMeshAnims;
    private MeshAnimBase[] rightMeshAnims;

    void Awake()
    {
        InitAnimation();
    }
    void Start()
    {
       
        EventCenter.CameraEvent.CameraReachAngleEvent += CameraReachAngle;
    }

    void OnDestroy()
    {
        EventCenter.CameraEvent.CameraReachAngleEvent -= CameraReachAngle;
    }

    private void CameraReachAngle(int angletype, bool reach)
    {
        switch (angletype)
        {
            case 0:
                StartLeftAnimation(reach);
                break;
            case 1:
                StartRightAnimation(reach);
                break;
                
        }
    }

    private void StartLeftAnimation(bool forward)
    {
        if (leftMeshAnims==null)
        {
            return;
        }
        int count = leftMeshAnims.Length;
        for (int i = 0; i < count; i++)
        {
            leftMeshAnims[i].StartAnimation(forward);
        }
    }

    private void StartRightAnimation(bool forward)
    {
        if (rightMeshAnims == null)
        {
            return;
        }
        int count = rightMeshAnims.Length;
        for (int i = 0; i < count; i++)
        {
            rightMeshAnims[i].StartAnimation(forward);
        }
    }

    private void InitAnimation()
    {
        if (leftRoot!=null)
        {
            leftMeshAnims = leftRoot.GetComponentsInChildren<MeshAnimBase>();
        }
        if (rightRoot!=null)
        {
            rightMeshAnims = rightRoot.GetComponentsInChildren<MeshAnimBase>();
        }
    }

}
