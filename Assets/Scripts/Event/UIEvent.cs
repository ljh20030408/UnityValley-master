using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCenter
{
    public class UIEvent
    {

        public delegate void LightFlareHandler(bool on);

        public static event LightFlareHandler LightFlareEvent;

        public static void RaiseLightFlare(bool on)
        {
            if (LightFlareEvent!=null)
            {
                LightFlareEvent(on);
            }
        }


        public delegate void ExplodeHandler(bool on);

        public static event ExplodeHandler ExplodeEvent;

        public static void RaiseExplode(bool on)
        {
            if (ExplodeEvent!=null)
            {
                ExplodeEvent(on);
            }
        }
    }

}

