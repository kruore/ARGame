using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapsceneManager : MonoBehaviour
{
    public static MapsceneManager Inst;
    // Start is called before the first frame update
    void Start()
    {
        if (Inst!=null)
        {
            Inst = this;
        }
    }
    public void gotomainScene()
    {
        Buttonmanager.Inst.SceneStackPop();
        Buttonmanager.Inst.gotoMainscene();
    }
}
