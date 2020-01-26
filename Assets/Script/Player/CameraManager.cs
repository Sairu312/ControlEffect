using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
create by inukai satoru
カメラの初期設定とプレイヤーに向く処理を行う
*/
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position + new Vector3(0,0,-radius);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + new Vector3(0,5,5);
        transform.LookAt(player.transform);
    }
}
