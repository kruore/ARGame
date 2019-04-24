using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMangager : MonoBehaviour
{
    public GameObject inputBox;
    UIInput UIInput;
    string text;

  
    public void GetMessage()
    {
       
        UIInput = inputBox.GetComponent<UIInput>();
        text = UIInput.label.text;
        print(text);

        PlayerPrefs.SetString("name", text);
    }
    public void SetMessage()
    {

        PlayerPrefs.GetString("name",text);
        PlayerPrefs.Save();
    }


}
