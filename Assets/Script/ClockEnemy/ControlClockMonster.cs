using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControlClockMonster : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public Material material;
    public Color color = new Color(1,1,1,1);
    // Start is called before the first frame update
    void Start()
    {
        CreateCubes();
    }

    // Update is called once per frame
    void Update()
    {
        if(color.g < 1f)color.g += Time.deltaTime;
        if(color.b < 1f)color.b += Time.deltaTime;
        if(color.r < 1f)color.r += Time.deltaTime;
        meshRenderer.material.color = color;
    }

    void CreateCubes(){
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        var mesh = new Mesh();

        var xCount = 10;
        var yCount = 10;

        mesh.vertices = (
            //これはLinqのクエリ構文
            from x in Enumerable.Range(0, xCount)
            from y in Enumerable.Range(0, yCount)
            from direction in new[] { Vector3.up, Vector3.right, Vector3.forward}

            from normal in new[] { direction, -direction }
            from binormal in new[] { new Vector3(normal.z, normal.x, normal.y) }
            from tangent in new[] { Vector3.Cross(normal, binormal) }
 
            from binormal2 in new[] { binormal, -binormal }
            from tangent2 in new[] { tangent, -tangent }
 
            from vec in new[] { normal + binormal2 + tangent2 }
 
            select vec
        ).ToArray();

        mesh.triangles = (
            from iterator in Enumerable.Range(0, mesh.vertices.Length / 4)
            from index in new[] {
                0, 2, 1, 1, 2, 3,
            }
            select (iterator * 4 + index) % mesh.vertices.Length
        ).ToArray();

        mesh.uv = (
            from x in Enumerable.Range(0, xCount)
            from y in Enumerable.Range(0, yCount)
            from _ in Enumerable.Range(0, 24)
            select new Vector2((float)x / xCount, (float)y / yCount)
        ).ToArray();


        //法線の再計算
         mesh.RecalculateNormals();
         mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100);

         meshFilter.mesh = mesh;
         meshRenderer.material = material;
    }
}
