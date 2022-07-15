using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Debug.Log("Player‚ª‚«‚½");
            boxCollider.enabled = true;
        }
    }

}
