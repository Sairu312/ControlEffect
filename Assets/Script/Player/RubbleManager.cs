using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleManager : MonoBehaviour
{
    public bool shootFlag;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerCamera;
    public GameObject Enemy;
    public float curvature;
    private float catchTime = 0f;
    public float catchTimeSum = 2f;
    private int rubbleSetNum;
    private Vector3 rubbleSet;
    private Vector3 forward;
    public Vector3 rubbleSet1 = new Vector3(50,50,0);
    public Vector3 rubbleSet2 = new Vector3(-50,50,0);
    public Vector3 rubbleSet3 = new Vector3(0,50,50);
    public Vector3 rubbleSet4 = new Vector3(0,50,-50);
    private RaycastHit Hit;
    public bool hitFlag;
    private float persent;

    public float shootSpeed;

    private Vector3 shootPosition;
    private Vector3 hitPosition;
    private EnemyScript enemyScript;
    public GameObject tutorialManagaer;//これは不適切他のスクリプトに回す
    private TutorialManagaer tutoSctipt;//これも
    public Vector3 offset;

    //Rubble用効果音達
    public AudioSource SESource;
    public AudioClip SEcall;
    public AudioClip SEcatch;
    public AudioClip SEshot;
    private bool catchSoudFrag = false;

    //Rubbleの状態変数
    public RubbleStatus rubbleStatus;
    public GameObject enemyModel;//これも
    public enum RubbleStatus
    {
        Call,//Rubbleを呼ぶ
        Catch,//Rubbleを持つ
        Shoot,//Rubbleを投げる
        Wait//デフォルト　待機状態
    }


    // Start is called before the first frame update
    void Start()
    {
        RubbleInitialized();
    }

    // Update is called once per frame
    void Update()
    {
        ShootRubble();
    }


    void ShootRubble()
    {
        //入力
        RubbleInput();
        
        //Rubbleの状態ごとの処理
        switch(rubbleStatus)
        {
            case RubbleStatus.Wait:
                forward = CalculationRubbleForward(rubbleSetNum);
                transform.position = rubbleSet;
                catchSoudFrag = true;
                break;

            case RubbleStatus.Call:
                CatchRubble(rubbleSet,forward,catchTime / catchTimeSum);
                catchTime += Time.deltaTime /catchTimeSum;
                if(catchTime > catchTimeSum - 0.2f  && catchSoudFrag)
                {
                    //Debug.Log(catchTime/catchTimeSum);
                    SESource.PlayOneShot(SEcatch);
                    catchSoudFrag = false;
                }
                if(catchTime >= catchTimeSum)
                    rubbleStatus = RubbleStatus.Catch;
                break;

            case RubbleStatus.Catch:
                transform.position = playerCamera.transform.position + playerCamera.transform.rotation * offset;
                break;

            case RubbleStatus.Shoot:
                Ray ray = new Ray (playerCamera.transform.position,playerCamera.transform.forward);
                if(hitFlag && Physics.Raycast(ray,out Hit,200f)){
                    SESource.PlayOneShot(SEshot);
                    shootPosition = transform.position;
                    hitPosition = Hit.point;
                    persent = 0f;
                    hitFlag = false;
                }
                persent += shootSpeed * Time.deltaTime /Vector3.Magnitude(hitPosition - shootPosition);
                transform.position = Vector3.Lerp(shootPosition, hitPosition, persent);
                
                if(hitPosition == transform.position)
                {
                    
                    if(Hit.collider.tag == "Enemy")
                    {
                        enemyScript.HP -= catchTime * 10;
                        enemyModel.GetComponent<ControlClockMonster>().color -= Color.cyan;
                    }

                    if(Hit.collider.tag == "BlueCube")
                        tutoSctipt.hitBlueCube = true;
                    
                    hitFlag = true;
                    rubbleStatus = RubbleStatus.Wait;
                }
                break;
        }
    }

    //瓦礫の初期位置の前方を返す
    Vector3 CalculationRubbleForward(int i)
    {
        switch(i)
        {
            case 1:
                rubbleSet = rubbleSet1;
                return rubbleSet1 + new Vector3(-curvature,0,0);
            case 2:
                rubbleSet = rubbleSet2;
                return rubbleSet2 + new Vector3(curvature,0,0);
            case 3:
                rubbleSet = rubbleSet3;
                return rubbleSet3 + new Vector3(0,0,-curvature);
            case 4:
                rubbleSet = rubbleSet4;
                return rubbleSet4 + new Vector3(0,0,curvature);
            default:
                Ray ray = new Ray (playerCamera.transform.position,playerCamera.transform.forward);
                Physics.Raycast(ray,out Hit,100f);
                rubbleSet = Hit.point;
                return rubbleSet - playerCamera.transform.forward;       
        }
    }

    //瓦礫キャッチ
    void CatchRubble(Vector3 rubblePosition,Vector3 rubbleForward,float t)
    {
        Vector3 rubblePoint = GetPoint(rubblePosition,
                                       rubblePosition + rubbleForward,
                                       playerCamera.transform.position + curvature * playerCamera.transform.forward + playerCamera.transform.rotation * offset,
                                       playerCamera.transform.position + playerCamera.transform.rotation * offset,
                                       t);
        transform.position = rubblePoint;
    }


    //リサージュ曲線
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
    }  

    //瓦礫用インプット
    void RubbleInput()
    {
        if(XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.LB) || Input.GetKey(KeyCode.Q))
        {
            rubbleStatus = RubbleStatus.Wait;
            hitFlag = true;
            return;
        }
        bool isWait = rubbleStatus == RubbleStatus.Wait;
        //コントローラーのトリガー判定
        if((XBoxInputManager.Instance.XBoxInputAxis(XBoxAxisCode.RTrigger)　>　0.9 || Input.GetMouseButtonDown(0)) && rubbleStatus == RubbleStatus.Wait)
        {
            //if(rubbleStatus == RubbleStatus.Wait)
            rubbleStatus = RubbleStatus.Call;
            catchTime = 0f;
            rubbleSetNum = (int)Mathf.Floor(Random.value * 4 + 1);
            SESource.PlayOneShot(SEcall);
        }else if((XBoxInputManager.Instance.XBoxInputAxis(XBoxAxisCode.RTrigger) < -0.9 && !Input.GetMouseButton(0)) && rubbleStatus != RubbleStatus.Wait)
        {
            rubbleStatus = RubbleStatus.Shoot;
        }
    }

    //初期化
    void RubbleInitialized()
    {
        SESource = GetComponent<AudioSource>();
        enemyScript = Enemy.GetComponent<EnemyScript>();
        tutoSctipt = tutorialManagaer.GetComponent<TutorialManagaer>();
        rubbleStatus = RubbleStatus.Wait;
        rubbleSetNum = 1;
        hitFlag = true;
    }

}
