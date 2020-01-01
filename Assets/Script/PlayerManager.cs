﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private GameObject cameraOrigin;
    public GameObject hpBar;
    public GameObject playerModel;
    public Vector3 offset;
    public float aimSpeedV = 1f;
    public float aimSpeedH = 1f;
    public float mouseAimSpeedV = 5f;
    public float mouseAimSpeedH = 5f;
    private CharacterController characterController;
    public  Vector3 moveDirection = Vector3.zero;
    private float moveSpeed = 10f;
    private float jumpPower = 20f;
    private float rotationSpeed = 0.2f;
    private float rotValueH = 0f;
    private float rotValueV = 0f;
    private Vector3 colliderDirection;
    public GameObject gameManager;
    private float coutTime2 = 0;
    

    public float HP = 100;
    private float MaxHP = 100;
    private bool dead = false;

    public bool defenseFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        InitializeComponent();
    }

    // Update is called once per frame
    void Update()
    {
        coutTime2 += Time.deltaTime;
        if(HP>0)
        {
            CharacterInput();
            CharacterMove();
        }
        if(HP <= 0 && !dead)
        {
            PlayerDead();
        }
        cameraOrigin.transform.position = transform.position + offset;
        HPback();
    }

    void InitializeComponent()
    {
        characterController = GetComponent<CharacterController>();
        cameraOrigin.transform.position = transform.position + offset;
        MaxHP = HP;
    }


    void CharacterInput()
    {
        rotValueH += Input.GetAxis("Mouse X") * mouseAimSpeedH;
        rotValueV -= Input.GetAxis("Mouse Y") * mouseAimSpeedV;
        rotValueH += Input.GetAxis("R_Stick_H") * aimSpeedH;
        rotValueV -= Input.GetAxis("R_Stick_V") * aimSpeedV;
        if(rotValueV > 54)rotValueV = 54;
        if(rotValueV < -54)rotValueV = -54;
        cameraOrigin.transform.rotation = Quaternion.Euler(rotValueV,rotValueH,0);
    }

    void CharacterMove()
    {
        var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (characterController.isGrounded) 
        { 
            moveDirection = Vector3.zero;
            //ジャンプ
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 16")) 
                moveDirection.y += jumpPower;
        }
        
        //移動
        if(input.magnitude > 0f)
        {
            moveDirection.x = (input.z * Mathf.Cos(GetAngle()) - input.x * Mathf.Sin(-GetAngle())) * moveSpeed;
            moveDirection.z = (-input.x * Mathf.Cos(-GetAngle()) + input.z * Mathf.Sin(GetAngle())) * moveSpeed;
            Vector3 playerLookAt = new Vector3(moveDirection.x, 0, moveDirection.z);
            Quaternion playerLookAtRotation = Quaternion.LookRotation(playerLookAt);
            transform.rotation = Quaternion.Slerp (this.transform.rotation, playerLookAtRotation, rotationSpeed);
        }
        /*
        Vector3 playerLookAt = new Vector3(moveDirection.x, 0, moveDirection.z);
        Quaternion playerLookAtRotation = Quaternion.LookRotation(playerLookAt);
        transform.rotation = Quaternion.Slerp (this.transform.rotation, playerLookAtRotation, rotationSpeed);
        //transform.LookAt(transform.position + new Vector3(moveDirection.x,0,moveDirection.z));
        */
        
        moveDirection += colliderDirection;
        colliderDirection = Vector3.zero;
        
        //重力
        moveDirection.y -= 20 * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        
    }

    float GetAngle()
    {
        //Debug.Log(camera.transform.forward);
        float angle = Mathf.PI / 2f;
        if(playerCamera.transform.forward.z != 0)
        {
            angle = Mathf.Atan2(playerCamera.transform.forward.z, playerCamera.transform.forward.x);
        }
        else if(playerCamera.transform.forward.x <0)
            angle = -Mathf.PI / 2f;
        return angle;
    }


    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            if(coutTime2 > 1f)
            {
                hukitobasi(collider.gameObject,30f);
                coutTime2 = 0;
                if(!defenseFlag){
                    HP-=10;
                }
            }
        }
        if(collider.gameObject.CompareTag("Arm"))
        {
            if(coutTime2 > 1f)
            {
                colliderDirection += new Vector3(0,30,0);
                coutTime2 =0;
                if(!defenseFlag){
                    HP -= 10;
                }
            }
        }
    }

    void hukitobasi(GameObject obj,float i)
    {
       Vector3 bec = transform.position - obj.transform.position;
       colliderDirection += bec.normalized * i;
    }

    void HPback()
    {
        hpBar.GetComponent<Slider>().value = HP/MaxHP;
    }
    
    void PlayerDead()
    {
        playerModel.transform.localScale -= Vector3.one * Time.deltaTime;
        if(playerModel.transform.localScale.x < 0)
        {
            gameManager.GetComponent<TutorialManagaer>().telopNum = 12;
            dead = true;
        }
    }
}
