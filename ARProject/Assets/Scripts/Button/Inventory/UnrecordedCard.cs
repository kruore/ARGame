using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnrecordedCard : MonoBehaviour
{
    public void UnrecordedCardButton()
    {
        CPGameDataBase.inst.currentcpDBstate = cpDBState.Deactivecard;
    }
}
