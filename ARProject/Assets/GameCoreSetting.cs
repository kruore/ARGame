using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

using ExitGames.Client.Photon;
public class GameCoreSetting : PunBehaviour, IMyTurnManagerCallbacks
{
    public static GameCoreSetting gameCoreSetting;
    public CardInfo cardInfo;
    //it's connected?
    public GameObject ConnectUiView;
    //GameUI / such as Deck, slot
    public GameObject GameUiView;

    //Button Canvas
    public GameObject CardPanel;
    //Battle Ended
    public GameObject CardBattlePanel;

    //it's management player Win ,Drow and Loss
    public GameObject GameBattlePanel;

    //Time countDown
    public UILabel TimeText;
    public GameObject Timer;

    //  where is the card? ,card position;
    public GameObject Pos01Card;
    public GameObject Pos02Card;
    public GameObject Pos03Card;

    //it's controlled player battle end sprite
    public UILabel PlayerBattled;
    public UILabel PlayerHp;
    public UILabel RemoteHP;

    //Create Prefab
    public GameObject playerPref;
    public GameObject remotePref;
    //Where Prefab
    public GameObject playerpos;
    public GameObject remotepos;


    //Sound
    public AudioClip battlebgm;

    public int PlayerDamage;
    public int RemoteDamage;
    public int localPlayerHp;
    public int remotePlayerHp;
    public int PlayerElement;
    public int RemoteElement;

    public bool IsShowingResults;

    private ResultType result;
    public enum ResultType
    {
        None = 0,
        Draw,
        LocalWin,
        LocalLoss
    }

    private GameTurnManager turnManager;

    public void Awake()
    {
        gameCoreSetting = this;
    }

    public void Start()
    {
      
        this.turnManager = this.gameObject.AddComponent<GameTurnManager>();
        this.turnManager.TurnManagerListener = this;
        this.turnManager.TurnDuration = 20.0f;
        this.localPlayerHp = 10;
        this.remotePlayerHp = 10;
        //Damage , Selected Reset
        this.PlayerDamage = 0;
        this.RemoteDamage = 0;


        RefreshUiView();
    }

    public void Update()
    {
        //디버그용
        if (Input.GetKeyUp(KeyCode.L))
        {
            PhotonNetwork.LeaveRoom();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            PhotonNetwork.ConnectUsingSettings(null);
            PhotonHandler.StopFallbackSendAckThread();
        }

        //real
        if (!PhotonNetwork.inRoom)
        {
            return;
        }
        if (PhotonNetwork.room.PlayerCount > 1)
        {
            if (this.turnManager.IsOver)
            {
                return;
            }
        }
        if (PhotonNetwork.room.PlayerCount >= 2)
        {
            ConnectUiView.gameObject.SetActive(false);
            GameUiView.gameObject.SetActive(true);
            CardPanel.gameObject.SetActive(true);
        }
        if (this.turnManager.Turn > 0 && this.TimeText.GetComponent<UILabel>().text != null)
        {
            TimeText.GetComponent<UILabel>().text = this.turnManager.RemainingSecondsInTurn.ToString("F1") + " SECONDS";
            Timer.GetComponent<UIProgressBar>().value = this.turnManager.RemainingSecondsInTurn / 20;
        }

    }

    void RefreshUiView()
    {
        ConnectUiView.gameObject.SetActive(true);
        GameUiView.gameObject.SetActive(false);
        CardPanel.gameObject.SetActive(false);
        CardBattlePanel.gameObject.SetActive(false);

    }
    public void RefreshCardView()
    {
        CardPanel.transform.GetChild(0).gameObject.SetActive(false);
        CardPanel.transform.GetChild(1).gameObject.SetActive(false);
        CardPanel.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void OnTurnBegins(int turn)
    {
   
        Debug.Log("OnTurnBegins() turn: " + turn);
        this.PlayerDamage = 0;
        this.RemoteDamage = 0;
        this.PlayerElement = 0;
        this.RemoteElement = 0;
        this.UpdateScores();

    }
    public void OnTurnCompleted(int obj)
    {
        Debug.Log("OnTurnCompleted: " + obj);
        this.CalculateWinAndLoss();
        this.OnEndTurn();
    }
    public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move, object elementchoice)
    {
        Debug.Log("OnPlayerMove: " + photonPlayer + " turn: " + turn + " action: " + move);
        throw new NotImplementedException();
    }
    public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move, object element)
    {
        Debug.Log("OnTurnFinished: " + photonPlayer + " turn: " + turn + " action: " + move+"element: " +element);
        //damage
        if (photonPlayer.IsLocal)
        {
            this.PlayerDamage = (int)(byte)move;
            this.PlayerElement = (int)(byte)element;
        }
        else
        {
            this.RemoteDamage = (int)(byte)move;
            this.RemoteElement = (int)(byte)element;
        }
    }
    public void OnTurnTimeEnds(int obj)
    {
        if (!IsShowingResults)
        {
            Debug.Log("OnTurnTimeEnds: Calling OnTurnCompleted");
            OnTurnCompleted(-1);
        }
    }
    private void UpdateScores()
    {
        cardInfo.CardDrow();
        CardPanel.transform.GetChild(0).gameObject.SetActive(true);
        CardPanel.transform.GetChild(1).gameObject.SetActive(true);
        CardPanel.transform.GetChild(2).gameObject.SetActive(true);
    }


    public void StartTurn()
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.turnManager.BeginTurn();
            SoundManager.Inst.Ds_BgmPlayer(battlebgm);
        }
    }
    public void MakeTurn(int damage, int element)
    {
        this.turnManager.SendMove((byte)damage,(byte)element ,true);
    }

    public void OnEndTurn()
    {
        this.StartCoroutine("ShowResultsBeginNextTurnCoroutine");
    }
    public IEnumerator ShowResultsBeginNextTurnCoroutine()
    {
        CardBattlePanel.gameObject.SetActive(true);
        PlayerHp.text = this.localPlayerHp.ToString();
        RemoteHP.text = this.remotePlayerHp.ToString();
        this.RefreshCardView();
        yield return new WaitForSeconds(2.0f);
        //오브젝트 삭제하기;
        this.DestroyChild();
        CardBattlePanel.gameObject.SetActive(false);
        if (this.turnManager.Turn < 10)
        {
            this.StartTurn();
        }
        if (this.turnManager.Turn >= 20 || this.localPlayerHp <= 0 || this.remotePlayerHp <= 0)
        {
            EndGame();
            SoundManager.Inst.Ds_BgmPlayer(null);
        }
    }
    public void EndGame()
    {
        Debug.Log("EndGame");
        PhotonNetwork.Disconnect();
        Buttonmanager.Inst.TouchBackButton();

    }

    public Item itemReturner(Item ItemReturn)
    {
        return ItemReturn;
    }

    #region Click Button(Card)

    public void OnClickBack()
    {
        this.EndGame();
    }
    public void OnClick01Card()
    {
        if( turnManager.IsFinishedByMe)
        {
            Debug.Log("씹혔다.");
            return;
        }
        if (cardInfo.hands.Count == 0)
        {
            this.Pos01Card.gameObject.SetActive(false);
            return;
        }
        MakeTurn(cardInfo.hands[0].cardDamage, CalculateChar(cardInfo.hands[0].cardElement));
        playerPref = Resources.Load((cardInfo.hands[0].cardName.ToString())) as GameObject;
        //Instantiate(playerPref, playerpos.transform.position, Quaternion.identity);
        playerpos.AddChild(playerPref);
        cardInfo.hands.RemoveAt(0);
        // this.Pos01Card.gameObject.SetActive(false);
        this.RefreshCardView();


    }
    public void OnClick02Card()
    {
        if (turnManager.IsFinishedByMe)
        {
            return;
        }
        if (cardInfo.hands.Count == 1)
        {
            this.Pos02Card.gameObject.SetActive(false);
            return;
        }
        MakeTurn(cardInfo.hands[1].cardDamage, CalculateChar(cardInfo.hands[1].cardElement));
        Debug.Log("CardDamage :" + GameDataBase.Inst.cards_Deck[1].cardDamage.ToString());
        playerPref = Resources.Load((cardInfo.hands[1].cardName.ToString())) as GameObject;
        playerPref.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //Instantiate(playerPref, playerpos.transform.position, Quaternion.identity);
        playerpos.AddChild(playerPref);
        cardInfo.hands.RemoveAt(1);
        this.RefreshCardView();
     
    }
    public void OnClick03Card()
    {
        if (turnManager.IsFinishedByMe)
        {
            return;
        }
        if (cardInfo.hands.Count == 2)
        {
            this.Pos03Card.gameObject.SetActive(false);
            return;
        }
        MakeTurn(cardInfo.hands[2].cardDamage, CalculateChar(cardInfo.hands[2].cardElement));
        Debug.Log("CardDamage :" + GameDataBase.Inst.cards_Deck[2].cardDamage.ToString());
        playerPref = Resources.Load((cardInfo.hands[2].cardName.ToString())) as GameObject;
        // Instantiate(playerPref, playerpos.transform.position, Quaternion.identity);
        playerpos.AddChild(playerPref);
        cardInfo.hands.RemoveAt(2);
        this.RefreshCardView();
      
    }
    #endregion
    public override void OnJoinedRoom()
    {

        if (PhotonNetwork.room.PlayerCount == 2)
        {
            if (this.turnManager.Turn == 0)
            {
                // when the room has two players, start the first turn (later on, joining players won't trigger a turn)
                this.StartTurn();
            }
        }
        else
        {
            Debug.Log("Waiting for another player");
        }
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("Other player arrived");

        if (PhotonNetwork.room.PlayerCount == 2)
        {
            if (this.turnManager.Turn == 0)
            {
             
                // when the room has two players, start the first turn (later on, joining players won't trigger a turn)
                this.StartTurn();
                
            }
        }
    }
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("Other player disconnected! " + otherPlayer.ToStringFull());
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("재접속");
    }
    private Element CalculateNum(int num)
    {
        if ((int)Element.Stone == num)
        {
            return Element.Stone;
        }
        else if ((int)Element.Wood == num)
        {
            return Element.Wood;
        }
        else if ((int)Element.Grass == num)
        {
            return Element.Grass;
        }
        else if ((int)Element.Chaos==num)
        {
            return Element.Chaos;
        }
        else
        {
            return 0;
        }
    }
    private int CalculateChar(Element num)
    {
        if (num==Element.Stone)
        {
            return 1;
        }
        else if (num == Element.Wood)
        {
            return 2;
        }
        else if (num == Element.Grass)
        {
            return 3;
        }
        else if (num == Element.Chaos)
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }

    private void CalculateWinAndLoss()
    {
        this.result = ResultType.Draw;



        {
            //안골랐을때
            if (this.PlayerElement == 0)
            {
                this.result = ResultType.LocalLoss;
                localPlayerHp = localPlayerHp - RemoteDamage;
            }
            if (this.RemoteElement == 0)
            {
                this.result = ResultType.LocalWin;
                remotePlayerHp = remotePlayerHp - localPlayerHp;
            }
            #region Rock
            //내가 바위일때
            if (this.PlayerElement == 1)
            {
                //상대가 바위일때
                if (this.RemoteElement == 1)
                {
                    if (this.RemoteDamage < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - this.RemoteDamage);
                    }
                    if (this.RemoteDamage > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - (this.RemoteDamage - this.PlayerDamage);
                    }
                    if (this.RemoteDamage == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
                //상대가 물일때
                if (this.RemoteElement == 3)
                {
                    if (this.RemoteDamage + 2 < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - (this.RemoteDamage + 2));
                    }
                    if (this.RemoteDamage + 2 > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - ((this.RemoteDamage + 2) - this.PlayerDamage);
                    }
                    if (this.RemoteDamage + 2 == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
                //상대가 풀일때
                if (this.RemoteElement == 2)
                {
                    if (this.RemoteDamage - 2 < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - (this.RemoteDamage - 2));
                    }
                    if (this.RemoteDamage - 2 > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - ((this.RemoteDamage - 2) - this.PlayerDamage);
                    }
                    if (this.RemoteDamage - 2 == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
            }
            #endregion
            #region Grass
            //내가 풀일때
            else if (this.PlayerElement == 3)
            {
                //상대가 바위일때
                if (this.RemoteElement == 1)
                {
                    if (this.RemoteDamage - 2 < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - (this.RemoteDamage - 2));
                    }
                    if (this.RemoteDamage - 2 > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - ((this.RemoteDamage - 2) - this.PlayerDamage);
                    }
                    if (this.RemoteDamage - 2 == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
                //상대가 물일때
                if (this.RemoteElement == 3)
                {
                    if (this.RemoteDamage < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - this.RemoteDamage);
                    }
                    if (this.RemoteDamage > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - (this.RemoteDamage - this.PlayerDamage);
                    }
                    if (this.RemoteDamage == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
                //상대가 풀일때
                if (this.RemoteElement == 2)
                {
                    if (this.RemoteDamage + 2 < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - (this.RemoteDamage + 2));
                    }
                    if (this.RemoteDamage + 2 > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - ((this.RemoteDamage + 2) - this.PlayerDamage);
                    }
                    if (this.RemoteDamage + 2 == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
            }
            #endregion
            #region Wood
            //내가 나무
            else if (this.PlayerElement == 2)
            {
                //상대가 바위일때
                if (this.RemoteElement == 1)
                {
                    if (this.RemoteDamage + 2 < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - (this.RemoteDamage + 2));
                    }
                    if (this.RemoteDamage + 2 > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - ((this.RemoteDamage + 2) - this.PlayerDamage);
                    }
                    if (this.RemoteDamage + 2 == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
                //상대가 물일때
                if (this.RemoteElement == 3)
                {
                    if (this.RemoteDamage - 2 < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - (this.RemoteDamage - 2));
                    }
                    if (this.RemoteDamage - 2 > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - ((this.RemoteDamage - 2) - this.PlayerDamage);
                    }
                    if (this.RemoteDamage - 2 == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
                //상대가 풀일때
                if (this.RemoteElement == 2)
                {
                    if (this.RemoteDamage < this.PlayerDamage)
                    {
                        this.result = ResultType.LocalWin;
                        remotePlayerHp = remotePlayerHp - (this.PlayerDamage - (this.RemoteDamage));
                    }
                    if (this.RemoteDamage > this.PlayerDamage)
                    {
                        this.result = ResultType.LocalLoss;
                        localPlayerHp = localPlayerHp - ((this.RemoteDamage) - this.PlayerDamage);
                    }
                    if (this.RemoteDamage == this.PlayerDamage)
                    {
                        this.result = ResultType.Draw;
                    }
                }
            }
            #endregion
            #region Chaos
            else if (this.PlayerElement == 4)
            {
                if (PlayerDamage < RemoteDamage)
                {
                    this.result = ResultType.LocalLoss;
                    remotePlayerHp = remotePlayerHp - ((this.RemoteDamage) - this.PlayerDamage);
                }
                if (PlayerDamage > RemoteDamage)
                {
                    this.result = ResultType.LocalWin;
                    localPlayerHp = localPlayerHp - ((this.RemoteDamage) - this.PlayerDamage);
                }
                if (PlayerDamage == RemoteDamage)
                {
                    this.result = ResultType.Draw;
                }
            }
            #endregion


        }
        if (this.result == ResultType.Draw)
        {
            PlayerBattled.text = "Draw!";
        }
        if (this.result == ResultType.LocalWin)
        {
            PlayerBattled.text = "승리!";
        }
        if (this.result == ResultType.LocalLoss)
        {
            PlayerBattled.text = "패배!";
        }
    }

    public void DestroyChild()
    {
        if(playerpos.transform.childCount<0)
        {
            return;
        }
        else
        {
            playerpos.transform.DestroyChildren();
        }
      
        if (remotepos.transform.childCount < 0)
        {
            return;
        }
        else
        {
            remotepos.transform.DestroyChildren();
        }

    }
}
