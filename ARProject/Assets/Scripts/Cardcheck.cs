using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardcheck : MonoBehaviour
{
    UISprite gameobjectsprite;

    public bool objectcheck = true;

    private void Awake()
    {
        gameobjectsprite = gameObject.GetComponentInChildren<UISprite>();
    }
    public void CardCheck()
    {
        switch (InventorysceneManager.inst.currentcpDBstate)
        {
            case cpDBState.Nomal:
                GameObject cardinfo = GameObject.Find("UI Root/CardListPanel/Cardinfo");
                cardinfo.SetActive(true);
                cardinfo.GetComponentInChildren<UISprite>();
                cardinfo.GetComponentInChildren<UISprite>().spriteName = gameobjectsprite.spriteName;
                Debug.Log(cardinfo.GetComponentInChildren<UISprite>().spriteName);
                break;
            case cpDBState.Multiselect:
                if (InventorysceneManager.inst.DestoryItem.Count < 8)
                {
                    objectcheck = !objectcheck;
                    gameObject.GetComponent<TestCell>().citem.check=objectcheck;
                    if (objectcheck)
                    {
                        gameobjectsprite.alpha = 1f;
                        InventorysceneManager.inst.DestoryItem.Remove(gameObject.GetComponent<TestCell>().citem.num);
                    }
                    else
                    {
                        gameobjectsprite.alpha = 0.5f;
                        InventorysceneManager.inst.DestoryItem.Add(gameObject.GetComponent<TestCell>().citem.num);
                    }
                }
                break;
            case cpDBState.Deckmaking:
                {
                    cardinfo = GameObject.Find("UI Root/CardListPanel/Cardinfo");
                    cardinfo.SetActive(true);
                    Debug.Log("AA");
                    cardinfo.GetComponentInChildren<UISprite>().spriteName=gameobjectsprite.spriteName;
                    Debug.Log(cardinfo.GetComponentInChildren<UISprite>().spriteName);
                }
                break;
            case cpDBState.Deactivecard:
                if (objectcheck)
                {
                    gameobjectsprite.alpha = 1f;
                    gameObject.GetComponent<UIButton>().enabled = true;
                }
                else
                {
                    gameobjectsprite.alpha = 0.5f;
                    gameObject.GetComponent<UIButton>().enabled = false;
                }
                break;
            default:
                break;
        }
    }
}

