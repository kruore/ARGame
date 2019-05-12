using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckImage : MonoBehaviour
{
    public int slotnumber;
    // Start is called before the first frame update
    void Start()
    {
        if (GameDataBase.Inst.cards_Deck.Count>slotnumber)
        {
            Debug.Log(GameDataBase.Inst.cards_Deck[slotnumber].cardName);
            GetComponent<UITexture>().mainTexture = Resources.Load(GameDataBase.Inst.cards_Deck[slotnumber].cardName) as Texture;
        }
    }
}
