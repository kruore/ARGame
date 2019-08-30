using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectrotate : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.Rotate(new Vector3(0,Random.Range(0,359),0));
    }
}
