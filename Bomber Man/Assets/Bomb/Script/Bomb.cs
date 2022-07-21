using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab; // �����G�t�F�N�g�̃v���n�u
    public LayerMask levelMask; // �X�e�[�W�̃��C���[
    public int Fire = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 3 �b��� Explode �֐������s
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Explode()
    {
        // ���e�̈ʒu�ɔ����G�t�F�N�g���쐬
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // ���e���\���ɂ���
        GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Collider").gameObject.SetActive(false);

        // �������L����
        StartCoroutine(CreateExplosions(Vector3.forward)); // ��ɍL����
        StartCoroutine(CreateExplosions(Vector3.right)); // �E�ɍL����
        StartCoroutine(CreateExplosions(Vector3.back)); // ���ɍL����
        StartCoroutine(CreateExplosions(Vector3.left)); // ���ɍL����

        // 0.3 �b��ɔ�\���ɂ������e���폜
        Destroy(gameObject, 0.3f);
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

            // 0.05 �b�҂��Ă���A���̃}�X�ɔ������L����
            yield return new WaitForSeconds(0.05f);
        }
    }
}
