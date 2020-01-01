using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public bool banish = false;
    public float threshold = 0;

    private Renderer mat1;
    private Renderer mat2;
    private Renderer mat3;
    private Collider col1;
    private Collider col2;
    private Collider col3;
    // Start is called before the first frame update
    void Start()
    {
        mat1 = wall1.GetComponent<Renderer>();
        mat2 = wall2.GetComponent<Renderer>();
        mat3 = wall3.GetComponent<Renderer>();
        col1 = wall1.GetComponent<MeshCollider>();
        col2 = wall2.GetComponent<MeshCollider>();
        col3 = wall3.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(banish)
        {
            col1.isTrigger = true;
            col2.isTrigger = true;
            col3.isTrigger = true;
            mat1.material.SetFloat("_Threshold",threshold);
            mat2.material.SetFloat("_Threshold",threshold);
            mat3.material.SetFloat("_Threshold",threshold);
            threshold += 1f * Time.deltaTime;
            if(threshold > 0.99)this.gameObject.SetActive(false);
        }
    }
}
