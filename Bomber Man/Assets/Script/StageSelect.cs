using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("stage0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
