using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    int selectCharaNumber;

    void Start()
    {
        selectCharaNumber = 0;
    }

    public void OnClickA()
    {
        selectCharaNumber = 0;
    }
    public void OnClickB()
    {
        selectCharaNumber = 1;
    }


    public void OnClickStart()
    {
        PlayerPrefs.SetInt("CHARA_NUMBER", selectCharaNumber);
        SceneManager.LoadScene("StageScene");
    }
}
