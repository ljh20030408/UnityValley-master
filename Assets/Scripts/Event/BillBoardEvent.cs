using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCenter
{

    public class BillBoardEvent
    {
        public delegate void ShowBillBoardHandler(bool show);

        public static ShowBillBoardHandler ShowBillBoardEvent;

        public static void RaiseShowBillBoard(bool show)
        {
            if (ShowBillBoardEvent!=null)
            {
                ShowBillBoardEvent(show);
            }
        }


        public delegate void SetBillBoardTargetHandler(Transform[] targetTfs);

        public static SetBillBoardTargetHandler SetBillBoardTargetEvent;

        public static void RaiseSetBillBoardTarget(Transform[] targetTfs)
        {
            if (SetBillBoardTargetEvent!=null)
            {
                SetBillBoardTargetEvent(targetTfs);
            }
        }

    }
}

