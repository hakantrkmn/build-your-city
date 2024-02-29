using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Grid : MonoBehaviour
{
    public float rotSpeed = 20;

    [FormerlySerializedAs("terrainCreator")]
    public GridCreator gridCreator;
    public int index;

    private void Start()
    {
        CheckTerrainData();
    }


    public void CheckTerrainData()
    {
        
        SaveManager.LoadGameData(Scriptable.GetTerrainData());
        var data = Scriptable.GetTerrainData().terrains[index].terrain;
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].terrainGrids.Count != 0)
            {
                gridCreator.cells[i].CreatePuzzle(data[i].terrainGrids);
            }
        }
        
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameStates.OnTerrain)
        {
            RotateWithMouse();
        }
    }

    void RotateWithMouse()
    {
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
            //transform.RotateAround(Vector3.up, -rotX);
            //ransform.RotateAround(Vector3.right, rotY);
        }
    }

    public void LockUnlockGrid(LockUnlockTypes type)
    {
        foreach (var grid in gridCreator.cells)
        {
            grid.LockUnlockCell(type);
        }
    }

    private void OnEnable()
    {
        EventManager.CheckIfGridDone += CheckIfGridDone;
    }

    private void OnDisable()
    {
        EventManager.CheckIfGridDone -= CheckIfGridDone;
    }

    public void CheckIfGridDone(Cell completedCell)
    {
        if (gridCreator.cells.Contains(completedCell))
        {
            foreach (var grid in gridCreator.cells)
                if (!grid.isCellDone)
                    return;

            EventManager.CurrentGridCompleted();
        }
    }
}