using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class PTest : MonoBehaviourPunCallbacks
{
    public bool ServerFlg; //サーバーフラグ
    public void Login(string ip, bool sf)
    {
        //サーバーフラグの設定
        ServerFlg = sf;
        //IPアドレスの設定
        PhotonNetwork.PhotonServerSettings.AppSettings.Server = ip;
        //ポート番号の設定
        PhotonNetwork.PhotonServerSettings.AppSettings.Port = 5055;
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
        var v = new Vector3(0f, 0f, 0f);
        PhotonNetwork.Instantiate("BomberMan", v, Quaternion.identity);
        //サーバーなら赤、クライアントなら青にする
        //if (ServerFlg)
        //{
        //    go.GetComponent<Renderer>().material.color = Color.red;
        //}
        //else
        //{
        //    go.GetComponent<Renderer>().material.color = Color.blue;
        //}
    }

    //接続状態の表示
    int status = 0;
    private void Update()
    {
        if (PhotonNetwork.NetworkClientState.ToString() == "ConnectingToMasterserver" && status == 0)
        {
            status = 1;
            Debug.Log("サーバーに接続中･･･");
        }
        if (PhotonNetwork.NetworkClientState.ToString() == "Authenticating" && status == 1)
        {
            status = 2;
            Debug.Log("認証中･･･");
        }
        if (PhotonNetwork.NetworkClientState.ToString() == "Joining" && status == 2)
        {
            status = 3;
            Debug.Log("ルームに参加中");
        }
    }
}
