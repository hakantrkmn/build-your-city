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
    private void Update()
    {
        if (GameManager.Instance.gameState==GameStates.OnTerrain)
        {

            if (Input.GetMouseButtonDown(0))
            {

                mouseStartPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                var direction = (Input.mousePosition - mouseStartPos).normalized;
                animator.SetFloat("Speed",direction.magnitude);

                model.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime * moveSpeed;
                model.forward=(new Vector3(direction.x, 0, direction.y));
            }

            if (Input.GetMouseButtonUp(0))
            {
                animator.SetFloat("Speed",0);
            }

        }
    }

   
}
