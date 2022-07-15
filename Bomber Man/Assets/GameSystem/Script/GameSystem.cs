using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab = null;       //îöíe
    [SerializeField] private GameObject _explosionPrefab = null;  //îöïó

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

        return true;
    }

    public bool Explode(int x, int z, int power)
    {
        Debug.Log("îöî≠");

        //îöïóÇÃê∂ê¨
        SpawnExplotion(x, z);
        for (int dz = 1; dz <= power; dz++) 
        {
            if(SpawnExplotion(x, z + dz) == false)
            {
                break;
            }

            if (PowerUpExplotion(x, z + dz) == false) 
            {
                
            }
        }
        for (int dz = 1; dz <= power; dz++)
        {
            if (SpawnExplotion(x, z - dz) == false)
            {
                break;
            }

            if (PowerUpExplotion(x, z - dz) == false)
            {
                
            }

        }
        for (int dx = 1; dx <= power; dx++)
        {
            if(SpawnExplotion(x + dx, z) == false)
            {
                break;
            }

            if (PowerUpExplotion(x + dx, z) == false)
            {
                
            }
        }
        for (int dx = 1; dx <= power; dx++)
        {
            if(SpawnExplotion(x - dx, z) == false)
            {
                break;
            }

            if (PowerUpExplotion(x - dx, z) == false)
            {
            
            }
        }

        return true;
    }

    private bool SpawnExplotion(int x, int z)
    {
        BlockField.Block block = BlockField.instance.GetWall(x, z);
        if (block == BlockField.Block.Break || block == BlockField.Block.Wall)
        {
            if (block == BlockField.Block.Break)
            {
                GameObject obj = Instantiate(_explosionPrefab);
                obj.transform.localPosition = BlockField.GetTruePositon(x, z);

                BlockField.instance.ReflectExplotion(x, z);
            }

            return false;
        }
        
        {
            GameObject obj = Instantiate(_explosionPrefab);
            obj.transform.localPosition = BlockField.GetTruePositon(x, z);
        }
       
        return true;
    }

    private bool PowerUpExplotion(int x, int z)
    {
        BlockField.Block block = BlockField.instance.GetWall(x, z);
        if (block == BlockField.Block.FireUp || block == BlockField.Block.Wall)
        {
            if (block == BlockField.Block.FireUp)
            {
                GameObject obj = Instantiate(_explosionPrefab);
                obj.transform.localPosition = BlockField.GetTruePositon(x, z);

                BlockField.instance.ReflectExplotion(x, z);
            }

            return false;
        }

        {
            GameObject obj = Instantiate(_explosionPrefab);
            obj.transform.localPosition = BlockField.GetTruePositon(x, z);
        }

        return true;
    }
}
