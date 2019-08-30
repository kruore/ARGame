using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDismantiling : MonoBehaviour
{
    public void Dismantiling()
    {
        for(int i=InventorysceneManager.inst.DestoryItem.Count-1;i>=0;i--)
        {
            InventorysceneManager.inst.cpcards_Inventory.RemoveAt(InventorysceneManager.inst.DestoryItem[i]);
        }
        foreach(Cardcheck check in InventorysceneManager.inst.testScrollView.gameObject.transform.Find("Grid").GetComponentsInChildren<Cardcheck>())
        {
            check.objectcheck = false;
            check.CardCheck();
        }
        Buttonmanager.Inst.SaveDB();
    }
}
