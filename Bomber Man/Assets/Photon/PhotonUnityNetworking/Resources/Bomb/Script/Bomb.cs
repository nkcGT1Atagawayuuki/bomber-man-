using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bomb : MonoBehaviourPunCallbacks
{
    int _x = 0;
    int _z = 0;

    public int x { get { return _x; } }
    public int z { get { return _z; } }

    public int Fire = 1;     //�����̈З�
    public int MaxFire = 7;  //�����̍ő�З�

    [SerializeField] private Transform _animObj = null;

    float _timer = 2.5f;
    float _animTimer = 0.0f;

    Player player;
    SoundManager soundManager;
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("BomberMan").GetComponent<Player>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float sxz = 0.8f + Mathf.Sin(_animTimer * Mathf.PI) * 0.2f;
        _animObj.localScale = new Vector3(sxz, 0.8f + -Mathf.Cos(_animTimer * Mathf.PI) * 0.2f, sxz);
        _animTimer += Time.deltaTime;
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
            //�o�^����
            GameSystem.instance.UnregisterBomb(this);
            //����
            GameSystem.instance.Explode(_x, _z, Fire);
            //������
            GameObject.Destroy(this.gameObject);
            //Player�̃��\�b�h���s
            player.BombCountAdd();
            soundManager.ExplotionSE();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("�v���C���[�Əd�Ȃ��Ă���");
        boxCollider.isTrigger = false;
        player.BomOverlap = false;
    }

    public void Chain()
    {
        if(_timer > 0.2f)
        {
            _timer = 0.2f;
        }
    }
}
