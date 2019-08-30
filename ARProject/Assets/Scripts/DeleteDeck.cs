using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDeck : MonoBehaviour
{
    public static List<EventDelegate> eventdel = new List<EventDelegate>();
    public GUITestScrollView scrollview;
    // Start is called before the first frame update

    public void Deckremove()
    {
        if (InventorysceneManager.inst.currentcpDBstate.Equals(cpDBState.Deckmaking))
        {
            int decknum = GetComponent<DeckImage>().slotnumber;
            if (decknum < InventorysceneManager.inst.cpcards_Deck.Count)
            {
                InventorysceneManager.inst.cpcards_Inventory.Add(InventorysceneManager.inst.cpcards_Deck[decknum]);
                InventorysceneManager.inst.cpcards_Inventory.Sort(delegate (Item a, Item b)//인벤토리 정렬 cost 낮은순
                {
                    if (a.cardCost > b.cardCost)
                    {
                        return 1;
                    }
                    else if (a.cardCost.Equals(b.cardCost))
                    {
                        return a.cardName.CompareTo(b.cardName);
                    }
                    else
                    {
                        return -1;
                    }

                });
                for (int i = 0; i < InventorysceneManager.inst.cpcards_Inventory.Count; i++)//인벤토리 넘버 수정
                {
                    InventorysceneManager.inst.cpcards_Inventory[i].cardInventoryNum = i;
                }
                InventorysceneManager.inst.deckcost -= InventorysceneManager.inst.cpcards_Deck[decknum].cardCost;
                InventorysceneManager.inst.cpcards_Deck.RemoveAt(decknum);

                EventDelegate.Execute(eventdel);
                scrollview.EV_UpdateAll();
            }
        }
    }
    private void OnDestroy()
    {
        eventdel.Clear();
    }
}
