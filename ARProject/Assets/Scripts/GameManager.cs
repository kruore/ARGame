using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager gameManager;

    public Element playableCharacterelement = Element.None;
    public GameObject FadePanel;
    public GameObject FinaldecisionPanel;
    int num;

    public void Awake()
    {
        FadePanel = GameObject.Find("UI Root/Panel01");
        FinaldecisionPanel = GameObject.Find("UI Root/Panel02");
        FadePanel.SetActive(false);
    }

    public void SelectCharacter()
    {

     switch(playableCharacterelement)
        {
            case Element.None:
                Debug.Log("아무것도 결정되지 않았습니다.");
                break;
            case Element.Stone:
                Debug.Log("돌을 골랐습니다.");
                break;
            case Element.Grass:
                Debug.Log("풀을 골랐습니다.");
                
                break;
            case Element.Wood:
                Debug.Log("나무을 골랐습니다.");
                break;
            default:
                return;
        }
    }
   public void SelectedStone()
    {
        playableCharacterelement = Element.Stone;
        num = 111;
        SelectCharacter();
    }
    public void SelectedGrass()
    {
        
        playableCharacterelement = Element.Grass;
        num = 109;
        SelectCharacter();
    }
    public void SelectedTree()
    {
        playableCharacterelement = Element.Wood;
        num = 110;

        SelectCharacter();
    }
    public void SelectedFinal()
    {
        if (playableCharacterelement == 0)
        {
            SelectCharacter();
        }
        if (playableCharacterelement!=0)
        {
            Debug.Log("해당 정령을 추가합니다 : " + playableCharacterelement);
            GameDataBase.Instance.InventoryInsert(num);
            FadePanel.SetActive(true);
            FinaldecisionPanel.SetActive(false);
            //FadePanel.GetComponent<TweenAlpha>().enabled = true;
            //TODO: 해당 기능 추가할 것(DataBase)
        }
    }
}
