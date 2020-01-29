using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
    private AsyncOperation async;//ロードをコルーチンで動かす用の変数
    public Text text;
    private bool next = false;
    private float coutTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(1,1,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        coutTime  += Time.deltaTime;
        text.color = new Color(1,1,1,text.color.a + Time.deltaTime * 0.6f);
        if((XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.B) || Input.GetKeyDown(KeyCode.E)) && coutTime > 1f)
        next = true;
        if(next)
        {
            text.text = "ここは奇怪な異常物体を管理する施設\n\n君はそこで管理されている異常物体\nオブジェクト173「積み木」\n意思を持って動き，個体を増やしていく\n\n今日は施設での実験の日\n\n暴走状態のオブジェクト049「物質的異常空間」\nと実験することで何かわかるかもしれない\n\nロード中";
            text.color = new Color(1,1,1,text.color.a - 1.5f * Time.deltaTime);
            if(text.color.a < 0)StartCoroutine(LoadScene());
        }
        //Debug.Log(coutTime);
    }

    IEnumerator LoadScene(){
        async = SceneManager.LoadSceneAsync("MainScene");
        yield return null;
    }
}
