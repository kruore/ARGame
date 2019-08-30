using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneManager : MonoBehaviour
{
    public AudioClip MainBGM;
    private void Start()
    {
        if (SoundManager.Inst.Ds_musicSource.clip != MainBGM)
        {
            SoundManager.Inst.Ds_BgmPlayer(MainBGM);
        }
    }
    public void gotoMainScene()
    {
        if(0==PlayerPrefs.GetFloat("playercharactor"))
        {
            
        }
        Buttonmanager.Inst.gotoMainscene();
    }
}
