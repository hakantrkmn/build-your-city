using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GridCreator gridCreator;

    private void Start()
    {
        CheckTerrainData();
    }


    private void CheckTerrainData()
    {
        SaveManager.LoadGameData(Scriptable.GetTerrainData());
        var grid = Scriptable.GetTerrainData().grid;

        if (grid.cells.Count == 0)
        {
            for (int i = 0; i < gridCreator.cells.Count; i++)
            {
                var cell = new CellData();
                grid.cells.Add(cell);
            }
        }

        foreach (var cell in gridCreator.cells)
        {
            cell.LoadPuzzle();
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

    void CheckIfGridDone(Cell completedCell)
    {
        if (!gridCreator.cells.Contains(completedCell)) return;
        
        foreach (var grid in gridCreator.cells)
        {
            if (!grid.isCellDone)
                return;
        }
    }
}