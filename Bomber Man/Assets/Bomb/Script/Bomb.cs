using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float ExplotionTime = 0f;
    public GameObject ExplotionPre;

    // Start is called before the first frame update
    void Start()
    {
        //�`�b��ɔ���
        Invoke("Explotion", ExplotionTime);
    }

    void Explotion()
    {
        //�����̐���
        Instantiate(ExplotionPre, transform.position, Quaternion.identity);

        //0.3�b��ɏ���
        Destroy(gameObject, 0.3f);
    }

}
