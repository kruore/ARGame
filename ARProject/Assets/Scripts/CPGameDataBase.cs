using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPGameDataBase : MonoBehaviour
{
    public GUITestScrollView testScrollView;
    public List<Item> cpcards_Deck = new List<Item>();
    public List<Item> cpcards_Inventory = new List<Item>();
    public List<Item> cards_category = new List<Item>();
    public int deckcost;
    public Element element;
    // Start is called before the first frame update
    
    void Awake()
    {
        foreach(Item deck in GameDataBase.Inst.cards_Deck)
        {
            gameObject.GetComponent<CPGameDataBase>().cpcards_Deck.Add(deck);
        }
        foreach(Item inven in GameDataBase.Inst.cards_Inventory)
        {
            gameObject.GetComponent<CPGameDataBase>().cpcards_Inventory.Add(inven);
        }
    }
    
}
