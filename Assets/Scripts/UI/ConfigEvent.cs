using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace EventCenter
{
    public static class ConfigEvent
    {

        public delegate void ClickColorWheelHandler(Color color, PointerEventData eventData);
        public static event ClickColorWheelHandler ClickColorWheelEvent;

        public static void RaiseClickColorWheel(Color color,PointerEventData eventData)
        {
            if (ClickColorWheelEvent!=null)
            {
                ClickColorWheelEvent(color,eventData);
            }
        }

        public delegate void ClickBodyHandler(bool click);

        public static event ClickBodyHandler ClickBodyEvent;

        public static void RaiseClickBody(bool click)
        {
            if (ClickBodyEvent != null)
            {
                ClickBodyEvent(click);
            }
        }
    }
}

