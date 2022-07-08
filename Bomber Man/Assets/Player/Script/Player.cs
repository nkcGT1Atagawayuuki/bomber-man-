using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private int _controlX = 0;
    private int _controlZ = 0;

    private float _angle = 0.0f;

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
        

        float forward = 0.0f;

        _controlX = 0;
        _controlZ = 0;
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
    }

    private void FixedUpdate()
    {
        //ˆÚ“®
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
}
