using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtext : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<UILabel>().text = GPS.Instance.test;
    }
}
