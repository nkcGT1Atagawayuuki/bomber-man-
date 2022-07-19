using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    int _x = 0;
    int _z = 0;

    public int x { get { return _x; } }
    public int z { get { return _z; } }

    float _timer = 0.6f;

    bool _isRegisterd = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer < 0.3f)
        {
            float s = _timer / 0.3f;
            transform.localScale = new Vector3(s, s, s);
        }
    }

    public void Initialize(int x, int z)
    {
        _x = x;
        _z = z;
    }

    private void FixedUpdate()
    {

        _timer -= Time.fixedDeltaTime;
        if(_timer <= 0.3f&& _isRegisterd)
        {
            GameSystem.instance.UnregisterExplotions(this);
        }

        if (_timer <= 0.0f)
        {
            //Á‚¦‚é
            GameObject.Destroy(this.gameObject);
        }
    }
}
