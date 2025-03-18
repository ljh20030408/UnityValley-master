using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResize : MonoBehaviour
{

    public static ScreenResize Instance;
    private int m_ActiveHeight;
    private int m_ActiveWidth;
    public CanvasScaler canvasScaler;
    public float m_AspectRatio;

    public int activeWidth
    {
        get { return m_ActiveWidth; }
    }

    public int activeHeight
    {
        get { return m_ActiveHeight; }
    }

    void Awake()
    {
        Instance = this;       
    }

    void Start()
    {
        m_ActiveHeight = (int)canvasScaler.referenceResolution.y;
        m_AspectRatio = (float)Screen.width/Screen.height;
        m_ActiveWidth = (int)(m_ActiveHeight*m_AspectRatio);
    }
}
