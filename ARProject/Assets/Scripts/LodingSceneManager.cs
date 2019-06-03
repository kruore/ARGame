using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LodingSceneManager : MonoBehaviour
{
    public static string Dsv_nextScene;
    float Dsv_timer = 0.0f;
    AsyncOperation Dsv_asyncoperation;
    string Dsv_nextSceneName;
    private void Start()
    {
        //Dsv_scenemanager = GameObject.Find("SceneControlManager").GetComponent<SceneControlManager>();
        //Dsv_asyncoperation = SceneManager.LoadSceneAsync(Dsv_scenemanager.Dsv_nextscene);
        //Dsv_asyncoperation.allowSceneActivation = false;
    }
    private void Update()
    {

        if (!Dsv_asyncoperation.isDone)
        {
            if (Dsv_asyncoperation.progress >= 0.9f)
            {
                Dsv_timer += Time.deltaTime;
                if (Dsv_timer >= 1)
                    Dsv_asyncoperation.allowSceneActivation = true;
            }
        }
    }
    
    public static void Dsf_LoadScene(string sceneName)
    {
        Dsv_nextScene = sceneName;
    }
}
