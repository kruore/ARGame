using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    private void Start()
    {
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("FadFanel__");
        Destroy(Fadepanel, 1f);
    }
    public void TouchBackButton()
    {
        Buttonmanager.Inst.TouchBackButton();
    }
}
