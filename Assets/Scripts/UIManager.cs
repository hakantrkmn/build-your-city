using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public FloatingJoystick joystick;


    private void OnEnable()
    {
        EventManager.GetJoystick += () => joystick;
    }
}
