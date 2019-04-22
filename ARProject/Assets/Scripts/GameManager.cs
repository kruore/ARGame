using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager gameManager;

    enum PlayableCharacterelement {None, Stone,Grass,Tree};
    PlayableCharacterelement playableCharacterelement = PlayableCharacterelement.None;
    public void SelectCharacter()
    {

     switch(playableCharacterelement)
        {
            case PlayableCharacterelement.None:
                Debug.Log("아무것도 결정되지 않았습니다.");
                break;
            case PlayableCharacterelement.Stone:
                Debug.Log("돌을 골랐습니다.");
                break;
            case PlayableCharacterelement.Grass:
                Debug.Log("풀을 골랐습니다.");
                break;
            case PlayableCharacterelement.Tree:
                Debug.Log("나무을 골랐습니다.");
                break;
            default:
                return;
        }
    }
   public void SelectedStone()
    {
        playableCharacterelement = PlayableCharacterelement.Stone;
        SelectCharacter();
    }
    public void SelectedGrass()
    {
        playableCharacterelement = PlayableCharacterelement.Grass;
        SelectCharacter();
    }
    public void SelectedTree()
    {
        playableCharacterelement = PlayableCharacterelement.Tree;
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
                //TODO: 해당 기능 추가할 것(DataBase)
        }
    }
}
