using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
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
            text.color = new Color(1,1,1,text.color.a - 1.5f * Time.deltaTime);
            if(text.color.a < 0)SceneManager.LoadScene("MainScene");
        }
        //Debug.Log(coutTime);
    }
}
