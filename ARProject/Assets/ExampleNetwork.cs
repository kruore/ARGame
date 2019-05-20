using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExampleNetwork : NetworkManager
{
    NetworkClient mClient;
    NetworkServer mServer;

    public NetworkDiscovery mDiscovery;

    void Awake ()
    {
    // Resources 폴더에 있는 Character라는 이름을 가진 프리팹 파일을 가져온다.
    GameObject prefab = Resources.Load<GameObject>("Misaki_SchoolUniform_summer");
    GameObject card = Resources.Load<GameObject>("Card");
        // spawnPrefabs 리스트에 스폰할 오브젝트를 추가
    spawnPrefabs.Add(prefab);
    spawnPrefabs.Add(card);
    playerPrefab = prefab;
    }

    #region Client
    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        Debug.Log("StartClient");
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        Debug.Log("StopClient");
    }

    public void SetupClient()
    {
        Debug.Log("SetUp");
        StartClient();

        mDiscovery.Initialize();
        mDiscovery.StartAsClient();


        mClient = new NetworkClient();
        mClient.Connect("127.0.0.1",8888);

    }
    #endregion

    #region Server
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        // spawnPrefabs에 등록된 프리팹을 스폰한다.
        GameObject charObj = Instantiate(spawnPrefabs[0]);

        // 네트워크를 통해서 이 오브젝트가 생성되었음을 클라이언트에 알린다.
        NetworkServer.Spawn(charObj);
    }
    #endregion
}
