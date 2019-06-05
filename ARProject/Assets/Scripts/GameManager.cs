using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GpsData currentplace;
    public int objectindex;
    public string loadingnextscene;
    public Stack<string> Scenestack= new Stack<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.platform.Equals(RuntimePlatform.Android))
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                switch(Scenestack.Pop())
                {
                    case "Main":
                        break;
                }
            }
        }
    }
}
