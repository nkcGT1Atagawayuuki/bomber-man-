using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    int _x = 0;
    int _z = 0;
    public int Fire = 1;     //�����̈З�
    public int MaxFire = 7;  //�����̍ő�З�

    float _timer = 2.5f;
    Player player;
    SoundManger soundManger;
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("BomberMan").GetComponent<Player>();
        soundManger = GameObject.Find("SoundManger").GetComponent<SoundManger>();
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
        if(MaxFire >= Fire)
        {
            Debug.Log("Fire���オ����");
            Fire += 1;
        }
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
            soundManger.ExplotionSE();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�v���C���[�Əd�Ȃ��Ă���");
        boxCollider.isTrigger = false;
        player.BomOverlap = false;
    }
}
