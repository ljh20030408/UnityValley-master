using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RectMesh : PrimitiveBase
{

    public float m_Width;
    public float m_Length;

    public float length
    {
        get { return m_Length; }
        set
        {
            m_Length = value;
            UpdateShape();
        }
    }
    public enum PivotAlign
    {
        Left,
        Center,
        Right,
    }
    public PivotAlign widthAlign=PivotAlign.Center;
    public PivotAlign lengthAlign=PivotAlign.Center;
    
   

    protected override void InitMesh()
    {
        if (cacheTransform == null || meshFilter == null)
        {
            Init();
        }
        triangles =new int[] {0,2,3,0,3,1};
        uvs=new Vector2[4];
        uvs[0].x = 0;
        uvs[0].y = 0;

        uvs[1].x = 1;
        uvs[1].y = 0;

        uvs[2].x = 0;
        uvs[2].y = 1;

        uvs[3].x = 1;
        uvs[3].y = 1;
        UpdateShape();
    }

    protected override void UpdateShape()
    {
        Vector3 localPos = offset;
        float w2 = m_Width*0.5f;
        float l2 = m_Length*0.5f;

        vertices=new Vector3[4];
        float x0, z0, x1, z1, x2, z2, x3, z3;
        x0 = z0 = x1 = z1 = x2 = z2 = x3 = z3 = 0;

        switch (widthAlign)
        {
            case PivotAlign.Left:
                x0 = 0f;
                x1 = m_Width;
                x2 = 0;
                x3 = m_Width;
                break;
            case PivotAlign.Center:
                x0 = -w2;
                x1 = w2;
                x2 = -w2;
                x3 = w2;
                break;
            case PivotAlign.Right:
                x0 = -m_Width;
                x1 = 0f;
                x2 = -m_Width;
                x3 = 0f;
                break;          
        }
        switch (lengthAlign)
        {
            case PivotAlign.Left:
                z0 = 0;
                z1 = 0;
                z2 = m_Length;
                z3 = m_Length;
                break;
            case PivotAlign.Center:
                z0 = -l2;
                z1 = -l2;
                z2 = l2;
                z3 = l2;
                break;
            case PivotAlign.Right:
                z0 = -m_Length;
                z1 = -m_Length;
                z2 = 0f;
                z3 = 0f;
                break;         
        }

        vertices[0].x = localPos.x + x0;
        vertices[0].y = localPos.y;
        vertices[0].z = localPos.z + z0;

        vertices[1].x = localPos.x + x1;
        vertices[1].y = localPos.y;
        vertices[1].z = localPos.z + z1;

        vertices[2].x = localPos.x + x2;
        vertices[2].y = localPos.y;
        vertices[2].z = localPos.z + z2;

        vertices[3].x = localPos.x + x3;
        vertices[3].y = localPos.y;
        vertices[3].z = localPos.z + z3;
        UpdateMesh();
    }

    public Tweener DoLength(float endValue, float duration, float delay)
    {
        return DOTween.To(() => length, x => length = x, endValue, duration).SetDelay(delay);
    }
}
