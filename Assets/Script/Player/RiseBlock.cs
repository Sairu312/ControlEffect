using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseBlock : MonoBehaviour
{
    private Vector3 normal;
    public bool riseFlag;
    private float time = 0;
    public float riseTime;
    private Vector3 startPos = Vector3.zero;
    public float riseDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rising();
    }

    public void RisingSet(Vector3 pos, Vector3 pos2, Vector3 pos3)
    {
        startPos = pos;
        Vector3 a = pos2 - startPos;
        Vector3 b = pos3 - startPos;
        normal = new Vector3(a.y * b.z - a.z * b.y,
                            a.z * b.x - a.x * a.z,
                            a.x * b.y - a.y * b.x).normalized;
        transform.position = startPos;
        transform.LookAt(startPos + normal);
        if(normal.y * normal.y > 0.1)
        {
        float angle = Mathf.Atan(a.z/a.x) * 180 / Mathf.PI;
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                            transform.rotation.eulerAngles.y,
                                            angle);
        Debug.Log(angle);
        }
        riseFlag = true;
    }

    void Rising()
    {
        if(!riseFlag)return;
        transform.position = Vector3.Lerp(startPos, startPos - normal.normalized * riseDistance,time/riseTime);
        time += 1f;
        if(time < riseTime)return;
        time = 0f;
        riseFlag = false;      
    }

}
