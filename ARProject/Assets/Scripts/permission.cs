using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
public class permission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GPS.Instance.count++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
