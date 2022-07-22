using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem1 : MonoBehaviour
{
    //　スタートボタンを押したら実行する
    public void titleGame()
    {
        SceneManager.LoadScene("Title");
    }
    public void RetryGame()
    {
        SceneManager.LoadScene("Stage0");
    }

    public void SelectGame()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		Application.OpenURL("http://www.yahoo.co.jp/");
#else
		Application.Quit();
#endif
    }
}
