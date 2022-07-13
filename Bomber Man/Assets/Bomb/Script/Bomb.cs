using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    int _x = 0;
    int _z = 0;
    public int _power = 0; //�����̈З�

    float _timer = 2.5f;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int x,int z)
    {
        _x = x;
        _z = z;
    }

    public void PowerUp()
    {
        
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;

        if (_timer <= 0.0f)
        {
            //����
            GameSystem.instance.Explode(_x, _z, _power);
            //������
            GameObject.Destroy(this.gameObject);
            //Player�̃��\�b�h���s
            player.BombCountAdd();
        }
    }
}
