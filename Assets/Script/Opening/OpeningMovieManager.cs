using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class OpeningMovieManager : MonoBehaviour
{
    public float time = 0f;
    public float finishTime = 100f;
    public BezieToolScript[] bezieSctipts = Array.Empty<BezieToolScript>();
    public float[] startTimes;
    private bool startFlag = true;
    Dictionary<int,BezieToolScript> bezieDic = new Dictionary<int, BezieToolScript>();
    // Start is called before the first frame update
    void Start()
    {
        if(bezieSctipts.Length != startTimes.Length)startFlag = false;
        //StartCoroutine("bezieTimeLine");
    }

    /*コルーチンでのループでの処理は配列内の順番や同時の処理がしづらいので保留
    IEnumerator bezieTimeLine()
    {
        float beforeTime = 0f;
        for(int i = 0; i < startTimes.Length; i++)
        {
            yield return new WaitForSeconds(startTimes[i] - beforeTime);
            bezieSctipts[i].flag = true;
            beforeTime = startTimes[i];
        }
    }
 */

    // Update is called once per frame
    void Update()
    {
        if(!startFlag)return;
        for(int i = 0; i < bezieSctipts.Length;i++)
        {
            if(time > startTimes[i] && startTimes[i] > time - 1f)bezieSctipts[i].flag = true;
            if(time > finishTime){
                GameObject audioManager = GameObject.Find("AudioManager");
                AudioSource audioSource = audioManager.GetComponent<AudioSource>();
                audioSource.mute = false;
                SceneManager.LoadScene("StoryScene");
            }
        }
        time += Time.deltaTime;
    }
}
