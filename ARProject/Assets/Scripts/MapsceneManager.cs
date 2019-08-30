using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapsceneManager : MonoBehaviour
{
    public static MapsceneManager Inst;
    public AudioClip MapBGM;
    // Start is called before the first frame update
    void Start()
    {
        if (Inst!=null)
        {
            Inst = this;
        }
        SoundManager.Inst.Ds_BgmPlayer(MapBGM);
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("FadFanel__");
        Destroy(Fadepanel, 1f);
    }
    public void TouchBackButton()
    {
        Buttonmanager.Inst.TouchBackButton();
    }
}
