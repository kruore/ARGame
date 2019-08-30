using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreateManager : MonoBehaviour
{
    public Item playercontect;
    public static PlayerCreateManager Inst;
    public GameObject CardInfo;
    private void Start()
    {
        Inst = this;
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("FadFanel__");
        Destroy(Fadepanel, 1f);
    }
    public void Activefalse()
    {
        UIButton.current.gameObject.SetActive(false);
    }
    public void DBinsert()
    {
        GameDataBase.Inst.Ds_InventoryInsert(playercontect.cardInventoryNum);
        Buttonmanager.Inst.gotoMainscene();
    }
}
