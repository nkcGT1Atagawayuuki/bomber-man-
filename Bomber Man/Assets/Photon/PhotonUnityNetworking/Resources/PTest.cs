using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class PTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //�l�b�g���[�N�ւ̐ڑ�
        PhotonNetwork.ConnectUsingSettings();
    }

    // �T�[�o�[�ւ̐ڑ�������������
    public override void OnConnectedToMaster()
    {
        //���[����������΍쐬���Ă��烋�[���Q������
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // ���[���ɓ������Ƃ���
    public override void OnJoinedRoom()
    {
        // �����_���Ȉʒu�Ƀl�b�g���[�N�I�u�W�F�N�g�𐶐�����
        var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        PhotonNetwork.Instantiate("Player", v, Quaternion.identity);
    }
}
