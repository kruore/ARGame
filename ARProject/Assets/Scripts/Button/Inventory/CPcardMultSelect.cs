using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPcardMultSelect : MonoBehaviour
{
    public void CPstatemultiselect()
    {
        if (InventorysceneManager.inst.currentcpDBstate != cpDBState.Multiselect)
        {
            InventorysceneManager.inst.currentcpDBstate = cpDBState.Multiselect;
            InventorysceneManager.inst.testScrollView.EV_UpdateAll();
            UIButton.current.gameObject.GetComponentInChildren<UILabel>().text = "취소";
        }
        else
        {
            InventorysceneManager.inst.currentcpDBstate = cpDBState.Deckmaking;
            InventorysceneManager.inst.testScrollView.EV_UpdateAll();
            UIButton.current.gameObject.GetComponentInChildren<UILabel>().text = "다중 선택";
        }
    }
}
