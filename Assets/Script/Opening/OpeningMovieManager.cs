using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpeningMovieManager : MonoBehaviour
{
    private float time = 0f;
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

/*
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
        }
        time += Time.deltaTime;
    }

}
