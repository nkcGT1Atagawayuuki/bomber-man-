using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockField2 : MonoBehaviour
{
    enum Block
    {
        //ブロックの順番
        None,  //0
        Floor, //1は床
        Break, //2は壊せるブロック
        Wall,　//3は壁
        Beruto,//4はベルトコンベアー

        Max
    }

    [SerializeField] private GameObject[] Prefab = null;

    //13×11フィールド
    const int FIELD_SIZE_X = 15;
    const int FIELD_SIZE_Y = 13;

    static readonly int[,] FIELD = new int[,]
    {
        { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,4,4,4,4,4,4,4,4,4,0,0,3, },
        { 3,0,3,4,3,0,3,0,3,0,3,4,3,0,3, },
        { 3,0,0,4,0,0,0,0,0,0,0,4,0,0,3, },
        { 3,0,3,4,3,0,3,0,3,0,3,4,3,0,3, },
        { 3,0,0,4,0,0,0,0,0,0,0,4,0,0,3, },
        { 3,0,3,4,3,0,3,0,3,0,3,4,3,0,3, },
        { 3,0,0,4,4,4,4,4,4,4,4,4,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3, },
    };

    // Start is called before the first frame update
    void Start()
    {
        //フィールドブロックを生成
        float ofsX = -(FIELD_SIZE_X - 1) * 0.5f;
        float ofsY = -(FIELD_SIZE_Y - 1) * 0.5f;
        for (int y = 0; y < FIELD_SIZE_Y; y++)
        {
            for (int x = 0; x < FIELD_SIZE_X; x++)
            {
                GameObject FloorObj = Instantiate<GameObject>(Prefab[(int)Block.Floor]);
                FloorObj.transform.localPosition = new Vector3(x + ofsX, -0.5f, y + ofsY);
                int none = FIELD[y, x];
                if (none == 0)
                {
                    continue;
                }
                GameObject newObj = Instantiate<GameObject>(Prefab[FIELD[y, x]]);
                newObj.transform.localPosition = new Vector3(x + ofsX, 0.5f, y + ofsY);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
