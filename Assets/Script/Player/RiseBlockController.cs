using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseBlockController : MonoBehaviour
{
    public LayerMask layerMask;
    private bool inputFlag = false;
    private GameObject camObj;
    private Camera cam;

    private RaycastHit Hit;
    private RaycastHit Hit2;
    private RaycastHit Hit3;
    public RiseBlock[] blocks = new RiseBlock[4];
    private int blockNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        camObj = Camera.main.gameObject;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RiseBlockInput();
    }

    //入力
    void RiseBlockInput()
    {
        if(XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.RB) && inputFlag == false)
        {
            inputFlag = true;
        }
        else if(!XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.RB) && inputFlag == true)
        {
            inputFlag = false;
            RisingBlock();
        }
    }

    //隆起する
    void RisingBlock() 
    {

        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
        Ray ray2 = cam.ScreenPointToRay(new Vector2(Screen.width/2 + 10,Screen.height/2));
        Ray ray3 = cam.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2 + 10));
        if(Physics.Raycast(ray,out Hit, 200,layerMask) && Physics.Raycast(ray2,out Hit2, 200, layerMask) && Physics.Raycast(ray3,out Hit3, 200,layerMask))
        {
            blocks[blockNum].RisingSet(Hit.point, Hit2.point, Hit3.point);
            ++blockNum;
            if(blockNum > 3)blockNum = 0;
        }

    }
}
