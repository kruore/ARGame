using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpdbstatenomal : MonoBehaviour
{
    public void cpdbstateNomal()
    {
        CPGameDataBase.inst.currentcpDBstate = cpDBState.Nomal;
    }
}
