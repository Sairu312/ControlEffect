using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Bezier {
    [Header("端点")]
    public Transform endPoint;
    [Header("制御点1")]
    public Transform controlPoint1;
    [Header("制御点2")]
    public Transform controlPoint2;
}


public class BezieToolScript : MonoBehaviour
{
    public GameObject moveObject;
    private float countTime;
    private float oneBezierMoveTime = 1f;//一つのベジェ曲線を移動するのにかかる時間
    private int bezierNum = 0;
    public bool flag = false;//移動開始のフラグ
    public float moveTime;//すべてのベジェ曲線を移動するのにかかる時間

    public bool drawGizmo = true;//Gizmosをびょうがするか？
    public Bezier[] bezier = Array.Empty<Bezier>();
    

    // Start is called before the first frame update
    void Start()
    {
        if(bezier.Length < 2)Debug.Log("不正な値"+this.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            BezierMove(bezier);
            countTime += Time.deltaTime;
        }
    }

    /*
    インスペクター上での設定を簡略化するため，
    このスクリプトの子オブジェクトを参照して
    自動的に端点と制御点を配列に格納する
    */
    private void Reset(){
        moveTime = 1f;
        if(transform.childCount < 2)return;
        Array.Resize(ref bezier, transform.childCount);
        for(int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(i);
            bezier[i].endPoint = transform.GetChild(i).transform;
            bezier[i].controlPoint1 = transform.GetChild(i).transform.GetChild(0).transform;
            bezier[i].controlPoint2 = transform.GetChild(i).transform.GetChild(1).transform;
        }
    }

    //ベジェ曲線に合わせてmoveObjectを動かす
    void BezierMove(Bezier[] bz)
    {
        oneBezierMoveTime = moveTime / (bz.Length - 1);
        bezierNum = (int)(countTime / oneBezierMoveTime);
        if(countTime >= moveTime)
        {
            countTime = 0;
            flag = false;
            return;
        }
        moveObject.transform.position = 
        GetBezierPoint(bz[bezierNum].endPoint.position,
                        bz[bezierNum].controlPoint2.position,
                        bz[bezierNum + 1].controlPoint1.position,
                        bz[bezierNum + 1].endPoint.position,
                        countTime / oneBezierMoveTime - bezierNum);                     
    }



    ////////////////////////////////////////////////////////////
    //Gizmos
    void OnDrawGizmos()
    {
        if(drawGizmo)
        BezierGizmos(bezier);
    }

    //一つのベジェ曲線をGizmosで描画する
    void BezierGizmos(Bezier[] bezier)
    {
        foreach(Bezier bz in bezier){
            DrawControlPoint(bz,0.3f);
            DrawEndPoint(bz,0.5f);
            DrawControlLine(bz);
        }
        for(int i = 0; i < bezier.Length - 1; i++)
        {
            DrawBezierLine(bezier[i],bezier[i+1]);
        }
    }

    //端点と制御点を結ぶ線
    void DrawControlLine(Bezier bezier)
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(bezier.endPoint.position,bezier.controlPoint1.position);
        Gizmos.DrawLine(bezier.endPoint.position,bezier.controlPoint2.position);
    }


    //制御点の描画
    void DrawControlPoint(Bezier bezier, float size)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(bezier.controlPoint1.position,size);
        Gizmos.DrawSphere(bezier.controlPoint2.position,size);
    }
   

    //端点の描画
    void DrawEndPoint(Bezier bezier,float size)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(bezier.endPoint.position,new Vector3(size,size,size));
    }

    //ベジェ曲線の描画
    void DrawBezierLine(Bezier bezier0, Bezier bezier1)
    {
        float nowPoint = 0f;
        for(float i = 0f; i < 1f; i+=0.01f)
        {
            Gizmos.DrawLine(
                GetBezierPoint(bezier0.endPoint.position,
                                bezier0.controlPoint2.position,
                                bezier1.controlPoint1.position,
                                bezier1.endPoint.position,
                                nowPoint),
                GetBezierPoint(bezier0.endPoint.position,
                                bezier0.controlPoint2.position,
                                bezier1.controlPoint1.position,
                                bezier1.endPoint.position,
                                i));
            nowPoint = i;
        }
    }

    //////////////////////////////////////////////////////////////
    //ベジェ曲線計算関数
    Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
    }

}
