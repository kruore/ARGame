﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager> {
    public AudioSource Ds_efxSource;//효과음
    public AudioSource Ds_musicSource;//배경음악
    public Slider Ds_soundslider;//옵션.사운드조절바
    public float Ds_volumeRange=0.5f;//현재사운드 크기
    private void Awake()
    {
        Ds_efxSource = gameObject.AddComponent<AudioSource>();
        Ds_efxSource.loop = false;
        Ds_musicSource = gameObject.AddComponent<AudioSource>();
        Ds_musicSource.loop = true;
        
    }
    //배경음 재생 GameObject.Find("SoundManager").GetComponent<SoundManager>().Ds_BgmPlayer(Audio ***);
    public void Ds_BgmPlayer(AudioClip clip)
    {
        Ds_musicSource.clip = clip;
        Ds_musicSource.Play();
        Ds_musicSource.volume = Ds_volumeRange;
    }
    public void Ds_PlaySingle(AudioClip clip)
    {
        //하나의 효과음재생 GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySingle(Audio ***);
        Ds_efxSource.clip = clip;
        Ds_efxSource.Play();
        Ds_efxSource.volume = Ds_volumeRange;
    }
    public void Ds_RandomizeSfx(params AudioClip[]clips)
    {
        //여러개의 효과음중 하나만 재생하고 싶을때 GameObject.Find("SoundManager").GetComponent<SoundManager>().RandomizeSfx(Audio***,*** ...);
        int randomIndex = Random.Range(0, clips.Length);
        Ds_efxSource.clip = clips[randomIndex];
        Ds_efxSource.Play();
        Ds_efxSource.volume = Ds_volumeRange;

    }
    // Use this for initialization
    
    public void Ds_SoundController()//슬라이더가 움직일 때마다 호출되어 사운드의 조절을 해준다.
    {
        Debug.Log(Ds_soundslider.value);
        Ds_volumeRange = Ds_soundslider.value;
        Debug.Log(Ds_volumeRange);
        Ds_musicSource.volume = Ds_volumeRange;
        Ds_efxSource.volume = Ds_volumeRange;
    }
    public void SoundInit()//설정 버튼을 눌렀을 시 설정창의 슬라이더를 현재의 볼륨과 동기화 시켜준다.
    {
        Ds_soundslider = GameObject.Find("SoundSlider").GetComponent<Slider>();
        Ds_soundslider.value = Ds_volumeRange;
        Debug.Log(Ds_soundslider);
    }
    
}
