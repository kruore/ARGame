using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateElement : NetworkBehaviour
{
    private GameObject RespawnPos;
    public GameObject SpawnPos;
    public GameObject EnemySpawnPos;
    public GameObject MyCharacter;
    public GameObject EnemyCharacter;

    [Command]
    void CmdCreateCard()
    {
        //if (!isLocalPlayer)
        //{
        //    RespawnPos = EnemySpawnPos;
        //    var Element = (GameObject)Instantiate(EnemyCharacter, RespawnPos.transform.position, Quaternion.Euler(new Vector3(0, 210, 0)));
        //    NetworkServer.Spawn(Element);
        //    Debug.Log("적 위치에 생성");
        //    return;
        //}
        //RespawnPos = SpawnPos;
        //var Player = (GameObject)Instantiate(MyCharacter, RespawnPos.transform.position, Quaternion.Euler(new Vector3(0, 30, 0)));
        //NetworkServer.Spawn(Player);
        //Debug.Log("내 위치에 생성");
        //var Player = Instantiate(MyCharacter) as GameObject;
        //Player.transform.localPosition = SpawnPos.transform.position;
        //NetworkServer.Spawn(Player);

    }
    //public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    //{
    //    var player = (GameObject)GameObject.Instantiate(MyCharacter, SpawnPos.transform.position, Quaternion.Euler(new Vector3(0,210,0)));
    //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    //}
}