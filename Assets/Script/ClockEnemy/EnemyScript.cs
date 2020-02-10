using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    public float HP = 100;
    private float MaxHP = 100;
    public GameObject player;
    public GameObject IKtarget1;
    public GameObject HPbar;
    public Vector3 waitPosition1;
    private float countTime = 0;
    private float countTime2 =0;
    private float countTime3 = 0f;

    private Vector3 originPosition;
    private Vector3 thrustPosition;
    public GameObject GameManager;
    public Light alartLight;
    public GameObject lightingManager;
    private LightingManager lightingManagerScript;

    private bool positionLockFlag = false;

    public AudioSource audioSource;
    public AudioSource SESource;
    public AudioSource BreakSource;
    public  AudioClip bomb;
    public AudioClip roll;
    public AudioClip breakBoss;
    public bool soudFlag;
    public bool tipHitFlag = false;
    public GameObject enemyModel;


    public enum EnemyStatus{
        Wait,
        Thrust,
        BackWait
    }
    public EnemyStatus nowStatus;
    EnemyStatus beforeStatus;//1フレーム前の状態

    // Start is called before the first frame update
    void Start()
    {
        EnemyInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        countTime2 += Time.deltaTime;
        positionUpdate();
        positionLockFlag = false;

        CountTimeStatus();
        HPback();
        EnemyAction();
    }

    void EnemyAction()
    {
        //状態遷移
        switch(nowStatus)
        {
            case EnemyStatus.Wait://待ち
                beforeStatus = EnemyStatus.Wait;
                Wait();
                break;
            case EnemyStatus.Thrust://突き攻撃
                beforeStatus = EnemyStatus.Thrust;
                positionLockFlag = true;
                Thrust(0.2f,2f);
                break;
            case EnemyStatus.BackWait://腕をもとに戻す
                beforeStatus = EnemyStatus.BackWait;
                positionLockFlag = true;
                BackWait(1f);
                break;
        }

        //HPが0になった時の処理
        if(HP<=0)
        {
            enemyModel.GetComponent<ControlClockMonster>().color = Color.white;
            nowStatus = EnemyStatus.Wait;
            BreakSource.enabled=true;
            audioSource.enabled = false;
            //alartLight.color = new Color(1f,1f,1f,1f);
            lightingManagerScript.alart = false;
            transform.localScale -= Vector3.one * Time.deltaTime / 2f;
            if(transform.localScale.x < 0)
            {
                Destroy(this.gameObject);
                GameManager.GetComponent<TutorialManagaer>().telopNum = 12;
            }
        }

        //敵のHPが半分を過ぎた時の処理
        else if(HP < MaxHP/2f)
        {
            audioSource.enabled = true;
            countTime3 += Time.deltaTime;
            if(transform.localScale.x < 2.5f)transform.localScale += Vector3.one * Time.deltaTime / 0.5f;
            lightingManagerScript.alart = true;
            //alartLight.color = new Color(Mathf.Sin(countTime2 * 10f)/4+0.75f,0f,0f,1f);
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    //敵の状態群

    //待ち
    void Wait()
    {
        IKtarget1.transform.position = waitPosition1 + new Vector3(2,6*Mathf.Sin(2 * countTime),0);
        //待ち状態が続いたら他の行動に移す
        if(countTime > 2f && !soudFlag)
        {
            soudFlag = true;
            SESource.volume =  1f;
            SESource.PlayOneShot(roll);
        }
        if(countTime > 2.5f)enemyModel.GetComponent<ControlClockMonster>().color = Color.blue;
        if(countTime > 3f)
        {
            soudFlag = false;
            nowStatus = EnemyStatus.Thrust;
        }
    }

    //突き(攻撃)
    void Thrust(float thrustTime,float motionTime)
    {
        if(!tipHitFlag && thrustTime - countTime > 0)
        {
            IKtarget1.transform.position = Vector3.Slerp(originPosition,thrustPosition,countTime/thrustTime);
        }
        else if(motionTime - countTime > 0f)
        {
            if(!tipHitFlag)
            {
                IKtarget1.transform.position = thrustPosition;
            }
            SESource.volume =  0.2f;
            SESource.PlayOneShot(bomb);
        }else nowStatus = EnemyStatus.BackWait;
    }

    //待ちに戻る
    void BackWait(float motionTime)
    {
        tipHitFlag = false;
        if(motionTime > countTime)
            IKtarget1.transform.position = Vector3.Slerp(thrustPosition,originPosition,countTime/motionTime);
        else nowStatus = EnemyStatus.Wait;

    }

    /////////////////////////////////////////////////////////////////////////////

    //状態遷移時のPlayerとIKの位置を保存する
    void positionUpdate()
    {
        if(!positionLockFlag)
        {
            originPosition = IKtarget1.transform.position;
            thrustPosition = player.transform.position;
        }
    }
    //その状態での時間
    void CountTimeStatus()
    {
        if(beforeStatus == nowStatus)countTime += Time.deltaTime;
        else countTime = 0f;
    }

    //スライダーにHPを送る
    void HPback()
    {
        HPbar.GetComponent<Slider>().value = HP/MaxHP;
    }

    //初期化
    void EnemyInitialize()
    {
        AudioSource[] audioS =GetComponents<AudioSource>();
        lightingManagerScript = lightingManager.GetComponent<LightingManager>();
        audioSource = audioS[0];
        SESource = audioS[1];
        BreakSource = audioS[2];
        audioSource.enabled = false;
        BreakSource.enabled = false;
        nowStatus = EnemyStatus.Wait;
        MaxHP = HP;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Rubble")
        {
            RubbleManager script = collider.GetComponent<RubbleManager>();
            if(script.rubbleStatus == RubbleManager.RubbleStatus.Shoot || script.rubbleStatus == RubbleManager.RubbleStatus.Wait){
                HP -= script.catchTime * 5f;
                enemyModel.GetComponent<ControlClockMonster>().color -= Color.cyan;
            }
        }
        
    }
}
