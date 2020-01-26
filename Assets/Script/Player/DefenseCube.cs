using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCube : MonoBehaviour
{
    public float changeTime = 0f;

    public GameObject player;
    private PlayerManager playerScript;
    public GameObject[] defenseCube = new GameObject[26];

    public float countTime = 0f;
    public float animTime = 2f;
    public bool startAnim = false;
    public float defenseSize = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DefenseInput();
        if(startAnim)
        {
            countTime = 0f;
            startAnim = false;
        }
        DefenseAnimation();
    }

    void DefenseAnimation()
    {
        //countTime += Time.deltaTime;
            for(int i = 0; i < defenseCube.Length; i++)
            {
                defenseCube[i].transform.localScale = Mathf.Sin((countTime * Mathf.PI) / (animTime * 2f) ) * Vector3.one * defenseSize;
                if(defenseCube[i].transform.localScale.x < 0.1) defenseCube[i].transform.localScale = Vector3.zero;
            }
    }

    void DefenseInput()
    {
        if(XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.LB) || Input.GetKey(KeyCode.Q))
        {
            playerScript.defenseFlag = true;
            countTime += (countTime < animTime) ? Time.deltaTime : 0f;
        }else if(!XBoxInputManager.Instance.XBoxInputButton(XBoxButtonCode.LB) || !Input.GetKey(KeyCode.Q))
        {
            playerScript.defenseFlag = false;
            countTime += (countTime > 0) ? -Time.deltaTime : 0f;
        }
    }
}
