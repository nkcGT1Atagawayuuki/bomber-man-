using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
{

    //部屋の作成・参加
    void Start()
    {
        //サーバー接続　絶対いるらしい
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() //OnConnectedToMaster()はPhotonが常に作ってる（上書きしている）
    {
        base.OnConnectedToMaster();

        Debug.Log("サーバーに接続した!");

        //部屋を作る or すでに部屋があるなら入室する
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        //RoomOptions()が入れる人数、TypedLobby.Defaultがロビーを指定できるらしい
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("部屋に参加!");
        //Playerを生成
        PhotonNetwork.Instantiate("BomberMan", new Vector3(9f, 0f, 5f), Quaternion.identity);
    }
}
