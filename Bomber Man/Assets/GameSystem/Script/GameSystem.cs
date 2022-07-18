using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab = null;       //���e
    [SerializeField] private GameObject _explosionPrefab = null;  //����

    List<Bomb> _bombLisst = new List<Bomb>();

    //����
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
        //Debug.Log("����");

        //�����̐���
        SpawnExplotion(x, z);//�����������̉�
        for (int dz = 1; dz <= power; dz++)  //�l�����ɔ������o��
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

    //�����̔���
    private bool SpawnExplotion(int x, int z)
    {
        BlockField.Block block = BlockField.instance.GetWall(x, z);//���W���Ƃ��Ă�
        if (block == BlockField.Block.Break || block == BlockField.Block.Wall)
        {
            if (block == BlockField.Block.Break)
            {
                int ItemCount = Random.Range(0, 4);//�����_��
                GameObject obj = Instantiate(_explosionPrefab);
                obj.transform.localPosition = BlockField.GetTruePositon(x, z);

                GameObject obj2 = Instantiate(Item[ItemCount]);//�A�C�e������
                obj2.transform.localPosition = BlockField.GetTruePositon(x, z);

                obj.GetComponent<Explotion>().Initialize(x, z);
                _explotionList.Add(obj.GetComponent<Explotion>());

                

                //���e�̋N���𑁂߂�
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

            //���e�̋N���𑁂߂�
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
