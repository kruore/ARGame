using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllcardInvisible : MonoBehaviour
{
    public void Allcard()
    {
        if (CPGameDataBase.inst.currentcpDBstate == cpDBState.Deactivecard)
        {
            CPGameDataBase.inst.currentcpDBstate = cpDBState.Deckmaking;
        }
        else
        {
            CPGameDataBase.inst.currentcpDBstate = cpDBState.Deactivecard;
        }
        
    }
}
