using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public float rotSpeed=20;
    public TerrainCreator terrainCreator;
    private void Update()
    {
        if (GameManager.Instance.gameState == GameStates.OnTerrain)
        {
            if (Input.GetMouseButton(0))
            {
                float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
                float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
                //transform.RotateAround(Vector3.up, -rotX);
                //ransform.RotateAround(Vector3.right, rotY);
            }
        }
    }

    public void LockUnlockGrids(bool isUnlock)
    {
        foreach (var grid in terrainCreator.gridObjects)
        {
            grid.GetComponent<GridCell>().LockUnlockGrid(isUnlock);
        }
    }

    private void OnEnable()
    {
        EventManager.CheckIfTerrainDone += CheckIfTerrainDone;
    }

    private void OnDisable()
    {
        EventManager.CheckIfTerrainDone -= CheckIfTerrainDone;
    }

    public void CheckIfTerrainDone()
    {
        foreach (var grid  in terrainCreator.gridObjects)
        {
            if (!grid.GetComponent<GridCell>().cellPuzzleDone)
            {
                return;
            }
        }

        EventManager.CurrentTerrainDone();
    }
}
