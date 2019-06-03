using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDeckButton : MonoBehaviour
{
    public ItemCellData item;
    public GUITestScrollView aa;
    //인벤토리에서 덱에 카드를 집어넣으며 코스트, 장수가 합당한지 검출
    public void Deckinsert()
    {
        Item newcard = new Item(item.num, item.cost,  item.damage, item.ImgName, item.element,item.rank);
        if (CPGameDataBase.inst.cpcards_Deck.Count < 10 && (CPGameDataBase.inst.deckcost + newcard.cardCost) < 31)
        {
            CPGameDataBase.inst.cpcards_Deck.Add(newcard);
            CPGameDataBase.inst.deckcost += newcard.cardCost;
            int currentnumber = -1;
            for (int i=0;i< CPGameDataBase.inst.cpcards_Inventory.Count;i++)
            {
                if(CPGameDataBase.inst.cpcards_Inventory[i].cardName==item.ImgName)
                {
                    currentnumber = i;
                    break;
                }
            }
            CPGameDataBase.inst.cpcards_Deck.Sort(delegate (Item a, Item b)//인벤토리 정렬 cost 낮은순
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
                Debug.Log(currentnumber);
            CPGameDataBase.inst.cpcards_Inventory.Remove(CPGameDataBase.inst.cpcards_Inventory[currentnumber]);
            aa = transform.parent.transform.parent.GetComponent<GUITestScrollView>();
            aa.EV_UpdateAll();
            EventDelegate.Execute(DeleteDeck.eventdel);
            Debug.Log(gameObject.name);
        }
        else
        {
            //실패했을시에 할행동;
        }
    }
}
