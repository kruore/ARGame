using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattleManager : NetworkBehaviour
{
    public enum Turn {StartGame,Drow,Battle,End};
    public Turn turn = Turn.StartGame;
    // Start is called before the first frame update
    [Command]
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

    [Command]
    public void CmdTurnEnd()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        turn++;
        if(turn == Turn.End)
        {
            turn = Turn.StartGame;
        }
        CmdDrowCard();
    }
    public void Surrender()
    {
        Debug.Log("항복합니다.");
    }
}
