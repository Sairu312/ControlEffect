using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RotAxis
{
    X,
    Y,
    Z
}

enum RotPosition
{
    Center,
    Right,
    Left
}

enum RotDirection
{
    Forward,
    Back
}

public class RubiksMove : MonoBehaviour
{
    public GameObject[] cubes = new GameObject[27];
    private GameObject[,,] rotCubes = new GameObject[3,3,3];
    private bool test = false;
    public bool rotFlag = false;
    public bool isRotate = false;
    public float rotTime = 1;
    public bool perfect = true;
    [SerializeField]RotAxis rotationAxis = RotAxis.X; 
    [SerializeField]RotPosition rotationPosition = RotPosition.Right;
    [SerializeField]RotDirection rotationDirection = RotDirection.Forward;

    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(!test){
            test = true;
            RotCubeSelect(rotCubes,cubes);
        }
        if(isRotate)return;
        if(rotFlag)
        Preparation(rotationAxis, rotationPosition, rotationDirection);
        

    }

    void Initialize()
    {
        foreach(GameObject cube in cubes)
        {
            if(cube == null)
            {
                Debug.Log("ルービックのオブジェクトが正しくありません");
                return;
            }
        }
    }

    void Preparation(RotAxis rotAxis, RotPosition rotPosition, RotDirection rotDirection)
    {
        Vector3Int ax = Vector3Int.zero;
        int po = 0;
        int di = 0;
        switch(rotAxis){
            case RotAxis.X:
                ax =  new Vector3Int(1,0,0);
                break;
            case RotAxis.Y:
                ax = new Vector3Int(0,1,0);
                break;
            case RotAxis.Z:
                ax = new Vector3Int(0,0,1);
                break;
        }
        switch(rotPosition){
            case RotPosition.Center:
                po = 1;
                break;
            case RotPosition.Right:
                po = 2;
                break;
            case RotPosition.Left:
                po = 0;
                break;
        }
        switch(rotDirection)
        {
            case RotDirection.Back:
                di = -90;
                break;
            case RotDirection.Forward:
                di = 90;
                break;
        }
        StartCoroutine(RotRubiks(rotCubes,ax,po,di));
    }

    IEnumerator RotRubiks(GameObject[,,] rotationCubes,Vector3Int direction,int point,float angle)
    {
        if(point > 2 && point < 0)yield break;
        isRotate = true;
        float sumAngle = 0f;
        float deltaAngle = 0f;
        while(sumAngle * sumAngle < angle * angle){
            deltaAngle = angle / rotTime * Time.deltaTime;
            sumAngle += deltaAngle;
            if(sumAngle * sumAngle > angle * angle){
                deltaAngle = angle - sumAngle + deltaAngle;
                sumAngle = angle;
            }
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        rotationCubes[i * (1 - direction.x) + direction.x * point, 
                                    j * (1 - direction.y) + direction.y * point,
                                    k * (1 - direction.z) + direction.z * point]
                                    .transform.RotateAround(transform.rotation * direction + transform.position, 
                                                            transform.rotation * direction, 
                                                            deltaAngle/3f);
                    }
                }
            }
            yield return null;
        }
        isRotate = false;
        rotFlag = false;
        RotCubeSelect(rotCubes,cubes);
        yield break;
    }
    
    void RotCubeSelect(GameObject[,,] rotationCubes, GameObject[] cubeArray)
    {
        foreach(GameObject cube in cubeArray)
        {
            rotationCubes[Mathf.RoundToInt(cube.transform.localPosition.x) + 1,
                        Mathf.RoundToInt(cube.transform.localPosition.y) + 1,
                        Mathf.RoundToInt(cube.transform.localPosition.z) + 1] = cube;
        }
        CheckCube(rotationCubes,cubeArray);
    }

    void CheckCube(GameObject[,,] rotationCubes, GameObject[] cubeArray)
    {
        int i;
        int j;
        int k;
        for(int cubeNum = 0; cubeNum < cubeArray.Length; cubeNum++)
        {
            i = cubeNum % 3;
            j = (int)(cubeNum/3);
            k = j % 3;
            j = j / 3;
            if(rotationCubes[j,k,i] != cubeArray[cubeNum])
            {
                perfect = false;
                return;
            }
        }
        perfect = true;
    }
}
