using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttonmanager : MonoBehaviour
{
    public void aabuttonclick()
    {
        SceneManager.LoadScene("mapscene");
    }
    public void bbbutonclick()
    {
        SceneManager.LoadScene("Inventory");
    }
    public void SaveDB()
    {
        CPGameDataBase copydb = GameObject.Find("DBcopy").GetComponent<CPGameDataBase>();
        GameDataBase.Inst.cards_Deck.Clear();
        foreach (Item item in copydb.cpcards_Deck)
        {
            GameDataBase.Inst.cards_Deck.Add(item);
        }
        GameDataBase.Inst.cards_Inventory.Clear();
        foreach (Item item in copydb.cpcards_Inventory)
        {
            GameDataBase.Inst.cards_Inventory.Add(item);
        }
        GameDataBase.Inst.cards_Deck.Sort(delegate (Item a, Item b)
        {
            if (a.cardCost > b.cardCost)
            {
                return 1;
            }
            else if (a.cardCost == b.cardCost)
            {
                return a.cardName.CompareTo(b.cardName);
            }
            else
            {
                return -1;
            }

        });
        GameDataBase.Inst.cards_Inventory.Sort(delegate (Item a, Item b)
        {
            if (a.cardCost > b.cardCost)
            {
                return 1;
            }
            else if (a.cardCost == b.cardCost)
            {
                return a.cardName.CompareTo(b.cardName);
            }
            else
            {
                return -1;
            }

        });
        for (int i = 0; i < GameDataBase.Inst.cards_Inventory.Count; i++)
        {
            GameDataBase.Inst.cards_Inventory[i].cardInventoryNum = i;
        }
        for (int i = 0; i < GameDataBase.Inst.cards_Deck.Count; i++)
        {
            GameDataBase.Inst.cards_Deck[i].cardInventoryNum = i;
        }
        copydb.testScrollView.EV_UpdateAll();
        GameDataBase.Inst.DS_InventoryReSetting();
        GameDataBase.Inst.DS_DeckReSetting();

    }
    public void Elementlist()
    {
        CPGameDataBase copydb = GameObject.Find("DBcopy").GetComponent<CPGameDataBase>();
        copydb.cards_category.Clear();
        if (UIButton.current.gameObject.CompareTag("Wood"))
        {
            copydb.element = Element.Wood;
        }
        else if (UIButton.current.gameObject.CompareTag("Stone"))
        {
            copydb.element = Element.Stone;
        }
        else if (UIButton.current.gameObject.CompareTag("Grass"))
        {
            copydb.element = Element.Grass;
        }
        else if (UIButton.current.gameObject.CompareTag("Chaos"))
        {
            copydb.element = Element.Chaos;
        }
        else
        {
            copydb.testScrollView.items = copydb.cpcards_Inventory;
        }
        foreach (Item card in copydb.cpcards_Inventory)
        {
            if (card.cardElement== copydb.element)
            {
                copydb.cards_category.Add(card);
            }
        }
        copydb.testScrollView.items = copydb.cards_category;
        copydb.testScrollView.EV_UpdateAll();
        
    }
}
