using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckImage : MonoBehaviour
{
    public int slotnumber;
    public UISprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("deckimage");
        EventDelegate.Add(DeleteDeck.eventdel, DeckReImage);//이벤트 델리게이트에 덱이미지를 재조정하는 함수추가
        if (InventorysceneManager.inst.cpcards_Deck.Count>slotnumber)
        {
            sprite = GetComponent<UISprite>();
             sprite.spriteName= InventorysceneManager.inst.cpcards_Deck[slotnumber].cardName;
            GetComponent<UIButton>().normalSprite = InventorysceneManager.inst.cpcards_Deck[slotnumber].cardName;
            
        }
        else
        {
            sprite = GetComponent<UISprite>();
            sprite.spriteName = "background";
            GetComponent<UIButton>().normalSprite = "background";
        }
    }
    public void DeckReImage()
    {
        if (InventorysceneManager.inst.cpcards_Deck.Count > slotnumber)
        {
            Debug.Log(gameObject.name);
            sprite = GetComponent<UISprite>();
            sprite.spriteName = InventorysceneManager.inst.cpcards_Deck[slotnumber].cardName;
            GetComponent<UIButton>().normalSprite = InventorysceneManager.inst.cpcards_Deck[slotnumber].cardName;
        }
        else
        {
            sprite = GetComponent<UISprite>();
            sprite.spriteName = "background";
            GetComponent<UIButton>().normalSprite = "background";
        }
    }
}
