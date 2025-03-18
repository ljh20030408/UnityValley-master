using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLock : MonoBehaviour
{
    public static MouseLock Instance;

    public GraphicRaycaster raycaster;


    void Awake()
    {
        Instance = this;
    }

    public void Lock(float duration)
    {
        SetDeactive();
        CancelInvoke("SetActive");
        Invoke("SetActive",duration);
    }

    private void SetDeactive()
    {
        raycaster.enabled = false;
    }

    private void SetActive()
    {
        raycaster.enabled = true;
    }
}
