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
        //Å`ïbå„Ç…îöî≠
        Invoke("Explotion", ExplotionTime);
    }

    void Explotion()
    {
        //îöïóÇÃê∂ê¨
        Instantiate(ExplotionPre, transform.position, Quaternion.identity);

        //0.3ïbå„Ç…è¡Ç∑
        Destroy(gameObject, 0.3f);
    }

}
