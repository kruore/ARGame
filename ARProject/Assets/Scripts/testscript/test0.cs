﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test0 : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        GetComponent<UILabel>().text=GameDataBase.a;
    }
}
