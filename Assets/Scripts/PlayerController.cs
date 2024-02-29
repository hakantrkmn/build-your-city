using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 mouseStartPos;
    public float moveSpeed;


    public Transform model;
    public Animator animator;

    private FloatingJoystick joystick;

    private void Start()
    {
        joystick = EventManager.GetJoystick();
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameStates.OnTerrain)
        {
            if (joystick.Direction.magnitude > .1f)
            {
                animator.SetFloat("Speed", joystick.Direction.magnitude);


                model.position += new Vector3(joystick.Horizontal, 0, joystick.Vertical) * Time.deltaTime * moveSpeed;
                model.forward = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        
    }
}


}