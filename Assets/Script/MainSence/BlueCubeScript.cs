using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCubeScript : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject);
        if(collider.gameObject.tag == "Rubble")
        {
            Debug.Log("gogogogo");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
    }
    
}
