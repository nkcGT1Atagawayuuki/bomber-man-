using UnityEngine;
using Photon.Pun;

public class Controller : MonoBehaviourPunCallbacks
{
    public int Speed = 5;
    void Update()
    {
        //�����̃I�u�W�F�N�g�����ړ�����
        if (photonView.IsMine)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(x, y, 0) * Time.deltaTime * Speed);
        }
    }
}