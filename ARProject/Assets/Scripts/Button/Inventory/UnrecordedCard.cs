using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnrecordedCard : MonoBehaviour
{
    public void UnrecordedCardButton()
    {
        InventorysceneManager.inst.currentcpDBstate = cpDBState.Deactivecard;
    }
}
