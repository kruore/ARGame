using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewChange : MonoBehaviour
{
    public GUITestScrollView scrollview;
   public void ChangeScrolllView()
    {
        InventorysceneManager.inst.CpReSet();
        EventDelegate.Execute(DeleteDeck.eventdel);
        InventorysceneManager.inst.testScrollView = scrollview;
        InventorysceneManager.inst.currentcpDBstate = cpDBState.Deckmaking;
    }
}
