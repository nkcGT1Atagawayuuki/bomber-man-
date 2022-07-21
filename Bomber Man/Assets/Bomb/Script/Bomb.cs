using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab; // �����G�t�F�N�g�̃v���n�u
    public LayerMask levelMask; // �X�e�[�W�̃��C���[
    public int Fire = 0;

    private bool explosion = false;

    //�����ς݁Hfalse�F�܂��������Ă��Ȃ�
    bool exploded = false;

    Player player;
    SoundManager soundManager;
    public SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("BomberMan").GetComponent<Player>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        // 3 �b��� Explode �֐������s
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

        // ���e�̈ʒu�ɔ����G�t�F�N�g���쐬
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // ���e���\���ɂ���
        GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Collider").gameObject.SetActive(false);

        exploded = true;

        // �������L����
        StartCoroutine(CreateExplosions(Vector3.forward)); // ��ɍL����
        StartCoroutine(CreateExplosions(Vector3.right)); // �E�ɍL����
        StartCoroutine(CreateExplosions(Vector3.back)); // ���ɍL����
        StartCoroutine(CreateExplosions(Vector3.left)); // ���ɍL����

        // 0.3 �b��ɔ�\���ɂ������e���폜
        Destroy(gameObject, 0.3f);

        player.BombCount += 1;
    }

    // �������L����
    private IEnumerator CreateExplosions(Vector3 direction)
    {
        // 2 �}�X�����[�v����
        for (int i = 1; i < Fire; i++)
        {
            // �u���b�N�Ƃ̓����蔻��̌��ʂ��i�[����ϐ�
            RaycastHit hit;

            // �������L������ɉ������݂��邩�m�F
            Physics.Raycast
            (
                transform.position + new Vector3(0, 0.5f, 0),
                direction,
                out hit,
                i,
                levelMask
            );

            // �������L������ɉ������݂��Ȃ��ꍇ
            if (!hit.collider)
            {
                // �������L���邽�߂ɁA
                // �����G�t�F�N�g�̃I�u�W�F�N�g���쐬
                Instantiate
                (
                    explosionPrefab,
                    transform.position + (i * direction),
                    explosionPrefab.transform.rotation
                );
            }
            // �������L������Ƀu���b�N�����݂���ꍇ
            else
            {
                // �����͂���ȏ�L���Ȃ�
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(exploded == true)
        {
            //���łɔ��������Ă���Ȃ�A�������I����
            return;
        }

        //�Ԃ���������Explosion�������甚�j����
        if (other.CompareTag("Explosion"))
        {
            Explode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Player�����ꂽ");
        
            player.BombOverlap = false;
        sphereCollider.isTrigger = false;
    }
}
