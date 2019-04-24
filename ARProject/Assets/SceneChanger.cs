﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    string scene_name;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void LoadScene()
    {
        SceneManager.LoadScene(scene_name);
    }
    void LateLoadScene()
    {
        Invoke("LoadScene", 0.5f);
    }
    public void Goto02()  //복사 씹가능
    {
        scene_name = "02";
        LateLoadScene();
    }

    public void Goto03()
    {
        scene_name = "03";
        LateLoadScene();
    }
    public void GotoAdventure()
    {
        scene_name = "Adventure";
        LateLoadScene();
    }
    public void GotoBattle()
    {
        scene_name = "Battle";
        LateLoadScene();
    }
    public void GotoCard()
    {
        scene_name = "Card";
        LateLoadScene();
    }
    public void GotoBag()
    {
        scene_name = "Bag";
        LateLoadScene();
    }
    public void GotoShop()
    {
        scene_name = "Shop";
        LateLoadScene();
    }
}
