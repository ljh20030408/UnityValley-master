using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChildren : MonoBehaviour
{

    private Transform[] children;
    private int count;
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.3f;
    private float[] speeds;

    void Start()
    {
        count = this.transform.childCount;
        children=new Transform[count];
        speeds=new float[count];
        for (int i = 0; i < count; i++)
        {
            children[i] = this.transform.GetChild(i);
        }
        for (int i = 0; i < count; i++)
        {
            speeds[i] = Random.Range(minSpeed, maxSpeed);
        }
    }

    void Update()
    {
        for (int i = 0; i < count; i++)
        {
            children[i].Rotate(0f,speeds[i],0f);
        }
    }
}
