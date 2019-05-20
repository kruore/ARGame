using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattleManager : MonoBehaviour
{
    //[SyncVar]
    static int PlayerHP= 100;
    public enum Turn { StartGame, Drow, Battle, End };
    public Turn turn = Turn.StartGame;

    public void Awake()
    {
    }
    // Start is called before the first frame update
    public void CmdDrowCard()
    {
        switch (turn)
        {
            case Turn.StartGame:
                Debug.Log("Start Battle");
                break;
            case Turn.Drow:
                Debug.Log("Drow Card");
                break;
            case Turn.Battle:
                Debug.Log("Battle");
                break;
            case Turn.End:
                Debug.Log("End");
                break;
        }
    }

    public void CmdTurnEnd()
    {
        turn++;
        if (turn == Turn.End)
        {
            turn = Turn.StartGame;
        }
        CmdDrowCard();
    }
    public void Surrender()
    {
        Debug.Log("항복합니다.");
    }

    public void TakeDamage(int amount)
    {
        //if(!isServer)
        //{
        //    return;
        //}
        //PlayerHP -= amount;
    }
}
