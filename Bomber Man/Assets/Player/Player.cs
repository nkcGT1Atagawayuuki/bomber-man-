using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int SpeedX = 0;
    private int SpeedY = 0;

    private float angle = 0.0f;

    static readonly int[,] ROTATION = new int[,]
    {
        {3,2,1},
        {4,-1,0},
        {5,6,7},
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedX = 0;
        SpeedY = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            SpeedY++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            SpeedY--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SpeedX++;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SpeedX--;
        }
    }

    private void FixedUpdate()
    {
        //ˆÚ“®
        float deltaX = 0.0f;
        float deltaY = 0.0f;
        if(SpeedX != 0)
        {
            deltaX += Time.deltaTime * SpeedX;
        }
        if (SpeedY!= 0)
        {
            deltaY += Time.deltaTime * SpeedY;
        }
        transform.localPosition += new Vector3(deltaX, 0.0f, deltaY);

        int SpeedAngle = ROTATION[SpeedY + 1, SpeedX + 1];
        if (SpeedAngle >= 0)
        {
            angle = SpeedAngle * 360.0f / 8.0f + 90.0f;
        }
        transform.localEulerAngles = new Vector3(0.0f, angle, 0.0f);
    }
}
