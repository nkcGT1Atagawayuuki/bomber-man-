using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab = null;       //”š’e
    [SerializeField] private GameObject _explosionPrefab = null;  //”š•—

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
        Debug.Log("”š”­");

        //”š•—‚Ì¶¬
        for (int dz = 0; dz <= power; dz++) 
        {
            SpawnExplotion(x, z + dz);
        } 

        return true;
    }

    private void SpawnExplotion(int x, int z)
    {
        GameObject obj = Instantiate(_explosionPrefab);
        obj.transform.localPosition = BlockField.GetTruePositon(x, z);
    }
}
