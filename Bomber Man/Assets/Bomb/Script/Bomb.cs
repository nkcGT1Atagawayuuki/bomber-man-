using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab; // 爆発エフェクトのプレハブ
    public LayerMask levelMask; // ステージのレイヤー
    public int Fire = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 3 秒後に Explode 関数を実行
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Explode()
    {
        // 爆弾の位置に爆発エフェクトを作成
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 爆弾を非表示にする
        GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Collider").gameObject.SetActive(false);

        // 爆風を広げる
        StartCoroutine(CreateExplosions(Vector3.forward)); // 上に広げる
        StartCoroutine(CreateExplosions(Vector3.right)); // 右に広げる
        StartCoroutine(CreateExplosions(Vector3.back)); // 下に広げる
        StartCoroutine(CreateExplosions(Vector3.left)); // 左に広げる

        // 0.3 秒後に非表示にした爆弾を削除
        Destroy(gameObject, 0.3f);
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

            // 0.05 秒待ってから、次のマスに爆風を広げる
            yield return new WaitForSeconds(0.05f);
        }
    }
}
