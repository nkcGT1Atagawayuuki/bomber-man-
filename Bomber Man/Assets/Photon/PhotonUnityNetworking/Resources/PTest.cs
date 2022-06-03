using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class PTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //ネットワークへの接続
        PhotonNetwork.ConnectUsingSettings();
    }

    // サーバーへの接続が成功した時
    public override void OnConnectedToMaster()
    {
        //ルームが無ければ作成してからルーム参加する
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // ルームに入ったとき時
    public override void OnJoinedRoom()
    {
        // ランダムな位置にネットワークオブジェクトを生成する
        var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        PhotonNetwork.Instantiate("Player", v, Quaternion.identity);
    }
}
