using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckImage : MonoBehaviour
{
    public int slotnumber;
    public UISprite sprite;
    public CPGameDataBase copydb;
    // Start is called before the first frame update
    void Start()
    {
        EventDelegate.Add(DeleteDeck.eventdel, DeckReImage);
        if (copydb.cpcards_Deck.Count>slotnumber)
        {
            sprite = GetComponent<UISprite>();
             sprite.spriteName= copydb.cpcards_Deck[slotnumber].cardName;
            GetComponent<UIButton>().normalSprite = copydb.cpcards_Deck[slotnumber].cardName;
            
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
        if (copydb.cpcards_Deck.Count > slotnumber)
        {
            
            sprite = GetComponent<UISprite>();
            sprite.spriteName = copydb.cpcards_Deck[slotnumber].cardName;
            GetComponent<UIButton>().normalSprite = copydb.cpcards_Deck[slotnumber].cardName;
        }
        else
        {
       
            sprite = GetComponent<UISprite>();
            sprite.spriteName = "background";
            GetComponent<UIButton>().normalSprite = "background";
        }
    }
}
