using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public AudioClip MainBGM;
    static MainManager Inst;
    // Start is called before the first frame update
    void Start()
    {
        Inst = this;
        if (SoundManager.Inst.Ds_musicSource.clip != MainBGM)
        {
            SoundManager.Inst.Ds_BgmPlayer(MainBGM);
        }
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("FadFanel__");
        Destroy(Fadepanel, 1f);
    }
    public void gotoMapscene()
    {
        Buttonmanager.Inst.gotoMapscene();
    }
    public void gotoInventory()
    {
        Buttonmanager.Inst.gotoInventory();
    }
    public void gotobattle()
    {
        Buttonmanager.Inst.gotobattle();
    }

}
