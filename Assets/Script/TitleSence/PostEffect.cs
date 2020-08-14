﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffect : MonoBehaviour
{
    [SerializeField]
    private Material material;

 
    [SerializeField]
    [Range(0, 1)]
    float rgbNoise;
    public float RGBNoise { get { return rgbNoise; } set { rgbNoise = value; } }
 
    [SerializeField]
    [Range(0, 2)]
    float scanLineTail = 1.5f;
    public float ScanLineTail { get { return scanLineTail; } set { scanLineTail = value; } }
 
    [SerializeField]
    [Range(-10, 10)]
    float scanLineSpeed = 10;
    public float ScanLineSpeed { get { return scanLineSpeed; } set { scanLineSpeed = value; } }


    [SerializeField]
    [Range(0,0.1f)]
    float greenOffset = 0.01f;
    public float GreenOffset { get {return greenOffset;} set { greenOffset = value;}}
 
    [SerializeField]
    [Range(0,0.1f)]
    float blueOffset = 0.02f;
    public float BlueOffset { get {return blueOffset;} set { blueOffset = value;}}
 


   //レンダリングが完了したタイミングでOnRenderImageが呼ばれる
   //sourceはレンダリング結果，destは編集後の画像を入れる
    private void OnRenderImage(RenderTexture source, RenderTexture dest){
        material.SetFloat("_RGBNoise", rgbNoise);
        material.SetFloat("_ScanLineSpeed", scanLineSpeed);
        material.SetFloat("_ScanLineTail", scanLineTail);
        material.SetFloat("_GreenOffset",greenOffset);
        material.SetFloat("_BlueOffset",blueOffset);
        Graphics.Blit(source, dest, material);//1を3を適用して2にコピー
   }
}
