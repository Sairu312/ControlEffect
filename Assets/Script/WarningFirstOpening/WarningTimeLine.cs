using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class WarningTimeLine : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public Text warning1;
    public Text warning2;
    public Text warning3;
    public Text warning4;
    public GameObject movie;
    VideoPlayer videoPlayer;
    RawImage rawImage;
    public GameObject gameManager;
    public DataSave dataSave;

    public bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = movie.GetComponent<VideoPlayer>();
        rawImage = movie.GetComponent<RawImage>();
        dataSave = gameManager.GetComponent<DataSave>();
        dataSave.LoadData();
        StartCoroutine("TimeLine");
    }

    // Update is called once per frame
    void Update()
    {
        if(XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.A)||Input.GetKeyDown(KeyCode.Space))
        {
            flag = !flag;
        }
    }

    IEnumerator TimeLine()
    {
         yield return new WaitForSeconds(1.0f);
         StartCoroutine("WarningFadeIn");
         yield return new WaitForSeconds(1.0f);
         yield return new WaitWhile(() => !flag);
         StartCoroutine("WarningFadeOut");
         yield return new WaitForSeconds(0.5f);
         Debug.Log(dataSave.saveData.FirstSaveFlag);
         if(!dataSave.saveData.FirstSaveFlag)
         {
            videoPlayer.time = 0f;
            videoPlayer.Play();
            yield return new WaitForSeconds(0.5f);
            rawImage.color = Color.white;
            dataSave.saveData.FirstSaveFlag = true;
            dataSave.SaveOut(); 
            yield return new WaitForSeconds((float)videoPlayer.length);
            rawImage.color = Color.clear;
            StartCoroutine("Warning2FadeIn");
            yield return new WaitForSeconds(1.0f);
            yield return new WaitWhile(() => flag);
            StartCoroutine("Warning2FadeOut");
         }
         yield return new WaitForSeconds(1.0f);
         SceneManager.LoadScene("Title");
    }


    IEnumerator WarningFadeIn()
    {
        Color tmp = warning1.color;
        while(tmp.a < 1.0f)
        {
            warning1.color = tmp;
            warning2.color = tmp;
            tmp = new Color(tmp.r,tmp.g,tmp.b,tmp.a + fadeSpeed　* Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator WarningFadeOut()
    {
        Color tmp = warning1.color;
        while(tmp.a > 0f)
        {
            warning1.color = tmp;
            warning2.color = tmp;
            tmp = new Color(tmp.r,tmp.g,tmp.b,tmp.a - fadeSpeed　* Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Warning2FadeIn()
    {
        Color tmp = warning1.color;
        while(tmp.a < 1.0f)
        {
            warning3.color = tmp;
            warning4.color = tmp;
            tmp = new Color(tmp.r,tmp.g,tmp.b,tmp.a + fadeSpeed　* Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Warning2FadeOut()
    {
        Color tmp = warning1.color;
        while(tmp.a > 0f)
        {
            warning3.color = tmp;
            warning4.color = tmp;
            tmp = new Color(tmp.r,tmp.g,tmp.b,tmp.a - fadeSpeed　* Time.deltaTime);
            yield return null;
        }
    }
}
