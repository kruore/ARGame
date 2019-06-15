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
                cardinfo.GetComponentInChildren<UISprite>().spriteName = gameobjectsprite.spriteName;
                foreach(Item card in GameDataBase.Inst.cards)
                {
                    if (card.cardName.Equals(gameobjectsprite.spriteName))
                    {
                        cardinfo.GetComponentInChildren<UILabel>().text = GameDataBase.Inst.CardInfostring(card.cardInventoryNum);
                        cardinfo.transform.Find("CardImg").GetComponent<UISprite>().spriteName = card.cardName;
                        cardinfo.transform.Find("Info").GetComponent<UILabel>().text = GameDataBase.Inst.CardInfostring(card.cardInventoryNum);
                        cardinfo.transform.Find("Name").GetComponent<UILabel>().text = "이 름 :" + card.cardName;
                        switch (card.cardRank)
                        {
                            case 1:
                                PlayerCreateManager.Inst.CardInfo.transform.Find("Rank").GetComponent<UILabel>().text = "등 급 : 브론즈";
                                break;
                            case 2:
                                PlayerCreateManager.Inst.CardInfo.transform.Find("Rank").GetComponent<UILabel>().text = "등 급 : 실버";
                                break;
                            case 3:
                                PlayerCreateManager.Inst.CardInfo.transform.Find("Rank").GetComponent<UILabel>().text = "등 급 : 골드";
                                break;
                        }
                    }
                }

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

