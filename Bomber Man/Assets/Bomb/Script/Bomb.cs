using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    int _x = 0;
    int _z = 0;
    public int Fire = 1;     //爆風の威力
    public int MaxFire = 7;  //爆風の最大威力

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
            //爆発
            GameSystem.instance.Explode(_x, _z, Fire);
            //消える
            GameObject.Destroy(this.gameObject);
            //Playerのメソッド実行
            player.BombCountAdd();
            soundManger.ExplotionSE();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("プレイヤーと重なっている");
        boxCollider.isTrigger = false;
        player.BomOverlap = false;
    }
}
