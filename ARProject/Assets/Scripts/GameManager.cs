using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GpsData currentplace;
    public int objectindex,objectrank=1;
    public string loadingnextscene,AsyncLoadSceneName;
    public Stack<string> Scenestack = new Stack<string>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Buttonmanager.Inst.TouchBackButton();
            }
        }
    }
}
