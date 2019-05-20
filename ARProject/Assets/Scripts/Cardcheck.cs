using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardcheck : MonoBehaviour
{
    UISprite gameobjectsprite;
    bool objectcheck = true;
    private void Awake()
    {
        gameobjectsprite = gameObject.GetComponentInChildren<UISprite>();
    }
    public void CardCheck()
    {
        switch (CPGameDataBase.inst.currentcpDBstate)
        {
            case cpDBState.Nomal:
                GameObject cardinfo = GameObject.Find("UI Root/CardListPanel/Cardinfo");
                cardinfo.SetActive(true);
                cardinfo.GetComponentInChildren<UISprite>();
                break;
            case cpDBState.Multiselect:
                objectcheck = !objectcheck;
                if (objectcheck)
                {
                    gameobjectsprite.alpha = 1f;
                }
                else
                {
                    gameobjectsprite.alpha = 0.5f;
                }
                break;
            default:
                break;
        }
    }
}

