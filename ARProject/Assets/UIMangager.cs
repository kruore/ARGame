using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMangager : MonoBehaviour
{
    public GameObject inputBox;
    public GameObject cards;
    UIInput UIInput;
    string text;
    Element element;

  
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
    
    public void CreateCard()
    {
        Instantiate(cards);
    }


}
