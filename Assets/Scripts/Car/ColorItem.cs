using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorItem : MonoBehaviour
{

    private Material material;

    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    public void SetStartPoint(Vector3 point)
    {
        material.SetVector("_StartPos",point);
    }

    public void SetMainColor(Color color)
    {
        material.SetColor("_MainColor",color);
    }

    public void SetTargetColor(Color color)
    {
        material.SetColor("_TargetColor",color);
    }

    public void SetOffset(float offset)
    {
        material.SetFloat("_WaveOffset",offset);
    }
}
