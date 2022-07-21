using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed = 0f;
    public bool Death = true;
    public bool StopPlayer = false;

    private Rigidbody rigidbody;
    private Transform PlayerTransform;
    private Animator animator;

    public GameObject Bomb;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PlayerTransform = transform;
        
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
        StopPlayer = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, Speed);
            PlayerTransform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walk", true);
            StopPlayer = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.velocity = new Vector3(-Speed, rigidbody.velocity.y, rigidbody.velocity.z);
            PlayerTransform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walk", true);
            StopPlayer = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, -Speed);
            PlayerTransform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walk", true);
            StopPlayer = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.velocity = new Vector3(Speed, rigidbody.velocity.y, rigidbody.velocity.z);
            PlayerTransform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walk", true);
            StopPlayer = true;
        }

        if (StopPlayer == false)
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetKeyDown (KeyCode.Space)) 
        { 
            SetBomb();
        }
    }

    private void SetBomb()
    {
        Instantiate(Bomb, new Vector3(Mathf.RoundToInt(transform.position.x),
        Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)),
        Bomb.transform.rotation);
    }
}
