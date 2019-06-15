using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cpDBState
{
    None, Nomal, Deckmaking, Multiselect, Deactivecard
}
public class InventorysceneManager : MonoBehaviour
{
    public static InventorysceneManager inst;
    public AudioClip InventoryBGM;
    public GUITestScrollView testScrollView;
    public UILabel CostLabel;
    public List<Item> cpcards_Deck = new List<Item>();
    public List<Item> cpcards_Inventory = new List<Item>();
    public List<int> DestoryItem = new List<int>();
    public List<Item> cpcards = new List<Item>();
    public GameObject InventoryLobby, Inventory, CardList, Cardinfo, canclecontract, carddismantiingbutton,Scenename,Errorpanel;
    public GUITestScrollView InventoryView, CardListView;
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
    private void Start()
    {
        if(SoundManager.Inst.Ds_musicSource.clip!=InventoryBGM)
        {
            SoundManager.Inst.Ds_BgmPlayer(InventoryBGM);
        }
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("FadFanel__");
        Destroy(Fadepanel, 1f);
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

    #region button
    #region buttonpublic
    public void ObjectControl()
    {
        Buttonmanager.Inst.ObjectlistControl();
    }
    #endregion
    public void TouchBackButton()
    {
        Buttonmanager.Inst.TouchBackButton();
    }
        public void gotoMainScene()
    {
        Buttonmanager.Inst.SceneStackPop();
        Buttonmanager.Inst.gotoMainscene();
    }
    public void Activeinventory()
    {
        Buttonmanager.Inst.ActiveInventorypanel();
    }
    public void ActiveCardpanel()
    {
        Buttonmanager.Inst.ActivecCardListpanel();
    }
    public void DeactiveInventorypanel()
    {
        Buttonmanager.Inst.SceneStackPop();
        Buttonmanager.Inst.DeactiveInventorypanel();
    }
    public void DeactiveCardListpanel()
    {
        Buttonmanager.Inst.SceneStackPop();
        Buttonmanager.Inst.DeactiveCardListpanel();
    }
    public void CPcardMultSelect()
    {
        Buttonmanager.Inst.CPcardMultSelect();
    }
    public void AllcardInvisible()
    {
        Buttonmanager.Inst.AllcardInvisible();
    }
    public void Activecanclecontract()
    {
        InventorysceneManager.inst.canclecontract.SetActive(true);
    }
    public void CardDismantiling()
    {
        Buttonmanager.Inst.SceneStackPop();
        Buttonmanager.Inst.CardDismantiling();
        InventorysceneManager.inst.canclecontract.SetActive(false);
    }
    public void CardDismantiingcancle()
    {
        Buttonmanager.Inst.SceneStackPop();
        InventorysceneManager.inst.canclecontract.SetActive(false);
    }
    public void CardSave()
    {
        DeactiveInventorypanel();
        Buttonmanager.Inst.SaveDB();
        currentcpDBstate= cpDBState.Nomal;
    }
    #endregion
}
