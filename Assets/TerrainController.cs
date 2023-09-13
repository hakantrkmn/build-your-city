using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public float rotSpeed=20;
    public GridCell selectedCell;
    private void Update()
    {
        if (GameManager.Instance.gameState == GameStates.OnTerrain)
        {
            if (Input.GetMouseButton(0))
            {
                float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;

                //transform.RotateAround(Vector3.up, -rotX);
            }
        }
    }

    private void OnEnable()
    {
        EventManager.CellSelected += CellSelected;
    }

    private void OnDisable()
    {
        EventManager.CellSelected -= CellSelected;
    }

    private void CellSelected(GridCell obj)
    {
        selectedCell = obj;
    }
}
