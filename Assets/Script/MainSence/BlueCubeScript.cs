using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCubeScript : MonoBehaviour
{
    public GameObject tutorialObj;
    private TutorialManagaer tutoScript;

    void Start()
    {
        tutoScript = tutorialObj.GetComponent<TutorialManagaer>();
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject);
        if(collider.gameObject.tag == "Rubble")
        {
            tutoScript.hitBlueCube = true;
        }
    }
    
}
