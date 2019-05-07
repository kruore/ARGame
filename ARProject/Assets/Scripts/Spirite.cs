using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirite : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    Debug.Log("Click");
                }
            }
        }
    }
}
