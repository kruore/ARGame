using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttonmanager : Singleton<Buttonmanager>
{
    #region public
    public void ObjectlistControl()
    {
        if (UIButton.current.gameObject.GetComponent<SetActiveObject>() != null)
        {
            SetActiveObject objectlistscirpt = UIButton.current.gameObject.GetComponent<SetActiveObject>();
            foreach (GameObject obj in objectlistscirpt.activeObject)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in objectlistscirpt.deactiveObject)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            Debug.Log("SetActiveObject가 없어요.");
        }
    }
    public void TouchBackButton()
    {
        if (GameManager.Inst.Scenestack.Count > 0)
        {
            switch (GameManager.Inst.Scenestack.Pop())
            {
                case "Main":
                    StopAllCoroutines();
                    GameManager.Inst.Scenestack.Push("Main");
                    break;
                case "mapscene":
                    StopAllCoroutines();
                    SceneManager.LoadScene("Main");
                    break;
                case "InventoryLobby":
                    StopAllCoroutines();
                    SceneManager.LoadScene("Main");
                    break;
                case "Inventorypanel":
                    DeactiveInventorypanel();
                    break;
                case "CardList":
                    DeactiveCardListpanel();
                    break;
                case "ARscene":
                    StopAllCoroutines();
                    SceneManager.LoadScene("mapscene");
                    break;
                case "Loading":
                    SceneManager.LoadScene("mapscene");
                    break;
                case "Battle2":
                    SceneManager.LoadScene("Main");
                    break;
            }
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    #endregion
    #region scenechage
    public void gotoMapscene()
    {
        GameManager.Inst.Scenestack.Push("mapscene");
        StopAllCoroutines();
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("Fadepanel");
        Invoke("mapsceneload", 1f);
    }
    void mapsceneload()
    {
        SceneManager.LoadScene("mapscene");
    }
    public void gotoInventory()
    {
        GameManager.Inst.Scenestack.Push("InventoryLobby");
        StopAllCoroutines();
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("Fadepanel");
        Invoke("Inventorysceneload", 1f);
    }
    void Inventorysceneload()
    {
        SceneManager.LoadScene("Inventory");
    }
    public void gotobattle()
    {
        GameManager.Inst.Scenestack.Push("Battle2");
        StopAllCoroutines();
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("Fadepanel");
        Invoke("Battle2ceneload", 1f);

    }
    void Battle2ceneload()
    {
        SceneManager.LoadScene("Battle2");
    }
    public void gotoMainscene()
    {
        StopAllCoroutines();
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("Fadepanel");
        Invoke("Mainsceneload", 1f);
    }
    void Mainsceneload()
    {
        SceneManager.LoadScene("Main");
    }
    #endregion
    #region inventoryscene
    #region publicinventory
    public void ActiveInventorypanel()
    {
        GameManager.Inst.Scenestack.Push("Inventorypanel");
        InventorysceneManager.inst.Scenename.GetComponent<UILabel>().text = "덱 편성";
        InventorysceneManager.inst.Inventory.SetActive(true);
        InventorysceneManager.inst.CpReSet();
        InventorysceneManager.inst.testScrollView = InventorysceneManager.inst.InventoryView;
        InventorysceneManager.inst.currentcpDBstate = cpDBState.Deckmaking;
        EventDelegate.Execute(DeleteDeck.eventdel);
    }
    public void ActivecCardListpanel()
    {
        GameManager.Inst.Scenestack.Push("CardList");
        InventorysceneManager.inst.Scenename.GetComponent<UILabel>().text = "카드 일람";
        InventorysceneManager.inst.testScrollView = InventorysceneManager.inst.CardListView;
        InventorysceneManager.inst.CardList.SetActive(true);
    }


    public void Elementlist()
    {
        InventorysceneManager.inst.testScrollView.EV_UpdateAll();
    }
    #endregion
    #region Lobby
    #endregion
    #region inventory
    public void DeactiveInventorypanel()
    {
        InventorysceneManager.inst.Scenename.GetComponent<UILabel>().text = "카드 관리";
        InventorysceneManager.inst.Inventory.SetActive(false);
    }
    #endregion
    #region cardlist
    public void DeactiveCardListpanel()
    {
        InventorysceneManager.inst.Scenename.GetComponent<UILabel>().text = "카드 관리";
        InventorysceneManager.inst.CardList.SetActive(false);
    }
    public void canclecontract_back()
    {
        InventorysceneManager.inst.canclecontract.SetActive(false);
    }
    public void CPcardMultSelect()
    {
        if (InventorysceneManager.inst.currentcpDBstate != cpDBState.Multiselect)
        {
            InventorysceneManager.inst.currentcpDBstate = cpDBState.Multiselect;
            InventorysceneManager.inst.testScrollView.EV_UpdateAll();
            InventorysceneManager.inst.carddismantiingbutton.GetComponent<UIButton>().isEnabled = true;
            UIButton.current.gameObject.GetComponentInChildren<UILabel>().text = "취소";
        }
        else
        {
            InventorysceneManager.inst.currentcpDBstate = cpDBState.Deckmaking;
            InventorysceneManager.inst.testScrollView.EV_UpdateAll();
            InventorysceneManager.inst.carddismantiingbutton.GetComponent<UIButton>().isEnabled = false;
            UIButton.current.gameObject.GetComponentInChildren<UILabel>().text = "다중 선택";
        }
    }
    public void AllcardInvisible()
    {
        for (int i = InventorysceneManager.inst.DestoryItem.Count - 1; i >= 0; i--)
        {
            InventorysceneManager.inst.cpcards_Inventory.RemoveAt(InventorysceneManager.inst.DestoryItem[i]);
        }
        foreach (Cardcheck check in InventorysceneManager.inst.testScrollView.gameObject.transform.Find("Grid").GetComponentsInChildren<Cardcheck>())
        {
            check.objectcheck = false;
            check.CardCheck();
        }
        SaveDB();
    }
    public void CardDismantiling()
    {
        for (int i = InventorysceneManager.inst.DestoryItem.Count - 1; i >= 0; i--)
        {
            InventorysceneManager.inst.cpcards_Inventory.RemoveAt(InventorysceneManager.inst.DestoryItem[i]);
        }
        foreach (Cardcheck check in InventorysceneManager.inst.testScrollView.gameObject.transform.Find("Grid").GetComponentsInChildren<Cardcheck>())
        {
            check.objectcheck = false;
            check.CardCheck();
        }
        SaveDB();
    }
    #endregion
    #endregion
    public void paneldisable()
    {
        UIButton.current.gameObject.GetComponent<UIPanel>().enabled = false;
        UIButton.current.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    public void SaveDB()
    {
        if (InventorysceneManager.inst.cpcards_Deck.Count.Equals(10))
        {
            GameDataBase.Inst.cards_Deck.Clear();
            foreach (Item item in InventorysceneManager.inst.cpcards_Deck)//덱을 비우고 카피데이터베이스에서 덱을 받아온다.
            {
                GameDataBase.Inst.cards_Deck.Add(item);
            }
            GameDataBase.Inst.cards_Inventory.Clear();
            foreach (Item item in InventorysceneManager.inst.cpcards_Inventory)//인벤토리를 비우고 카피데이터베이스에서 인벤토리를 받아온다.
            {
                GameDataBase.Inst.cards_Inventory.Add(item);
            }
            GameDataBase.Inst.cards_Deck.Sort(delegate (Item a, Item b)//덱 정렬 cost 낮은순
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
            GameDataBase.Inst.cards_Inventory.Sort(delegate (Item a, Item b)//인벤토리 정렬 cost 낮은순
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
            for (int i = 0; i < GameDataBase.Inst.cards_Inventory.Count; i++)//인벤토리 넘버 수정
            {
                GameDataBase.Inst.cards_Inventory[i].cardInventoryNum = i;
            }
            for (int i = 0; i < GameDataBase.Inst.cards_Deck.Count; i++)//덱 넘버 수정
            {
                GameDataBase.Inst.cards_Deck[i].cardInventoryNum = i;
            }
            InventorysceneManager.inst.testScrollView.EV_UpdateAll();
            GameDataBase.Inst.DS_InventoryReSetting();
            GameDataBase.Inst.DS_DeckReSetting();
            // EventDelegate.Execute(UIButton.current.onClick);
        }
        else
        {
            Debug.Log(GameObject.Find("UI Root/ErrorPanel").name.ToString());
            GameObject.Find("UI Root/ErrorPanel/Sprite/Label").GetComponent<UILabel>().text = "덱이 10장 미만이기 때문에 사용할 수 없습니다. ";
            GameObject.Find("UI Root/ErrorPanel").GetComponent<UIPanel>().enabled = true;
            GameObject.Find("UI Root/ErrorPanel").GetComponent<BoxCollider>().enabled = true;
        }
        InventorysceneManager.inst.currentcpDBstate = cpDBState.Deckmaking;
    }
    public void SceneStackPop()
    {
        if (GameManager.Inst.Scenestack.Count > 0)
        {
            GameManager.Inst.Scenestack.Pop();
        }
    }
}
