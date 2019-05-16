using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        GetComponent<UILabel>().text = GPSTest.b;
    }
}
