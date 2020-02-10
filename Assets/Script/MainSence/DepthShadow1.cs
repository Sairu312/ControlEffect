using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthShadow1 : MonoBehaviour
{
    [SerializeField]
    Material m_mat;

    void OnWillRenderObject()
    {
        Camera cam = Camera.current;
        if(cam.name == "ShadowCamera")
        {
            //ライトからのカメラ行列
            Matrix4x4 lightVMatrix = cam.worldToCameraMatrix;
            Matrix4x4 lightPMatrix = GL.GetGPUProjectionMatrix(cam.projectionMatrix, false);
            Matrix4x4 lightVP = lightPMatrix * lightVMatrix;
        }
    }
}
