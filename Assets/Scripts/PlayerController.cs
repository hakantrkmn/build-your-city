using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;


    public Transform model;
    public Animator animator;

    private FloatingJoystick joystick;

    private float _timer;

    private void Start()
    {
        joystick = EventManager.GetJoystick();
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameStates.OnTerrain)
        {
            if (Input.GetMouseButton(0))
            {
                _timer += Time.deltaTime;
                
                if(_timer > .2f)
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
            else if (Input.GetMouseButtonUp(0))
            {
                if (_timer < .2f)
                {
                    animator.SetFloat("Speed", 0);
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    int layer_mask = LayerMask.GetMask("Grid");
                    if (Physics.Raycast(ray, out hit,Mathf.Infinity,layer_mask))
                    {
                            EventManager.CellSelected(hit.collider.GetComponent<Cell>());
                    }
                    
                }
                animator.SetFloat("Speed", 0);

                _timer = 0;
            }

            
        }
    }
}