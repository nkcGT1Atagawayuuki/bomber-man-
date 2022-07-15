using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockField : MonoBehaviour
{
    static private BlockField _instance = null;
    public static BlockField instance { get { return _instance; } }


    public enum Block
    {
        //ブロックの順番
        None,     //0
        Floor,    //1は床
        Break,    //2は壊せるブロック
        Wall,　   //3は壁
        FireUp,   //4はパワーアップアイテム
        SpeedUp,　//5はパワーアップアイテム

        Max
    }

    [SerializeField] private GameObject[] Prefab = null;
    public int stage = 0;

    //13×11フィールド
    const int FIELD_SIZE_X = 15;
    const int FIELD_SIZE_Y = 13;

    int[,] FIELD = new int[,]
    {
        { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3, },
        { 3,4,0,0,0,0,0,0,0,0,0,0,0,5,3, },
        { 3,2,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,4,2,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3, },
    };

    GameObject[,] _valls = new GameObject[FIELD_SIZE_Y, FIELD_SIZE_X];

    static public Vector3 GetTruePositon(int x, int z)
    {
        float ofsX = -(FIELD_SIZE_X - 1) * 0.5f;
        float ofsY = -(FIELD_SIZE_Y - 1) * 0.5f;
        return new Vector3(x + ofsX, -0.0f, z + ofsY);
    }

    static public void GetBomberPositon(out int x, out int z, Vector3 truePosition)
    {
        float ofsX = -(FIELD_SIZE_X - 1) * 0.5f;
        float ofsY = -(FIELD_SIZE_Y - 1) * 0.5f;
         
        x = (int)(truePosition.x - ofsX + 0.5f);
        z = (int)(truePosition.z - ofsY + 0.5f);
    }

    public Block GetWall(int x,int z)
    {
        return (Block)FIELD[z, x];
    }


    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

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

                if (FIELD[y, x] == (int)Block.Break) 
                {
                    _valls[y, x] = newObj;
                }

                if (FIELD[y, x] == (int)Block.FireUp)
                {
                    _valls[y, x] = newObj;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReflectExplotion(int x, int z)
    {
        if(_valls[z,x] != null)
        {
            GameObject.Destroy(_valls[z, x].gameObject);
            _valls[z, x] = null;
            FIELD[z, x] = (int)Block.None;
            return true;
        }
        return false;
    }
}
