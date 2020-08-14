using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImage : MonoBehaviour
{
    public Camera cam;
    private CameraRot camRot;
    public GameObject cut2Target;
    public GameObject cut3Target;
    public GameObject cut4Target;
    public RubiksPattern rubiksPattern;
    public float brackTime = 30f;
    public float titleTime;
    public float cut2Time;
    public float cut3Time;
    public float cut4Time;
    public float lastcutTime;
    private Image brackOut;
    public Text control;
    public Text effect;
    
    // Start is called before the first frame update
    void Start()
    {
        camRot = cam.GetComponent<CameraRot>();
        this.brackOut = this.GetComponent<Image>();
        brackOut.color = new Color(0f,0f,0f,0f);
        StartCoroutine("UIcol");
    }
    IEnumerator UIcol()
    {
        yield return new WaitForSeconds(brackTime);
        brackOut.color = new Color(0f,0f,0f,1f);
        yield return new WaitForSeconds(titleTime - brackTime);
        while(control.color.a < 0.9f)
        {
            yield return null;
            float nowCol = control.color.a;
            nowCol += 1f / 60f;
            control.color = new Color(1f, 1f, 1f, nowCol);
            effect.color = new Color(1f,1f,1f,nowCol);
        }
        yield return new WaitForSeconds(cut2Time - titleTime);
        camRot.obj = cut2Target;
        brackOut.color = Color.clear;
        control.color = Color.clear;
        effect.color = Color.clear;
        yield return new WaitForSeconds(cut3Time - cut2Time);
        camRot.obj = cut3Target;
        yield return new WaitForSeconds(cut4Time - cut3Time);
        camRot.obj = cut4Target;
        rubiksPattern.flag = true;
        yield return new WaitForSeconds(lastcutTime - cut4Time);
        while(control.color.a < 0.9f)
        {
            yield return null;
            float nowCol = brackOut.color.a;
            nowCol += 1f / 60f;
            brackOut.color = new Color(0f, 0f, 0f, nowCol);
        }
    }
}
