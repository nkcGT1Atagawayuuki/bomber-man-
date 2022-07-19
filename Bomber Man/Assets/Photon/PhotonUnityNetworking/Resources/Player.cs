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

    public float speed = 1f;       //�v���C���[�̑��x
    public int BombCount = 1;        //�v���C���[�̃{���̌�
    private float Maxspeed = 3f;   //�v���C���[�̍ő呬�x
    public int MaxBomCount = 4;      //�v���C���[�̍ő�{���̌�

    private float _angle = 0.0f;

    public bool Death = false;       //�v���C���[�����񂾂Ƃ�true�ɂ���if�̏���������
    public bool BomOverlap = false;  //�{�����d�˂Ă����Ȃ��悤�ɂ���
    public CapsuleCollider capsuleCollider;  //CapsulCollider�̎擾
    public Rigidbody rigidbody;      //Rigidbody�̎擾

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

            
            if(BombCount >= 1 && BomOverlap == false)�@                     //�{���J�E���g��1�ȏ�̎��{�����u����
            {
                if (Input.GetKeyDown(KeyCode.Space)) //�{�^������������{����z�u
                {
                    int x, z;
                    BlockField.GetBomberPositon(out x, out z, transform.localPosition);
                    GameSystem.instance.SetBomb(x, z);
                    BombCount -= 1;                  //�{���J�E���g��-1����
                    BomOverlap = true;
                }
            }
        } 
    }

    private void FixedUpdate()
    {
        //�ړ�
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

        //�����Ƃ̓����蔻��
        int x, z;
        BlockField.GetBomberPositon(out x, out z, transform.localPosition);
        if (GameSystem.instance.CheckExploltion(x, z))
        {
            Death = true;
            animator.SetBool("Death", true);
            bomb.FireReset();  //Bomb�X�N���v�g�̃��\�b�h���s
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
            //Debug.Log("FireUp���E����");
            bomb.FireUp(); //Bomb�X�N���v�g�̃��\�b�h���s
        }

        if (other.gameObject.tag == "SpeedUp")
        {
            //Debug.Log("SpeedUp���E����");
            if (Maxspeed >= speed)
            {
                speed += 0.5f;
            }
        }

        if (other.gameObject.tag == "BomUp")
        {
            //Debug.Log("BomUp���E����");
            if (MaxBomCount >= BombCount)
            {
                BombCount += 1;
            }
        }
    }
}
