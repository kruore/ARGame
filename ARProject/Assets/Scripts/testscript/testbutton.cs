using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbutton : MonoBehaviour
{
    public void debugbutton()
    {
        if(PlayerPrefs.HasKey("name"))
        {
            Debug.Log(PlayerPrefs.GetString("name"));
        }
        else
        {
            PlayerPrefs.SetString("name", "rnrytjr");
            PlayerPrefs.Save();
        }
    }
}
