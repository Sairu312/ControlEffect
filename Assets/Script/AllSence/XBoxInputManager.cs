using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum XBoxButtonCode{
    A,
    B,
    X,
    Y,
    LB,
    RB,
    Back,
    Start,
    LStickClick,
    RStickClick,
    DPadUp,
    DPadDown,
    DPadLeft,
    DPadRight,
    XBox
}

public enum XBoxAxisCode{
    LTrigger,
    RTrigger,
    LHorizontal,
    LVertical,
    RHorizontal,
    RVertical
}

public class XBoxInputManager : SingletonMonoBehaviour<XBoxInputManager>
{
    public bool XBoxInputButton(XBoxButtonCode buttonCode)
    {
        //Debug.Log(buttonCode);
        switch(buttonCode){
            #if UNITY_STANDALONE_WIN
                case XBoxButtonCode.A:
                    return Input.GetKeyDown("joystick button 0");
                case XBoxButtonCode.B:
                    return Input.GetKeyDown("joystick button 1");
                case XBoxButtonCode.X:
                    return Input.GetKeyDown("joystick button 2");
                case XBoxButtonCode.Y:
                    return Input.GetKeyDown("joystick button 3");
                case XBoxButtonCode.LB:
                    return Input.GetKey("joystick button 4");
                case XBoxButtonCode.RB:
                    return Input.GetKeyDown("joystick button 5");
                case XBoxButtonCode.Back:
                    return Input.GetKeyDown("joystick button 6");
                case XBoxButtonCode.Start:
                    return Input.GetKeyDown("joystick button 7");
                case XBoxButtonCode.LStickClick:
                    return Input.GetKeyDown("joystick button 8");
                case XBoxButtonCode.RStickClick:
                    return Input.GetKeyDown("joystick button 9");

            #else
                case XBoxButtonCode.A:
                    return Input.GetKeyDown("joystick button 16");
                case XBoxButtonCode.B:
                    return Input.GetKeyDown("joystick button 17");
                case XBoxButtonCode.X:
                    return Input.GetKeyDown("joystick button 18");
                case XBoxButtonCode.Y:
                    return Input.GetKeyDown("joystick button 19");
                case XBoxButtonCode.LB:
                    return Input.GetKey("joystick button 13");
                case XBoxButtonCode.RB:
                    return Input.GetKeyDown("joystick button 14");
                case XBoxButtonCode.Back:
                    return Input.GetKeyDown("joystick button 10");
                case XBoxButtonCode.Start:
                    return Input.GetKeyDown("joystick button 9");
                case XBoxButtonCode.LStickClick:
                    return Input.GetKeyDown("joystick button 11");
                case XBoxButtonCode.RStickClick:
                    return Input.GetKeyDown("joystick button 12");
            #endif
        }
        return false;
    }

    public float XBoxInputAxis(XBoxAxisCode AxisCode)
    {
        switch(AxisCode){
            #if UNITY_STANDALONE_WIN
                case XBoxAxisCode.LTrigger:
                    return Input.GetAxis("L_Trigger_Win")*2-1f;
                case XBoxAxisCode.RTrigger:
                    return Input.GetAxis("R_Trigger_Win")*2-1f;
                case XBoxAxisCode.RHorizontal:
                    return Input.GetAxis("R_Stick_H_Win");
                case XBoxAxisCode.RVertical:
                    return Input.GetAxis("R_Stick_V_Win");
                
              
            #else
                case XBoxAxisCode.LTrigger:
                    return Input.GetAxis("L_Trigger");
                case XBoxAxisCode.RTrigger:
                    return Input.GetAxis("R_Trigger");
                case XBoxAxisCode.RHorizontal:
                    return Input.GetAxis("R_Stick_H");
                case XBoxAxisCode.RVertical:
                    return Input.GetAxis("R_Stick_V");
            #endif
        }
        return 0f;
    }
}