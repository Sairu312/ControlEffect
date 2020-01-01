using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Light[] lightArray = new Light[4];

    public bool alart = false;
    private bool once = false;
    private float countTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if(alart){
                AlartON();
        }else
        {
            AlartOFF();
        }
    }

    void AlartON()
    {
        //lightArray[0].color = new Color(Mathf.Sin(countTime * 10f)/4+0.75f,0.2f,0.2f,1f);
        for(int i = 0; i < 4; i++){
            lightArray[i].color = new Color(Mathf.Sin(countTime * 10f)/4+0.75f,0.2f,0.2f,1f);
        }
    }
    void AlartOFF()
    {
        for(int i = 0; i < 4; i++)
        {
            lightArray[i].color = Color.white;
        }
    }

}
