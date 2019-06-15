using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GetComponent<UILabel>().text = GPSTest.c;
    }
}
