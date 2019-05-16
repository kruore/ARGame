using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDeck : MonoBehaviour
{
    public static List<EventDelegate> eventdel = new List<EventDelegate>();
    public GUITestScrollView scrollview;
    public CPGameDataBase copydb;
    // Start is called before the first frame update

    public void Deckremove()
    {
        int decknum = GetComponent<DeckImage>().slotnumber;
        if (decknum< copydb.cpcards_Deck.Count)
        {
            copydb.cpcards_Inventory.Add(copydb.cpcards_Deck[decknum]);
            copydb.deckcost -= copydb.cpcards_Deck[decknum].cardCost;
            copydb.cpcards_Deck.RemoveAt(decknum);
            
            EventDelegate.Execute(eventdel);
        }
        scrollview.EV_UpdateAll();
    }
    
}
