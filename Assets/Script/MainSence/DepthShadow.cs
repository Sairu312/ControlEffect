using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DepthShadow : MonoBehaviour
{
    public Material material;
    void Start()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
    }

    // Update is called once per frame
    public void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, material);
    }
}
