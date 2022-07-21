using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{

    public float exp = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, exp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
