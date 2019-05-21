using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDismantiling : MonoBehaviour
{
    public void Dismantiling()
    {
        for(int i=CPGameDataBase.inst.DestoryItem.Count-1;i>=0;i--)
        {
            CPGameDataBase.inst.cpcards_Inventory.RemoveAt(CPGameDataBase.inst.DestoryItem[i]);
        }
        foreach(Cardcheck check in CPGameDataBase.inst.testScrollView.gameObject.transform.Find("Grid").GetComponentsInChildren<Cardcheck>())
        {
            check.objectcheck = false;
            check.CardCheck();
        }
        Buttonmanager.Inst.SaveDB();
    }
}
