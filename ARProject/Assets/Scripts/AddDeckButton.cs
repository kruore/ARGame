using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDeckButton : MonoBehaviour
{
    public ItemCellData item;
    public GUITestScrollView aa;
    public GameObject copydb;
    private void Start()
    {
        copydb = GameObject.Find("DBcopy");
    }
    //인벤토리에서 덱에 카드를 집어넣으며 코스트, 장수가 합당한지 검출
    public void Deckinsert()
    {
        Item newcard = new Item(item.num, item.cost,  item.Damage, item.ImgName, item.element,item.rank);
        if (copydb.GetComponent<CPGameDataBase>().cpcards_Deck.Count < 10 && (copydb.GetComponent<CPGameDataBase>().deckcost + newcard.cardCost) < 30)
        {
            copydb.GetComponent<CPGameDataBase>().cpcards_Deck.Add(newcard);
            copydb.GetComponent<CPGameDataBase>().deckcost += newcard.cardCost;
            int a = -1;
            for (int i=0;i< copydb.GetComponent<CPGameDataBase>().cpcards_Inventory.Count;i++)
            {
                if(copydb.GetComponent<CPGameDataBase>().cpcards_Inventory[i].cardName==item.ImgName)
                {
                    a = i;
                    break;
                }
            }
            Debug.Log(a);
            copydb.GetComponent<CPGameDataBase>().cpcards_Inventory.Remove(copydb.GetComponent<CPGameDataBase>().cpcards_Inventory[a]);
            aa = transform.parent.transform.parent.GetComponent<GUITestScrollView>();
            
            EventDelegate.Execute(DeleteDeck.eventdel);
            Debug.Log(gameObject.name);
        }
        else
        {
            //실패했을시에 할행동;
        }
    }
}
