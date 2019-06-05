using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllcardInvisible : MonoBehaviour
{
    public void Allcard()
    {
        if (InventorysceneManager.inst.currentcpDBstate.Equals(cpDBState.Deactivecard))
        {
            InventorysceneManager.inst.currentcpDBstate = cpDBState.Deckmaking;
        }
        else
        {
            InventorysceneManager.inst.currentcpDBstate = cpDBState.Deactivecard;
        }
        
    }
}
