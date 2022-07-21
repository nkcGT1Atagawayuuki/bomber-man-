using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
{

    //�����̍쐬�E�Q��
    void Start()
    {
        //�T�[�o�[�ڑ��@��΂���炵��
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() //OnConnectedToMaster()��Photon����ɍ���Ă�i�㏑�����Ă���j
    {
        base.OnConnectedToMaster();

        Debug.Log("�T�[�o�[�ɐڑ�����!");

        //��������� or ���łɕ���������Ȃ��������
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        //RoomOptions()�������l���ATypedLobby.Default�����r�[���w��ł���炵��
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("�����ɎQ��!");
        //Player�𐶐�
        PhotonNetwork.Instantiate("BomberMan", new Vector3(9f, 0f, 5f), Quaternion.identity);
    }
}
