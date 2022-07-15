using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    int _x = 0;
    int _z = 0;
    public int Fire = 1; //�����̈З�

    float _timer = 2.5f;
    Player player;
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("BomberMan").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int x,int z)
    {
        _x = x;
        _z = z;
    }

    public void FireUp()
    {
        Debug.Log("Fire+1");
        Fire += 1;
    }

    public void FireReset()
    {
        Fire = 1;  //�{���p���[�������l�ɖ߂�
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;

        if (_timer <= 0.0f)
        {
            //����
            GameSystem.instance.Explode(_x, _z, Fire);
            //������
            GameObject.Destroy(this.gameObject);
            //Player�̃��\�b�h���s
            player.BombCountAdd();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�v���C���[�Əd�Ȃ��Ă���");
        boxCollider.isTrigger = false;
    }

}
