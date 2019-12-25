using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
    private int rubbleSetNum;
    private Vector3 rubbleSet;
    private Vector3 forward;
    public Vector3 rubbleSet1 = new Vector3(50,50,0);
    public Vector3 rubbleSet2 = new Vector3(-50,50,0);
    public Vector3 rubbleSet3 = new Vector3(0,50,50);
    public Vector3 rubbleSet4 = new Vector3(0,50,-50);
    private RaycastHit Hit;
    public bool hitFlag;
    public bool shotFlag;
    private float persent;

    public float shootSpeed;

    private Vector3 shootPosition;
    private Vector3 hitPosition;
    private EnemyScript enemyScript;
    public GameObject tutorialManagaer;
    private TutorialManagaer tutoSctipt;
    public Vector3 offset;

    public AudioSource SESource;
    public AudioClip SEcall;
    public AudioClip SEcatch;
    public AudioClip SEshot;
    private bool soudFrag;



    // Start is called before the first frame update
    void Start()
    {
        SESource = GetComponent<AudioSource>();
        enemyScript = Enemy.GetComponent<EnemyScript>();
        tutoSctipt = tutorialManagaer.GetComponent<TutorialManagaer>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootRubble();
    }


    void ShootRubble()
    {
        //コントローラーのトリガー判定
        if((Input.GetAxis("R_Trigger")>0.9 || Input.GetMouseButton(0)) && !shootFlag)
        {
            shootFlag = true;
            shotFlag = false;
            hitFlag = false;
            catchTime = 0f;
            rubbleSetNum = (int)Mathf.Floor(Random.value * 4 + 1);
            SESource.PlayOneShot(SEcall);
        }else if(Input.GetAxis("R_Trigger") < -0.9 || Input.GetMouseButtonUp(0))
        {
            //SESource.PlayOneShot(SEshot);
            shootFlag = false;
        }

        //キャッチ処理
        if(shootFlag)
        {
            if(catchTime < 1)
            {

                forward = CalculationRubbleForward(rubbleSetNum);
                CatchRubble(rubbleSet,forward,catchTime);
                catchTime += 0.5f * Time.deltaTime;
            }else
            {
                if(!soudFrag)
                {
                    SESource.PlayOneShot(SEcatch);
                    soudFrag = true;
                }
                transform.position = player.transform.position + offset;
            }
        }
        //シュート処理
        else if(!shootFlag && !shotFlag)
        {
            Ray ray = new Ray (playerCamera.transform.position,playerCamera.transform.forward);
            if(!hitFlag && Physics.Raycast(ray,out Hit,100f)){
                //Physics.Raycast (ray,out Hit,100);
                SESource.PlayOneShot(SEshot);
                shootPosition = transform.position + offset;
                hitPosition = Hit.point;
                persent = 0f;
                hitFlag = true;
            }
            persent += shootSpeed/(Time.deltaTime *Vector3.Magnitude(hitPosition-shootPosition));
            transform.position = Vector3.Lerp(shootPosition, hitPosition, persent);
            //Debug.Log (Hit.point);//デバッグログにヒットした場所を出す
            //Debug.Log(persent);
            if(hitPosition == transform.position)
            {
                    shotFlag = true;
                    if(Hit.collider.tag == "Enemy")
                    enemyScript.HP -= catchTime * 10;
                    if(Hit.collider.tag == "BlueCube")
                    tutoSctipt.hitBlueCube = true;

            }
        }
        if(shotFlag)
        {
            transform.position = rubbleSet1;
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
                                       player.transform.position + curvature * player.transform.forward + offset,
                                       player.transform.position + offset,
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

}
