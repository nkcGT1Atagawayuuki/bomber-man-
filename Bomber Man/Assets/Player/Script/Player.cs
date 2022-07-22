using UnityEngine;
using System.Collections;
using System;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPunCallbacks
{
    public float Speed = 3f;
    public int BombCount = 1;
    public float DeleteTime = 0f;

    int MaxSpeed = 7;
    int MaxBombCount = 5;

    public bool Stop = false;
    public bool Death = false;
    public bool BombOverlap = false;

    public GameObject bombPrefab;

    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;
    public CapsuleCollider capsuleCollider;
    public Bomb bomb;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!Death)
        {
            animator.SetBool("Death", true);
            bomb.FireReset();
            Speed = 3;
            BombCount = 1;
            DeleteTime += Time.deltaTime;
            if (DeleteTime > 3)
            {
                Debug.Log("時間が経過した");
                Application.Quit();
            }
            return;
        }

        //自分が生成したキャラなら操作できる
        if (photonView.IsMine)
        {
            UpdatePlayerMovement();
        }
    }

    private void UpdatePlayerMovement()
    {
        Stop = false;

        if(Stop == false)
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        { 
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, Speed);
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        { 
            rigidBody.velocity = new Vector3(-Speed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        { 
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -Speed);
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.velocity = new Vector3(Speed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (BombCount >= 1 && BombOverlap == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //DropBomb();
                photonView.RPC(nameof(DropBomb), RpcTarget.All);
                BombOverlap = true;
                BombCount -= 1;
            }
        }
    }

    [PunRPC]
    private void DropBomb ()
    {
        var pos = new Vector3(Mathf.RoundToInt(myTransform.position.x), bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z));

        if (bombPrefab)
        {
            Instantiate(bombPrefab, pos, bombPrefab.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            Death = false;
            capsuleCollider.isTrigger = true;
        }

        if (other.gameObject.tag == "FireUp")
        {
            bomb.FireUp();
        }

        if (other.gameObject.tag == "SpeedUp")
        {

            if (MaxSpeed >= Speed)
            {
                Speed += 0.5f;
            }
        }

        if (other.gameObject.tag == "BomUp")
        {

            if (MaxBombCount >= BombCount)
            {
                BombCount += 1;
            }
        }
    }
}
