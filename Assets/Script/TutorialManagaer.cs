using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManagaer : MonoBehaviour
{
    public bool tutorial = false;
    public GameObject player;
    public GameObject enemy;
    public GameObject bossHpSlider;
    public Text telop;
    public Text subTelop;
    public GameObject tutoCamera;
    public GameObject wall;
    private DissolveManager dissolve;

    private float CountTime = 0f;
    public int telopNum = 0;
    private bool telopChange = false;
    private RaycastHit Hit;
    public GameObject blueCube;
    private bool first = false;
    public bool hitBlueCube = false;
    public Image panel;
    private bool cameGame = true;


    public Vector3 tutorialPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        tutorial = false;
        if(TextManager.GetSelect() == 1)tutorial = true;
        if(tutorial)
        {
            SetUpTutorial();
        }else telopNum = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hitBlueCube);
        if(cameGame)
        {   
            panel.color = new Color(0,0,0,panel.color.a - Time.deltaTime);
            if(panel.color.a < 0.1)cameGame = false;
        }else
        TelopManager();
    }

    void SetUpTutorial()
    {
        player.transform.position = tutorialPlayerPos;
        enemy.SetActive(false);
        bossHpSlider.SetActive(false);
        wall.SetActive(true);
        dissolve = wall.GetComponent<DissolveManager>();
        blueCube.SetActive(true);
    }

    void TelopManager()
    {
        CountTime += Time.deltaTime;
        DisplayTelop();
    }

    void GoToMain()
    {
        if(!first){
            enemy.SetActive(true);
            bossHpSlider.SetActive(true);
            blueCube.SetActive(false);
            wall.GetComponent<DissolveManager>().banish = true;
            first = true;
        }
    }

    void DisplayTelop()
    {
        switch(telopNum)
        {
            case 0:
                if(CountTime > 3f)
                {
                    telopNum = 1;
                }
                break;
            case 1:
                if((Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.E)))
                {
                    telopNum = 2;
                    CountTime = 0;
                }
                telop.text = "こんにちは オブジェクト173";
                subTelop.text = "Bボタン(E)で進む";
                break;
            case 2:
                if((Input.GetKeyDown("joystick button 16") || Input.GetKeyDown(KeyCode.Space)) && CountTime > 1f && !telopChange)
                {
                    telopChange = true;
                    CountTime = 0;
                }
                if(telopChange && CountTime > 1f)
                {
                    telopNum = 3;
                    CountTime = 0;
                }
                telop.text = "聞こえていたらジャンプをしてくれ";
                subTelop.text = "Aボタン(Space)でジャンプ";
                break;
            case 3:
                if(CountTime > 3f)
                {
                    Ray ray = new Ray (tutoCamera.transform.position,tutoCamera.transform.forward);
                    if(Physics.Raycast(ray,out Hit,100f) && Hit.collider.tag == "Window")
                    {
                        telopNum = 4;
                        CountTime = 0;
                    }
                }
                telop.text = "いい調子だ　こちらを見てくれるか？";
                subTelop.text = "左スティックで移動し\n右スティックで窓を見る";
                break;
            case 4:
                if(Input.GetKeyDown("joystick button 17")|| Input.GetKeyDown(KeyCode.E) && CountTime > 1f)
                {
                    telopNum = 5;
                }
                telop.text = "君の正面がどこか見分けられない\nという点以外はいいとしよう";
                subTelop.text = "Bボタン(E)で進む";
                break;
            case 5:
                if(Input.GetAxis("R_Trigger") > 0.9 || Input.GetMouseButton(0))
                {
                    if(CountTime > 2.5f)telopNum = 6;
                }else CountTime = 0;
                telop.text = "ではオブジェクト173\n君の変異能力を見せてくれ";
                subTelop.text = "右トリガー(RT)長押しで\n弾を呼ぶ";
                break;
            case 6:
                if((Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.E)))telopNum = 7;
                
                telop.text = "今日は赤色のようだね";
                subTelop.text = "右トリガーを離して飛ばす\nBボタン(E)で進む";
                break;
            case 7:
                if((Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.E)))telopNum = 8;
                telop.text = "今日の実験は\nオブジェクト049とのクロステストだ";
                subTelop.text = "Bボタン(E)で進む";
                break;
            case 8:
                if((Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.E)))
                {
                    telopNum = 9;
                    hitBlueCube = false;
                }
                telop.text = "オブジェクト049を暴走状態にした\nその状態での相互反応を確認する";
                subTelop.text = "Bボタン(E)で進む";
                break;
            case 9:
                if(hitBlueCube)
                {
                    telopNum = 10;
                    CountTime = 0;
                }
                telop.text = "準備ができたら\n青いオブジェクトを攻撃してくれ";
                subTelop.text = "青いオブジェクトに\n攻撃する";
                break;
            case 10:
                GoToMain();
                telop.text = "オブジェクト049を倒せ！";
                subTelop.text = "";
                if(CountTime > 5f)telopNum = 11;
                break;
            case 12:
                telopNum = 13;
                CountTime = 0;
                telop.text = "実験終了";
                break;
            case 13:
            Debug.Log(CountTime);
                if(CountTime > 5f)
                {
                    telopNum = 14;
                    CountTime = 0;
                }
                break;
            case 14:
                if(CountTime > 5f)
                {
                    telopNum = 15;
                    CountTime = 0;
                }
                telop.text = "大変興味深いデーターが取れた";
                break;
            case 15:
                if(CountTime > 5f)
                {
                    telopNum = 16;
                    CountTime = 0;
                } 
                telop.text = "安全を確認次第　再収容を行う";
                break;
            case 16:
                if(CountTime > 5f)
                {
                    telopNum = 17;
                }
                telop.text = "実験終了";
                break;
            case 17:
                panel.color = new Color(0,0,0,panel.color.a + Time.deltaTime);
                if(panel.color.a > 0.99) SceneManager.LoadScene("GameClear");
                break;
            default:
                telop.text = "";
                subTelop.text = "";
                break;
        }
    }
}
