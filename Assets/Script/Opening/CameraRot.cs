using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    public Camera cam;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        cam.transform.LookAt(obj.transform);
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.LookAt(obj.transform);
    }
}
