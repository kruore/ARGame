using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPcardMultSelect : MonoBehaviour
{
    public void CPstatemultiselect()
    {
        if (CPGameDataBase.inst.currentcpDBstate != cpDBState.Multiselect)
        {
            CPGameDataBase.inst.currentcpDBstate = cpDBState.Multiselect;
            CPGameDataBase.inst.testScrollView.EV_UpdateAll();
            UIButton.current.gameObject.GetComponentInChildren<UILabel>().text = "취소";
        }
        else
        {
            CPGameDataBase.inst.currentcpDBstate = cpDBState.Deckmaking;
            CPGameDataBase.inst.testScrollView.EV_UpdateAll();
            UIButton.current.gameObject.GetComponentInChildren<UILabel>().text = "다중 선택";
        }
    }
}
