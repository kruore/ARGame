using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllcardInvisible : MonoBehaviour
{
    public void Allcard()
    {
        CPGameDataBase.inst.currentcpDBstate = cpDBState.Deactivecard;
        
    }
}
