using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public bool Stop = false;
    public bool Death = true;

    public GameObject bombPrefab;

    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!Death)
        {
            return;
        }

        UpdatePlayerMovement();
    }

    private void UpdatePlayerMovement()
    {
        Stop = false;

        if(Stop == false)
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        { //Up movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        { //Left movement
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        { //Down movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walk", true);
            Stop = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
    }

    private void DropBomb ()
    {
        var pos = new Vector3(Mathf.RoundToInt(myTransform.position.x), bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z));

        if (bombPrefab)
        {
            Instantiate(bombPrefab, pos, bombPrefab.transform.rotation);
        }
    }
}
