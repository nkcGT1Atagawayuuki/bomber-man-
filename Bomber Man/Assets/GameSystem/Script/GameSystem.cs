using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab = null;       //爆弾
    [SerializeField] private GameObject _explosionPrefab = null;  //爆風

    List<Bomb> _bombLisst = new List<Bomb>();

    //爆風
    List<Explotion> _explotionList = new List<Explotion>();

    public GameObject[] Item;
   
    static private GameSystem _instance = null;
    public static GameSystem instance { get { return _instance; } }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SetBomb(int x, int z)
    {
        GameObject obj = Instantiate(_bombPrefab);
        obj.transform.localPosition = BlockField.GetTruePositon(x, z);
        obj.GetComponent<Bomb>().Initialize(x, z);

        _bombLisst.Add(obj.GetComponent<Bomb>());

        return true;
    }

    public bool Explode(int x, int z, int power)
    {
        //Debug.Log("爆発");

        //爆風の生成
        SpawnExplotion(x, z);//ここが中央の炎
        for (int dz = 1; dz <= power; dz++)  //四方向に爆風を出す
        {
            
            if(SpawnExplotion(x, z + dz) == false)
            {
                break;
            }
        }
        for (int dz = 1; dz <= power; dz++)
        {
            if (SpawnExplotion(x, z - dz) == false)
            {
                break;
            }
        }
        for (int dx = 1; dx <= power; dx++)
        {
            if(SpawnExplotion(x + dx, z) == false)
            {
                break;
            }          
        }
        for (int dx = 1; dx <= power; dx++)
        {
            if(SpawnExplotion(x - dx, z) == false)
            {
                break;
            }
        }

        return true;
    }

    //爆風の発生
    private bool SpawnExplotion(int x, int z)
    {
        BlockField.Block block = BlockField.instance.GetWall(x, z);//座標をとってる
        if (block == BlockField.Block.Break || block == BlockField.Block.Wall)
        {
            if (block == BlockField.Block.Break)
            {
                int ItemCount = Random.Range(0, 4);//ランダム
                GameObject obj = Instantiate(_explosionPrefab);
                obj.transform.localPosition = BlockField.GetTruePositon(x, z);

                GameObject obj2 = Instantiate(Item[ItemCount]);//アイテム生成
                obj2.transform.localPosition = BlockField.GetTruePositon(x, z);

                obj.GetComponent<Explotion>().Initialize(x, z);
                _explotionList.Add(obj.GetComponent<Explotion>());

                

                //爆弾の起爆を早める
                ChainBomb(x, z);

                BlockField.instance.ReflectExplotion(x, z);
            }

            return false;
        }
        
        {
            GameObject obj = Instantiate(_explosionPrefab);
            obj.transform.localPosition = BlockField.GetTruePositon(x, z);
            obj.GetComponent<Explotion>().Initialize(x, z);
            _explotionList.Add(obj.GetComponent<Explotion>());

            //爆弾の起爆を早める
            ChainBomb(x, z);
        }
       
        return true;
    }

    private bool ChainBomb(int x, int z)
    {
        int n = _bombLisst.Count;
        for (int i = 0; i < n; i++) 
        {
            if(_bombLisst[i].x == x&& _bombLisst[i].z == z)
            {
                _bombLisst[i].Chain();
            }
        }
        return true;
    }

    public void UnregisterBomb(Bomb ex)
    {
        int index = _bombLisst.IndexOf(ex);
        if (index >= 0)
        {
            _bombLisst.RemoveAt(index);
        }
    }

    public void UnregisterExplotions(Explotion ex)
    {
        int index = _explotionList.IndexOf(ex);
        if(index >= 0)
        {
            _explotionList.RemoveAt(index);
        }
    }

    public bool CheckExploltion(int x, int z)
    {
        int n = _explotionList.Count;
        for(int i =0; i<n; i++)
        {
            if (_explotionList[i].x == x && _explotionList[i].z == z)
            {
                return true;
            }
        }
        return false;
    }
}
