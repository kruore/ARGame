using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewChange : MonoBehaviour
{
    public GUITestScrollView scrollview;
   public void ChangeScrolllView()
    {
        CPGameDataBase.inst.testScrollView = scrollview;
        CPGameDataBase.inst.currentcpDBstate = cpDBState.Deckmaking;
    }
}
