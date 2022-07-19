using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class PTest : MonoBehaviourPunCallbacks
{
    public static PTest instance;
    public bool ServerFlg; //�T�[�o�[�t���O
    public GameObject[] Player;
    int PlayerCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Login(string ip, bool sf)
    {
        //�T�[�o�[�t���O�̐ݒ�
        ServerFlg = sf;
        //IP�A�h���X�̐ݒ�
        PhotonNetwork.PhotonServerSettings.AppSettings.Server = ip;
        //�|�[�g�ԍ��̐ݒ�
        PhotonNetwork.PhotonServerSettings.AppSettings.Port = 5055;
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
        var v = new Vector3(9f, 0f, 5f);
        PhotonNetwork.Instantiate("BomberMan", v, Quaternion.identity);
        
    }

    //�ڑ���Ԃ̕\��
    int status = 0;
    private void Update()
    {
        if (PhotonNetwork.NetworkClientState.ToString() == "ConnectingToMasterserver" && status == 0)
        {
            status = 1;
            Debug.Log("�T�[�o�[�ɐڑ������");
        }
        if (PhotonNetwork.NetworkClientState.ToString() == "Authenticating" && status == 1)
        {
            status = 2;
            Debug.Log("�F�ؒ����");
        }
        if (PhotonNetwork.NetworkClientState.ToString() == "Joining" && status == 2)
        {
            status = 3;
            Debug.Log("���[���ɎQ����");
        }
    }
}
