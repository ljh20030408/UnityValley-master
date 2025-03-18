using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCenter
{
    public class CameraEvent : MonoBehaviour
    {
        public delegate void CameraReachAngleHanlder(int angleType, bool reach);

        public static event CameraReachAngleHanlder CameraReachAngleEvent;

        public static void RaiseCameraReachAngle(int angleType, bool reach)
        {
            if (CameraReachAngleEvent!=null)
            {
                CameraReachAngleEvent(angleType, reach);
            }
        }

    }

}

