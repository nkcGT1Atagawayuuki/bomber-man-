using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    int _x = 0;
    int _z = 0;

    float _timer = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int x, int z)
    {
        _x = x;
        _z = z;
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;

        if (_timer <= 0.0f)
        {
            //Á‚¦‚é
            GameObject.Destroy(this.gameObject);
        }
    }
}
