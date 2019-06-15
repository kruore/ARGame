using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreateCharactor : MonoBehaviour
{
    
    List<GameObject> otherobject=new List<GameObject>();
    Item currentcard;
    private void OnMouseUp()
    {
        
        
        foreach(Item card in GameDataBase.Inst.cards)
        {
            if(card.cardName.Equals(gameObject.name))
            {
                currentcard = card;
                break;
            }
        }
        PlayerCreateManager.Inst.playercontect = currentcard;
        Debug.Log(currentcard.cardName);
        PlayerCreateManager.Inst.CardInfo.SetActive(true);
        PlayerCreateManager.Inst.CardInfo.transform.Find("CardImg").GetComponent<UISprite>().spriteName=currentcard.cardName;
        PlayerCreateManager.Inst.CardInfo.transform.Find("Info").GetComponent<UILabel>().text = GameDataBase.Inst.CardInfostring(currentcard.cardInventoryNum);
        PlayerCreateManager.Inst.CardInfo.transform.Find("Name").GetComponent<UILabel>().text = "이 름 :"+currentcard.cardName;
        switch(currentcard.cardRank)
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
        PlayerCreateManager.Inst.CurrentCharactor = gameObject;
    }
    public void playAnimationSuccess()
    {
        Animator[] childAnimator;
        childAnimator =GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("emotion_success");
        }
    }
    public void playAnimationIdle()
    {
        Animator[] childAnimator;
        childAnimator = GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("standA@loop");
        }
    }
}
