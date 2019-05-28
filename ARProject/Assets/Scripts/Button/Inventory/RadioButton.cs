using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButton : MonoBehaviour
{
    public static List<EventDelegate> RadioButtonImage = new List<EventDelegate>();
    private void Start()
    {
        GetComponent<UIButton>().tweenTarget = null;
        EventDelegate.Add(RadioButtonImage, ButtonBGIChange);
    }
    public void Elementlist()
    {
        CPGameDataBase.inst.testScrollView.EV_UpdateAll();
        EventDelegate.Execute(RadioButtonImage);
    }
    public void ButtonBGIChange()
    {
        if (gameObject.CompareTag(CPGameDataBase.inst.element.ToString()))
        {
            GetComponentInChildren<UISprite>().spriteName = "List_On";

        }
        else
        {
            GetComponentInChildren<UISprite>().spriteName = "List_Off";
        }
        UIButton.current.gameObject.GetComponentInChildren<UISprite>().spriteName = "List_On";
    }

}
