using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab; // 爆発エフェクトのプレハブ
    public LayerMask levelMask; // ステージのレイヤー
    public int Fire = 0;

    private bool explosion = false;

    //爆発済み？false：まだ爆発していない
    bool exploded = false;

    Player player;
    SoundManager soundManager;
    public SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("BomberMan").GetComponent<Player>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        // 3 秒後に Explode 関数を実行
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireUp()
    {
        Fire += 1;
    }

    public void FireReset()
    {
        Fire = 2;
    }

    void Explode()
    {
        soundManager.ExplotionSE();

        // 爆弾の位置に爆発エフェクトを作成
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 爆弾を非表示にする
        GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Collider").gameObject.SetActive(false);

        exploded = true;

        // 爆風を広げる
        StartCoroutine(CreateExplosions(Vector3.forward)); // 上に広げる
        StartCoroutine(CreateExplosions(Vector3.right)); // 右に広げる
        StartCoroutine(CreateExplosions(Vector3.back)); // 下に広げる
        StartCoroutine(CreateExplosions(Vector3.left)); // 左に広げる

        // 0.3 秒後に非表示にした爆弾を削除
        Destroy(gameObject, 0.3f);

        player.BombCount += 1;
    }

    // 爆風を広げる
    private IEnumerator CreateExplosions(Vector3 direction)
    {
        // 2 マス分ループする
        for (int i = 1; i < Fire; i++)
        {
            // ブロックとの当たり判定の結果を格納する変数
            RaycastHit hit;

            // 爆風を広げた先に何か存在するか確認
            Physics.Raycast
            (
                transform.position + new Vector3(0, 0.5f, 0),
                direction,
                out hit,
                i,
                levelMask
            );

            // 爆風を広げた先に何も存在しない場合
            if (!hit.collider)
            {
                // 爆風を広げるために、
                // 爆発エフェクトのオブジェクトを作成
                Instantiate
                (
                    explosionPrefab,
                    transform.position + (i * direction),
                    explosionPrefab.transform.rotation
                );
            }
            // 爆風を広げた先にブロックが存在する場合
            else
            {
                // 爆風はこれ以上広げない
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(exploded == true)
        {
            //すでに爆発をしているなら、処理を終える
            return;
        }

        //ぶつかった物がExplosionだったら爆破する
        if (other.CompareTag("Explosion"))
        {
            Explode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Playerが離れた");
        
            player.BombOverlap = false;
        sphereCollider.isTrigger = false;
    }
}
