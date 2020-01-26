using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerCheck : MonoBehaviour
{
    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag){
        if (Input.GetKeyDown ("joystick button 10")) {
            Debug.Log ("button10");
        }
        if (Input.GetKeyDown ("joystick button 11")) {
            Debug.Log ("button11");
        }
        if (Input.GetKeyDown ("joystick button 12")) {
            Debug.Log ("button12");
        }
        if (Input.GetKeyDown ("joystick button 13")) {
            Debug.Log ("button13");
        }
        if (Input.GetKeyDown ("joystick button 14")) {
            Debug.Log ("button14");
        }
        if (Input.GetKeyDown ("joystick button 5")) {
            Debug.Log ("button5");
        }
        if (Input.GetKeyDown ("joystick button 6")) {
            Debug.Log ("button6");
        }
        if (Input.GetKeyDown ("joystick button 7")) {
            Debug.Log ("button7");
        }
        if (Input.GetKeyDown ("joystick button 8")) {
            Debug.Log ("button8");
        }
        if (Input.GetKeyDown ("joystick button 9")) {
            Debug.Log ("button9");
        }
        if (Input.GetKeyDown ("joystick button 15")) {
            Debug.Log ("button15");
        }
        if (Input.GetKeyDown ("joystick button 16")) {
            Debug.Log ("button16");
        }
        if (Input.GetKeyDown ("joystick button 17")) {
            Debug.Log ("button17");
        }
        if (Input.GetKeyDown ("joystick button 18")) {
            Debug.Log ("button18");
        }
        if (Input.GetKeyDown ("joystick button 19")) {
            Debug.Log ("button19");
        }
        Debug.Log("R_Trigger : " + Input.GetAxis("R_Trigger"));
        Debug.Log("L_Trigger : " + Input.GetAxis("L_Trigger"));
    }
    }
}
