using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockField : MonoBehaviour
{
    static private BlockField _instance = null;
    public static BlockField instance { get { return _instance; } }

    public enum Block
    {
        //�u���b�N�̏���
        None,     //0
        Floor,    //1�͏�
        Break,    //2�͉󂹂�u���b�N
        Wall,�@   //3�͕�
        bcup,     //4�`7�̓x���g�R���x�A
        bcdown,   //
        bcleft,   //
        bcright,  //

        Max
    }


    [SerializeField] private GameObject[] Prefab = null;
    public int stage = 0;

    //13�~11�t�B�[���h
    const int FIELD_SIZE_X = 21;
    const int FIELD_SIZE_Y = 13;

    int[,] FIELD = new int[,]
    {
        { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3, },
        { 3,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,2,3,2,3,2,3,2,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,2,3,2,3,2,3,2,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3,0,3, },
        { 3,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3, },
        { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3, },
    };

    const int Floor_SIZE_X = 21;
    const int Floor_SIZE_Y = 13;

    //��
    int[,] Floor = new int[,]
   {
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
   };

    GameObject[,] _walls = new GameObject[FIELD_SIZE_Y, FIELD_SIZE_X];

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

        //�t�B�[���h�u���b�N�𐶐�
        float ofsX = -(FIELD_SIZE_X - 1) * 0.5f;
        float ofsY = -(FIELD_SIZE_Y - 1) * 0.5f;
        for (int y = 0; y < FIELD_SIZE_Y; y++)
        {
            for (int x = 0; x < FIELD_SIZE_X; x++)
            {
                //GameObject FloorObj = Instantiate<GameObject>(Prefab[(int)Block.Floor]);
                //Floor.transform.localPosition = new Vector3(x + ofsX, -0.5f, y + ofsY);
                int none = FIELD[y, x];
                if (none == 0)
                {
                    continue;
                }
                GameObject newObj = Instantiate<GameObject>(Prefab[FIELD[y, x]]);
                newObj.transform.localPosition = new Vector3(x + ofsX, 0.5f, y + ofsY);

                if (FIELD[y, x] == (int)Block.Break)
                {
                    _walls[y, x] = newObj;
                }
            }
        }

        float floX = -(Floor_SIZE_X - 1) * 0.5f;
        float floY = -(Floor_SIZE_Y - 1) * 0.5f;
        for (int y = 0; y < Floor_SIZE_Y; y++)
        {
            for (int x = 0; x < Floor_SIZE_X; x++)
            {
                GameObject newObj = Instantiate<GameObject>(Prefab[Floor[y, x]]);
                newObj.transform.localPosition = new Vector3(x + floX, -0.5f, y + floY);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReflectExplotion(int x, int z)
    {
        if(_walls[z,x] != null)
        {
            GameObject.Destroy(_walls[z, x].gameObject);
            _walls[z, x] = null;
            FIELD[z, x] = (int)Block.None;
            return true;
        }
        return false;
    }
}