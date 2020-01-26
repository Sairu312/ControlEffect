///////////////////////////////////
//ゲームマネージャー　ゲーム全体を通してゲームを管理する
//
//Inukai Satoru

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;//カーソル
        DontDestroyOnLoad(this);
    }
    void Update () {
      if (Input.GetKey(KeyCode.Escape)) Quit();
    }

    //escで終了
    void Quit() 
    {
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
      #endif
    } 
}
