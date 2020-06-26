using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetScript : MonoBehaviour
{
    public GameObject enemy;
    public EnemyScript enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        enemyScript = enemy.GetComponent<EnemyScript>();
    }

    void Update()
    {
        if(enemy == null)
        {
            enemy = GameObject.Find("Enemy");
            enemyScript = enemy.GetComponent<EnemyScript>();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Floor" || collider.gameObject.tag == "RiseBlock")
        {
            //Debug.Log(collider.gameObject.name);
            enemyScript.tipHitFlag = true;
        }
    }

}
