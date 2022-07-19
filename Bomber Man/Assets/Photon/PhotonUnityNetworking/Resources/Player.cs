using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator animator = null;

     int _controlX = 0;
     int _controlZ = 0;

    public float Xlimit = 6;
    public float Ylimit = 1;
    public float Zlimit = 6;

    public float speed = 1f;       //プレイヤーの速度
    public int BombCount = 1;        //プレイヤーのボムの個数
    private float Maxspeed = 3f;   //プレイヤーの最大速度
    public int MaxBomCount = 4;      //プレイヤーの最大ボムの個数

    private float _angle = 0.0f;

    public bool Death = false;       //プレイヤーが死んだときtrueにしてifの処理をする
    public bool BomOverlap = false;  //ボムを重ねておけないようにする
    public CapsuleCollider capsuleCollider;  //CapsulColliderの取得
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

        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, -Xlimit, Xlimit);
        currentPos.y = Mathf.Clamp(currentPos.y, -Ylimit, Ylimit);
        currentPos.z = Mathf.Clamp(currentPos.z, -Zlimit, Zlimit);
        transform.position = currentPos;

        if (Death == false)
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

            
            if(BombCount >= 1 && BomOverlap == false)　                     //ボムカウントが1以上の時ボムが置ける
            {
                if (Input.GetKeyDown(KeyCode.Space)) //ボタンを押したらボムを配置
                {
                    int x, z;
                    BlockField.GetBomberPositon(out x, out z, transform.localPosition);
                    GameSystem.instance.SetBomb(x, z);
                    BombCount -= 1;                  //ボムカウントを-1する
                    BomOverlap = true;
                }
            }
        } 
    }

    private void FixedUpdate()
    {
        //移動
        float deltaX = 0.0f;
        float deltaZ = 0.0f;
        if(_controlX != 0)
        {
            deltaX += Time.deltaTime * _controlX * speed;
        }
        if (_controlZ != 0)
        {
            deltaZ += Time.deltaTime * _controlZ * speed;
        }
        transform.localPosition += new Vector3(deltaX, 0.0f, deltaZ);

        int controlAngle = ROTATION[_controlZ + 1, _controlX + 1];
        if (controlAngle >= 0)
        {
            _angle = controlAngle * 360.0f / 8.0f + 90.0f;
        }
        transform.localEulerAngles = new Vector3(0.0f, _angle, 0.0f);

        //爆風との当たり判定
        int x, z;
        BlockField.GetBomberPositon(out x, out z, transform.localPosition);
        if (GameSystem.instance.CheckExploltion(x, z))
        {
            Death = true;
            animator.SetBool("Death", true);
            bomb.FireReset();  //Bombスクリプトのメソッド実行
            speed = 1f;
            BombCount = 1;
            rigidbody.isKinematic = true;
            capsuleCollider.enabled = false;
        }
    }

    public void BombCountAdd()
    {
        BombCount += 1;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "FireUp")
        {
            //Debug.Log("FireUpを拾った");
            bomb.FireUp(); //Bombスクリプトのメソッド実行
        }

        if (other.gameObject.tag == "SpeedUp")
        {
            //Debug.Log("SpeedUpを拾った");
            if (Maxspeed >= speed)
            {
                speed += 0.5f;
            }
        }

        if (other.gameObject.tag == "BomUp")
        {
            //Debug.Log("BomUpを拾った");
            if (MaxBomCount >= BombCount)
            {
                BombCount += 1;
            }
        }
    }
}
