using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cpDBState
{
    None, Nomal, Deckmaking, Multiselect, Deactivecard
}
public class CPGameDataBase : MonoBehaviour
{
    public static CPGameDataBase inst;
    public GUITestScrollView testScrollView;
    public UILabel CostLabel;
    public List<Item> cpcards_Deck = new List<Item>();
    public List<Item> cpcards_Inventory = new List<Item>();
    public List<int> DestoryItem = new List<int>();
    public List<Item> cpcards = new List<Item>();
    int m_deckcost;
    public cpDBState m_currentcpDBstate;
    public cpDBState currentcpDBstate
    {
        get { return m_currentcpDBstate; }
        set
        {
            switch (value)
            {
                case cpDBState.Deactivecard:
                    {
                        DestoryItem.Clear();
                        if (m_currentcpDBstate != value)
                        {
                            m_currentcpDBstate = value;
                            testScrollView.EV_UpdateAll();
                        }
                    }
                    break;
                case cpDBState.Deckmaking:
                    {
                        DestoryItem.Clear();
                        if (m_currentcpDBstate != value)
                        {
                            m_currentcpDBstate = value;
                            testScrollView.EV_UpdateAll();
                        }
                    }
                    break;
                case cpDBState.Multiselect:
                    {
                        DestoryItem.Clear();
                        if (m_currentcpDBstate != value)
                        {
                            m_currentcpDBstate = value;
                            testScrollView.EV_UpdateAll();
                        }
                    }
                    break;
                case cpDBState.Nomal:
                    {
                        DestoryItem.Clear();
                        if (m_currentcpDBstate != value)
                        {
                            m_currentcpDBstate = value;

                        }
                    }
                    break;
                case cpDBState.None:
                    {
                        DestoryItem.Clear();
                        if (m_currentcpDBstate != value)
                        {
                            m_currentcpDBstate = value;

                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
    //public List<Item>

    public int deckcost
    {
        get
        {
            return m_deckcost;
        }
        set
        {
            m_deckcost = value;
            CostLabel.text = "Cost " + deckcost + "/30";
        }
    }
    public Element element;
    // Start is called before the first frame update

    void Awake()
    {
        inst = this;
        currentcpDBstate = cpDBState.Nomal;
        CpReSet();
    }
    public void CpReSet()
    {
        inst.cpcards_Deck.Clear();
        inst.cpcards_Inventory.Clear();
        foreach (Item deck in GameDataBase.Inst.cards_Deck)
        {
            inst.cpcards_Deck.Add(deck);
        }
        foreach (Item inven in GameDataBase.Inst.cards_Inventory)
        {
            inst.cpcards_Inventory.Add(inven);
        }
        cpcards = GameDataBase.Inst.cards;
        deckcost = GameDataBase.Inst.deckcost;
    }
}
