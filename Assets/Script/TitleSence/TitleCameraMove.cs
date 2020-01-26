using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraMove : MonoBehaviour
{
    public float Speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0,Speed * Time.deltaTime,0);
    }
}
