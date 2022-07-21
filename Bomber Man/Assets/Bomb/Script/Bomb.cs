using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Fire = 0;
    public float ExplotionTime = 0f;
    public GameObject ExplotionPre;

    // Start is called before the first frame update
    void Start()
    {
        //Å`ïbå„Ç…îöî≠
        Invoke("Explotion", ExplotionTime);
    }

    IEnumerator Explotions (Vector3 direcion)
    {
        for(int i=0; i<Fire; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direcion, out hit);
        }
        return null;
    }

    void Explotion()
    {
        //îöïóÇÃê∂ê¨
        Instantiate(ExplotionPre, transform.position, Quaternion.identity);

        StartCoroutine(Explotions(Vector3.forward));
        StartCoroutine(Explotions(Vector3.right));
        StartCoroutine(Explotions(Vector3.back));
        StartCoroutine(Explotions(Vector3.left));

        Destroy(gameObject);
    }

}
