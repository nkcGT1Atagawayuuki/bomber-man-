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

    public int Fire = 1;     //爆風の威力
    public int MaxFire = 7;  //爆風の最大威力

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
            Debug.Log("Fireが上がった");
            Fire += 1;
        }
    }

    public void FireReset()
    {
        Fire = 1;  //ボムパワーを初期値に戻す
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;

        if (_timer <= 0.0f)
        {
            //登録解除
            GameSystem.instance.UnregisterBomb(this);
            //爆発
            GameSystem.instance.Explode(_x, _z, Fire);
            //消える
            GameObject.Destroy(this.gameObject);
            //Playerのメソッド実行
            player.BombCountAdd();
            soundManager.ExplotionSE();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("プレイヤーと重なっている");
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
