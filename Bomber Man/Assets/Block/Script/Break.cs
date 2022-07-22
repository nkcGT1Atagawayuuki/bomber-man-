using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Break : MonoBehaviourPunCallbacks
{

    public GameObject[] Item;
    int BreakHp = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Explosion")
        {
            BreakHp -= 1;

            if(BreakHp == 0)
            {
                Destroy(gameObject);
                int ItemCount = Random.Range(0, 4);//ランダム
                //アイテム生成       
                Instantiate(Item[ItemCount],transform.position,transform.rotation);
            }
        }
    }
}
