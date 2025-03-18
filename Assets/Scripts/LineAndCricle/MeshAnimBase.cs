using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnimBase : MonoBehaviour
{
    public float minDuration = 0.3f;
    public float maxDuration = 0.7f;
    public float minDelay = 0.3f;
    public float maxDelay = 0.8f;
    public bool animAtStart = false;

    public virtual void StartAnimation(bool forward) { }

}
