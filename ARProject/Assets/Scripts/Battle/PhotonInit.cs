using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhotonInit : Photon.PunBehaviour

{
    public string version = "v1.0";
    public string previousRoom;
    public string UserId;
    string previousRoomPlayerPrefKey = "PHOTON.ROOM";
    const string NickNamePlayerPrefsKey = "NickName";
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(version);
    }

    public void ApplyUserIdAndConnect()
    {
        string nickName = "DemoNick";
        //if (this.InputField != null && !string.IsNullOrEmpty(this.InputField.text))
        {
            //    nickName = this.InputField.text;
            PlayerPrefs.SetString(NickNamePlayerPrefsKey, nickName);
        }

        if (PhotonNetwork.AuthValues == null)
        {
            PhotonNetwork.AuthValues = new AuthenticationValues();
        }

        PhotonNetwork.AuthValues.UserId = nickName;

        Debug.Log("Nickname: " + nickName + " userID: " + this.UserId, this);

        PhotonNetwork.playerName = nickName;
        PhotonNetwork.ConnectUsingSettings("0.5");

        PhotonHandler.StopFallbackSendAckThread();
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Make");
    }
    public override void OnConnectedToMaster()
    {
        // after connect 
        this.UserId = PhotonNetwork.player.UserId;
        ////Debug.Log("UserID " + this.UserId);

        if (PlayerPrefs.HasKey(previousRoomPlayerPrefKey))
        {
            Debug.Log("getting previous room from prefs: ");
            this.previousRoom = PlayerPrefs.GetString(previousRoomPlayerPrefKey);
            PlayerPrefs.DeleteKey(previousRoomPlayerPrefKey); // we don't keep this, it was only for initial recovery
        }


        // after timeout: re-join "old" room (if one is known)
        if (!string.IsNullOrEmpty(this.previousRoom))
        {
            Debug.Log("ReJoining previous room: " + this.previousRoom);
            PhotonNetwork.ReJoinRoom(this.previousRoom);
            this.previousRoom = null;       // we only will try to re-join once. if this fails, we will get into a random/new room
        }
        else
        {
            // else: join a random room
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnJoinedLobby()
    {
        //if player join the lobby
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Join?");
    }
    public void OnPhotonRandomJoinFailed()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        Debug.Log("No Room!");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2, PlayerTtl = 20000 }, null);
        Debug.Log(roomOptions.MaxPlayers.ToString());
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.room.Name);
        this.previousRoom = PhotonNetwork.room.Name;
        Debug.Log(PhotonNetwork.playerList.Length);
    }
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        
    }

}
