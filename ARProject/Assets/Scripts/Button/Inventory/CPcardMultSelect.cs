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
        }
        else
        {
            CPGameDataBase.inst.currentcpDBstate = cpDBState.Deckmaking;
            CPGameDataBase.inst.testScrollView.EV_UpdateAll();
        }
    }
}
