using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{

    public GameObject expPrefab;
    public LayerMask levelMask;

    private bool explosion = false;
    bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        Instantiate(expPrefab, transform.position, Quaternion.identity);

        exploded = true;

        StartCoroutine(CreateExp(Vector3.forward));
        StartCoroutine(CreateExp(Vector3.right));
        StartCoroutine(CreateExp(Vector3.back));
        StartCoroutine(CreateExp(Vector3.left));

        Destroy(gameObject, 0.3f);
    }

    private IEnumerator CreateExp(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            RaycastHit hit;

            Physics.Raycast
            (
                transform.position + new Vector3(0, 0.5f, 0),
                direction,
                out hit,
                i,levelMask
            );

            if (!hit.collider)
            {
                Instantiate
                (
                    expPrefab,
                    transform.position + (i * direction),
                    expPrefab.transform.rotation
                );
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exploded == true)
        {
            return;
        }

        if (other.CompareTag("Explosion"))
        {
            Explode();
        }
    }
}
