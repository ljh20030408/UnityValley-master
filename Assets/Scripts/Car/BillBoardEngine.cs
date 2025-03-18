using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardEngine : MonoBehaviour
{

    public Transform[] billBoardTfs;
	
	void Start () {
		EventCenter.BillBoardEvent.RaiseSetBillBoardTarget(billBoardTfs);
	}
	
	
}
