using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    private float CountTime = 0f;
    public Text startGameText;
    public Text tutorialText;
    protected static int select = 1;
    public Color selectColor;
    public Color nonSelectColor;
    private bool goGame = false;
    public Image panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!goGame){
        TitleInput();
        SerectText();
        }else GoGame();
    }

    void TitleInput()
    {
        CountTime += Time.deltaTime;
        if(Input.GetAxis("Vertical") > 0.1f && CountTime > 0.2f)
        {
            CountTime = 0f;
            select = 1;
        }
        else if(Input.GetAxis("Vertical") < -0.1f && CountTime > 0.2f)
        {
            CountTime = 0f;
            select = 0;
        }
        if((XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.B) || Input.GetKeyDown(KeyCode.E)))
        {
            goGame = true;
            CountTime = 0f;
        }
    }

    void SerectText()
    {
        switch(select)
        {
            case 1:
                startGameText.color = selectColor;
                tutorialText.color = nonSelectColor;
                break;
            case 0:
                startGameText.color = nonSelectColor;
                tutorialText.color = selectColor;
                break;
        }
    }

    public static int GetSelect()
    {
        return select;
    }
    void GoGame()
    {
        panel.color = new Color(0,0,0,panel.color.a + Time.deltaTime);
        //Debug.Log(panel.color.a);
        if(panel.color.a > 0.99)
        {
            if(select == 0)
            SceneManager.LoadScene("MainScene");
            else
            SceneManager.LoadScene("OpeningMove");
        }
    }
}
