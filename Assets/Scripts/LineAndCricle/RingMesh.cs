using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class RingMesh : PrimitiveBase
{

    public int segment = 1;
    public float ringWidth;
    public float _ringRadius;
    public float _angleBegin;
    public float _angleEnd;

    public float AngleBegin
    {
        get { return _angleBegin; }
        set { _angleBegin = value;UpdateShape(); }
    }

    public float AngleEnd
    {
        get { return _angleEnd; }
        set { _angleEnd = value;UpdateShape(); }
    }

    protected override void InitMesh()
    {
        if (cacheTransform==null||meshFilter==null)
        {
            Init();
        }
        segment = segment > 0 ? segment : 1;
        triangles=new int[segment*2*3];     
        for (int i = 0; i < segment; i++)
        {
            int k = i*6;
            int j = i*2;
            triangles[k] = j;  
            triangles[k + 1] = j + 2;
            triangles[k + 2] = j + 3;
            triangles[k + 3] = j;
            triangles[k + 4] = j + 3;
            triangles[k + 5] = j + 1;
        }
        int vertexCount = segment*2 + 2;
        uvs=new Vector2[vertexCount];
        float singleUV = 1f/segment;
        float uvY = 0f;
        for (int i = 0; i < vertexCount; i+=2)
        {
            uvs[i].x = 0f;
            uvs[i + 1].x = 1f;
            uvs[i].y = uvY;
            uvs[i + 1].y = uvY;
            uvY += singleUV;
        }     
        UpdateShape();   
    }

    protected override void UpdateShape()
    {
        int vertexCount = segment*2 + 2;
        vertices=new Vector3[vertexCount];

        float angle = _angleBegin*Mathf.Deg2Rad;
        float wHalf = ringWidth*0.5f;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float minRingRadius = _ringRadius - wHalf;
        float maxRingRadius = _ringRadius + wHalf;
        Vector3 localPos = offset;

        float x = cos*minRingRadius + localPos.x;
        float y = localPos.y;
        float z = sin*minRingRadius + localPos.z;

        vertices[0].x = x;
        vertices[0].y = y;
        vertices[0].z = z;

        x = cos*maxRingRadius + localPos.x;
        z = sin*maxRingRadius + localPos.z;

        vertices[1].x = x;
        vertices[1].y = y;
        vertices[1].z = z;

        float singleAngle = (_angleEnd - _angleBegin)/segment*Mathf.Deg2Rad;
        for (int i = 0; i < segment; i++)
        {
            angle += singleAngle;
            sin = Mathf.Sin(angle);
            cos = Mathf.Cos(angle);
            x = cos*minRingRadius + localPos.x;
            y = localPos.y;
            z = sin*minRingRadius + localPos.z;
            vertices[i*2+2]=new Vector3(x,y,z);

            x = cos*maxRingRadius + localPos.x;
            y = localPos.y;
            z = sin*maxRingRadius + localPos.z;
            vertices[i*2+3]=new Vector3(x,y,z);
        }
        UpdateMesh();
    }

    public Tweener DoEndAngle(float endValue, float duration, float delay)
    {
        return DOTween.To(() => AngleEnd, x => AngleEnd = x, endValue, duration).SetDelay(delay);
    }
}
