using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPGameDataBase : MonoBehaviour
{

    public List<Item> cpcards_Deck = new List<Item>();
    public List<Item> cpcards_Inventory = new List<Item>();
    public int deckcost;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Item deck in GameDataBase.Inst.cards_Deck)
        {
            cpcards_Deck.Add(deck);
        }
        foreach(Item inven in GameDataBase.Inst.cards_Inventory)
        {
            cpcards_Inventory.Add(inven);
        }
        
    }
    //인벤토리에서 덱에 카드를 집어넣으며 코스트, 장수가 합당한지 검출
    void Deckinsert(Item newcard)
    {
        if(cpcards_Deck.Count<10&&(deckcost+newcard.cardCost)<30)
        {
            cpcards_Deck.Add(newcard);
        }
        else
        {
            //실패했을시에 할행동;
        }
    }
}
