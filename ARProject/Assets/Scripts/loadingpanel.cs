using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingpanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroyobject",3f);
    }

    void Destroyobject()
    {
        Destroy(gameObject);
    }
}
