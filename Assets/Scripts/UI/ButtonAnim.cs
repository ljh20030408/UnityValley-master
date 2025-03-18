using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{

    public TweenText tweenText;
    public RingMeshAnim ringMeshAnim;

    public string OnText = "ON";
    public string OffText = "OFF";

    public void Open()
    {
        tweenText.TweenTo(OnText);   
        ringMeshAnim.StartAnimToAndBack();     
    }

    public void Close()
    {
        tweenText.TweenTo(OffText);
        ringMeshAnim.StartAnimToAndBack();
    }
}
