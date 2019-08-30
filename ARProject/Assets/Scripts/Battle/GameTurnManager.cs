using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;
using ExitGames = ExitGames.Client.Photon.Hashtable;


public class GameTurnManager : PunBehaviour
{
    //Finished Players Data
    private readonly HashSet<PhotonPlayer> finishedPlayers = new HashSet<PhotonPlayer>();

    //what's the Event
    public const byte TurnManagerEventOffset = 0;

    //move event message byte.
    public const byte EvCharacterClicked = 1 + TurnManagerEventOffset;

    //Fimal Move event
    public const byte EvFinalMove = 2 + TurnManagerEventOffset;

    //keep track of message calls
    private bool _isOverCallProcessed = false;

    public IMyTurnManagerCallbacks TurnManagerListener;

    public int Turn
    {
        get
        {
            return PhotonNetwork.room.GetTurn();
        }
        private set
        {
            _isOverCallProcessed = false;

            PhotonNetwork.room.SetTurn(value, true);
        }
    }

    public float TurnDuration = 20f;

    public float ElapsedTimeInTurn
    {
        get { return ((float)(PhotonNetwork.ServerTimestamp - PhotonNetwork.room.GetTurnStart())) / 1000.0f; }
    }
    public float RemainingSecondsInTurn
    {
        get { return Mathf.Max(0f, this.TurnDuration - this.ElapsedTimeInTurn);}
    }

    public bool IsCompletedByAll
    {
        get { return PhotonNetwork.room != null && Turn > 0 && this.finishedPlayers.Count == PhotonNetwork.room.PlayerCount; }
    }
    public bool IsFinishedByMe
    {
        get { return this.finishedPlayers.Contains(PhotonNetwork.player); }
    }
    public bool IsOver
    {
        get { return this.RemainingSecondsInTurn <= 0f; }
    }

    private void Start()
    {
        PhotonNetwork.OnEventCall += OnEvent;

    }
    void Update()
    {
        if (Turn > 0 && this.IsOver && !_isOverCallProcessed)
        {
            _isOverCallProcessed = true;
            this.TurnManagerListener.OnTurnTimeEnds(this.Turn);
        }

    }



    public void BeginTurn()
    {
        Turn = this.Turn + 1;
    }

    public void SendMove(object move,object element, object cardnum,bool finished)
    {
        if (IsFinishedByMe)
        {
            UnityEngine.Debug.LogWarning("Can't SendMove");
            return;
        }

        Hashtable moveHit = new Hashtable();
        moveHit.Add("turn", Turn);
        moveHit.Add("move", move);
        moveHit.Add("element",element);
        moveHit.Add("cardnum", cardnum);

        byte evCode = (finished) ? EvFinalMove : EvCharacterClicked;
        PhotonNetwork.RaiseEvent(evCode, moveHit, true, new RaiseEventOptions() { CachingOption = EventCaching.AddToRoomCache });
        if (finished)
        {
            PhotonNetwork.player.SetFinishedTurn(Turn);
        }
        OnEvent(evCode, moveHit, PhotonNetwork.player.ID);
    }
    public bool GetPlayerFinishedTurn(PhotonPlayer player)
    {
        if (player != null && this.finishedPlayers != null && this.finishedPlayers.Contains(player))
        {
            return true;
        }

        return false;
    }
    public void OnEvent(byte eventCode, object content, int senderId)
    {
        PhotonPlayer sender = PhotonPlayer.Find(senderId);
        switch (eventCode)
        {
            case EvCharacterClicked:
                {
                    Hashtable evTable = content as Hashtable;
                    int turn = (int)evTable["turn"];
                    object move = evTable["move"];
                    object element = evTable["element"];
                    object cardnum = evTable["cardnum"];
                    break;
                }
            case EvFinalMove:
                {
                    Hashtable evTable = content as Hashtable;
                    int turn = (int)evTable["turn"];
                    object move = evTable["move"];
                    object element = evTable["element"];
                    object cardnum = evTable["cardnum"];

                    if (turn == this.Turn)
                    {
                        this.finishedPlayers.Add(sender);

                        this.TurnManagerListener.OnPlayerFinished(sender, turn, move, element,cardnum);

                    }

                    if (IsCompletedByAll)
                    {
                        this.TurnManagerListener.OnTurnCompleted(this.Turn);
                    }
                    break;
                }
        }

    }
    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {

        //   Debug.Log("OnPhotonCustomRoomPropertiesChanged: "+propertiesThatChanged.ToStringFull());

        if (propertiesThatChanged.ContainsKey("Turn"))
        {
            _isOverCallProcessed = false;
            this.finishedPlayers.Clear();
            this.TurnManagerListener.OnTurnBegins(this.Turn);
        }
    }
}
public interface IMyTurnManagerCallbacks
{
    void OnTurnBegins(int turn);

    void OnTurnCompleted(int turn);

    void OnPlayerMove(PhotonPlayer player, int turn, object move,object elementchoice, object cardnum);

    void OnPlayerFinished(PhotonPlayer player, int turn, object move,object elementchoice,object cardnum);
    void OnTurnTimeEnds(int turn);


}

public static class TurnExtensions
{
    /// <summary>
    /// currently ongoing turn number
    /// </summary>
    public static readonly string TurnPropKey = "Turn";

    /// <summary>
    /// start (server) time for currently ongoing turn (used to calculate end)
    /// </summary>
    public static readonly string TurnStartPropKey = "TStart";

    /// <summary>
    /// Finished Turn of Actor (followed by number)
    /// </summary>
    public static readonly string FinishedTurnPropKey = "FToA";

    /// <summary>
    /// Sets the turn.
    /// </summary>
    /// <param name="room">Room reference</param>
    /// <param name="turn">Turn index</param>
    /// <param name="setStartTime">If set to <c>true</c> set start time.</param>
    public static void SetTurn(this Room room, int turn, bool setStartTime = false)
    {
        if (room == null || room.CustomProperties == null)
        {
            return;
        }

        Hashtable turnProps = new Hashtable();
        turnProps[TurnPropKey] = turn;
        if (setStartTime)
        {
            turnProps[TurnStartPropKey] = PhotonNetwork.ServerTimestamp;
        }

        room.SetCustomProperties(turnProps);
    }

    /// <summary>
    /// Gets the current turn from a RoomInfo
    /// </summary>
    /// <returns>The turn index </returns>
    /// <param name="room">RoomInfo reference</param>
    public static int GetTurn(this RoomInfo room)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnPropKey))
        {
            return 0;
        }

        return (int)room.CustomProperties[TurnPropKey];
    }


    /// <summary>
    /// Returns the start time when the turn began. This can be used to calculate how long it's going on.
    /// </summary>
    /// <returns>The turn start.</returns>
    /// <param name="room">Room.</param>
    public static int GetTurnStart(this RoomInfo room)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnStartPropKey))
        {
            return 0;
        }

        return (int)room.CustomProperties[TurnStartPropKey];
    }

    /// <summary>
    /// gets the player's finished turn (from the ROOM properties)
    /// </summary>
    /// <returns>The finished turn index</returns>
    /// <param name="player">Player reference</param>
    public static int GetFinishedTurn(this PhotonPlayer player)
    {
        Room room = PhotonNetwork.room;
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnPropKey))
        {
            return 0;
        }

        string propKey = FinishedTurnPropKey + player.ID;
        return (int)room.CustomProperties[propKey];
    }

    /// <summary>
    /// Sets the player's finished turn (in the ROOM properties)
    /// </summary>
    /// <param name="player">Player Reference</param>
    /// <param name="turn">Turn Index</param>
    public static void SetFinishedTurn(this PhotonPlayer player, int turn)
    {
        Room room = PhotonNetwork.room;
        if (room == null || room.CustomProperties == null)
        {
            return;
        }

        string propKey = FinishedTurnPropKey + player.ID;
        Hashtable finishedTurnProp = new Hashtable();
        finishedTurnProp[propKey] = turn;

        room.SetCustomProperties(finishedTurnProp);
    }
}