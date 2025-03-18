using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance;
    public Transform mainCameraTranfom;
    public Camera mainCamera;
    void Awake()
    {
        Instance = this;
    }
    
}
