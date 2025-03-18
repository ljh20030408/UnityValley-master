using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamgeObjectUtil
{

    public static Vector3 MultiplayEachElement(this Vector3 sourceVector3, Vector3 targetVector3)
    {
        return new Vector3(sourceVector3.x*targetVector3.x,sourceVector3.y*targetVector3.y,sourceVector3.z*targetVector3.z);
    }
    public static T AddComponentIfNotHave<T>(this GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }
}
