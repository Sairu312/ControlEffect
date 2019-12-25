using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////
//BGMを流すAudioSourceがシーンをまたいでも壊れないようにする
//これによってゲームを通してBGMが流せる
//
//Inukai Satoru
//12/24


public class BGM : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
