using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOriginScript : MonoBehaviour
{
    public float mouseAimSpeedH = 5;
    public float mouseAimSpeedV = 5;
    public float aimSpeedH = 5;
    public float aimSpeedV = 5;
    private float rotValueH = 0;
    private float rotValueV = 0;
    public float rotLimitMaxV = 54;
    public float rotLimitMinV = -54;


    // Update is called once per frame
    void Update()
    {
        CameraInput();
    }

    void CameraInput()
    {
        rotValueH += Input.GetAxis("Mouse X") * mouseAimSpeedH;
        rotValueV -= Input.GetAxis("Mouse Y") * mouseAimSpeedV;
        rotValueH += Input.GetAxis("R_Stick_H") * aimSpeedH;
        rotValueV -= Input.GetAxis("R_Stick_V") * aimSpeedV;
        if(rotValueV > rotLimitMaxV)rotValueV = rotLimitMaxV;
        if(rotValueV < rotLimitMinV)rotValueV = rotLimitMinV;
        transform.rotation = Quaternion.Euler(rotValueV,rotValueH,0);
    }
}
