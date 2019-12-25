using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSetUp : MonoBehaviour
{
    
    public RootMaster rootMasterScript;//現在使われていないが，後々プレハブを使用しない生成方法のために設置しておく．
    public GameObject bornObject;
    public GameObject targetObject;
    public GameObject tipObject;
    Transform rootTransform;
    public int boonNum;
    public List<GameObject> jointList;

    // Start is called before the first frame update
    void Start()
    {
        MakeIK();
    }

//IKの生成
    void MakeIK(){
        rootTransform = transform;
        jointList = new List<GameObject>();
        for(int i = 0; i < boonNum; i++)
        {
            jointList.Add(MakeBone(rootTransform));
        }
        jointList.Add(MakeTip(rootTransform));
    }
    

    GameObject MakeBone(Transform parentTransform)
    {
        GameObject obj = (GameObject)Instantiate(bornObject,Vector3.zero,Quaternion.Euler(Vector3.zero));
        obj.transform.parent = parentTransform;
        obj.transform.localPosition = new Vector3(0, 0, 2);
        rootTransform = obj.transform;
        return obj;
    }

    GameObject MakeTip(Transform parentTransform)
    {
        GameObject obj = (GameObject)Instantiate(tipObject);
        obj.name = "Tip";
        obj.transform.parent = parentTransform;
        obj.transform.localPosition = new Vector3(0, 0, 2);
        return obj;
    }

    void Update()
    {
        Vector3 targetPos = targetObject.transform.position;
        Vector3 tipPos = jointList[jointList.Count - 1].transform.position;
        //Debug.Log(tipPos);
        //if(Vector3.Distance(targetPos,tipPos) > 0.1)
        //{
            for(int i = 0; i < jointList.Count - 1; i++)
            {
                RootMaster script =  jointList[i].GetComponent<RootMaster>();
                script.IKRotation();
            }
        //}
        //else Debug.Log("ガッチ！");
    }
}
