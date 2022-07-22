using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float exp = 0f;
    public Bomb bomb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, exp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Break")
        {
            Debug.Log("‚Ô‚Â‚©‚Á‚½");
        }
    }

}
