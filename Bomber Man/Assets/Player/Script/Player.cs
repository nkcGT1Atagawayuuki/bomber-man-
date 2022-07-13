using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private int _controlX = 0;
    private int _controlZ = 0;

    private float _angle = 0.0f;

    public int PlayerLife = 0;       //プレイヤーの体力
    public int BombCount = 0;        //プレイヤーのボムの個数
    public bool Death = false;       //プレイヤーが死んだときtrueにしてifの処理をする
    public CapsuleCollider Capsule;  //CapsulColliderの取得
    public Rigidbody rigidbody;      //Rigidbodyの取得

    public Bomb bomb;

    static readonly int[,] ROTATION = new int[,]
    {
        {3,2,1},
        {4,-1,0},
        {5,6,7},
    };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Death);

        float forward = 0.0f;

        _controlX = 0;
        _controlZ = 0;

        if(Death == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _controlZ++;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _controlZ--;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _controlX++;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _controlX--;
            }

            if (_controlX != 0 || _controlZ != 0)
            {
                forward = 1.0f;
            }
            animator.SetFloat("Walk", forward, 0.1f, Time.deltaTime);

            
            if(BombCount >= 1)　                     //ボムカウントが1以上の時ボムが置ける
            {
                if (Input.GetKeyDown(KeyCode.Space)) //ボタンを押したらボムを配置
                {
                    int x, z;
                    BlockField.GetBomberPositon(out x, out z, transform.localPosition);
                    GameSystem.instance.SetBomb(x, z);
                    BombCount -= 1;                  //ボムカウントを-1する
                }
            }
        } 
    }

    private void FixedUpdate()
    {
        //移動
        float speed = 3.0f;
        float deltaX = 0.0f;
        float deltaZ = 0.0f;
        if(_controlX != 0)
        {
            deltaX += Time.deltaTime * _controlX * speed;
        }
        if (_controlZ != 0)
        {
            deltaZ += Time.deltaTime * _controlZ * speed;//
        }
        transform.localPosition += new Vector3(deltaX, 0.0f, deltaZ);

        int controlAngle = ROTATION[_controlZ + 1, _controlX + 1];
        if (controlAngle >= 0)
        {
            _angle = controlAngle * 360.0f / 8.0f + 90.0f;
        }
        transform.localEulerAngles = new Vector3(0.0f, _angle, 0.0f);
    }

    public void BombCountAdd()
    {
        BombCount += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Explotion")
        {
            Debug.Log("爆風に当たった");
            PlayerLife -= 1;

            if(PlayerLife == 0)
            {
                rigidbody.isKinematic = true;
                Capsule.enabled = false;
                Death = true;
                animator.SetBool("Death", true);
            }
        }

        if(other.gameObject.tag == "PowerUp")
        {
            Debug.Log("up");
            bomb.PowerUp(); //Bombスクリプトのメソッド実行
        }
    }
}
