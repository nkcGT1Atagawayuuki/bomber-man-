using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    SoundManager soundManager;
    float timer =0.0f;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("BomberMan"))
        {
            Destroy(gameObject);
            soundManager.PowerUpSE();
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Explosion"))
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)  //1.5秒以上のExplotionに当たり続けると消える
            {
                Destroy(gameObject);
            }
        }
    }
}
