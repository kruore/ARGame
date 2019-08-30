using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameralook : MonoBehaviour
{
    Transform player;
    float localy;
    public float Localpositiony
    {
        get { return localy; }
        set
        {
            if(value>20)
            {
                localy = 20;
            }
            else if(value<5)
            {
                localy = 5;
                
            }
            else
            {
                localy = value;
            }
            gameObject.transform.localPosition = new Vector3(0, localy, -7);
            gameObject.transform.LookAt(new Vector3(player.position.x, player.position.y + 5, player.position.z), Vector3.up);
        }
    }
    private void Start()
    {
        player = GameObject.Find("player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.LookAt(new Vector3(player.position.x, player.position.y+5, player.position.z),Vector3.up);
    }
}
