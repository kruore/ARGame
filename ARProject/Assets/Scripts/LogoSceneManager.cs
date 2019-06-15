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
            Debug.Log(PlayerPrefs.GetInt("Jewely", -1));
        }
    }
    public void gotoMainScene()
    {
        if (-1 == PlayerPrefs.GetInt("Jewely", -1))
        {
            Buttonmanager.Inst.gotoCreatsecene();
        }
        else
        {
            Buttonmanager.Inst.gotoMainscene();
        }
    }
}
