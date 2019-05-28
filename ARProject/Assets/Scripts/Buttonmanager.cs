using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttonmanager : Singleton<Buttonmanager>
{
    public void aabuttonclick()
    {
        SceneManager.LoadScene("mapscene");
    }
    public void bbbutonclick()
    {
        SceneManager.LoadScene("Inventory");
    }
    public void Mainscene()
    {
        SceneManager.LoadScene("Main");
    }
    public void SaveDB()
    {
        if (CPGameDataBase.inst.cpcards_Deck.Count == 10)
        {
            GameDataBase.Inst.cards_Deck.Clear();
            foreach (Item item in CPGameDataBase.inst.cpcards_Deck)//덱을 비우고 카피데이터베이스에서 덱을 받아온다.
            {
                GameDataBase.Inst.cards_Deck.Add(item);
            }
            GameDataBase.Inst.cards_Inventory.Clear();
            foreach (Item item in CPGameDataBase.inst.cpcards_Inventory)//인벤토리를 비우고 카피데이터베이스에서 인벤토리를 받아온다.
            {
                GameDataBase.Inst.cards_Inventory.Add(item);
            }
            GameDataBase.Inst.cards_Deck.Sort(delegate (Item a, Item b)//덱 정렬 cost 낮은순
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
            GameDataBase.Inst.cards_Inventory.Sort(delegate (Item a, Item b)//인벤토리 정렬 cost 낮은순
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
            for (int i = 0; i < GameDataBase.Inst.cards_Inventory.Count; i++)//인벤토리 넘버 수정
            {
                GameDataBase.Inst.cards_Inventory[i].cardInventoryNum = i;
            }
            for (int i = 0; i < GameDataBase.Inst.cards_Deck.Count; i++)//덱 넘버 수정
            {
                GameDataBase.Inst.cards_Deck[i].cardInventoryNum = i;
            }
            CPGameDataBase.inst.testScrollView.EV_UpdateAll();
            GameDataBase.Inst.DS_InventoryReSetting();
            GameDataBase.Inst.DS_DeckReSetting();
            UIButton.current.gameObject.GetComponent<SetActiveObject>().ObjectControl();
        }
        else
        {
            GameObject.Find("UI Root/ErrorPanel/Sprite/Label").GetComponent<UILabel>().text= "덱이 10장 미만이기 때문에 사용할 수 없습니다. ";
            GameObject.Find("UI Root/ErrorPanel").GetComponent<UIPanel>().enabled = true;
            GameObject.Find("UI Root/ErrorPanel").GetComponent<BoxCollider>().enabled = true;
        }
        CPGameDataBase.inst.currentcpDBstate = cpDBState.Nomal;
    }
    public void Elementlist()
    {
        
        CPGameDataBase.inst.testScrollView.EV_UpdateAll();
    }
    public void paneldisable()
    {
        UIButton.current.gameObject.GetComponent<UIPanel>().enabled = false;
        UIButton.current.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
