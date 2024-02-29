using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public GameObject gridPrefab;
    public Vector2 gridSize;
    public List<Cell> cells;


    [Button]
    private void CreateGrid()
    {
        foreach (var cell in cells)
        {
            DestroyImmediate(cell.gameObject);
        }

        cells.Clear();
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                Vector3 gridPosition = new Vector3((-(gridSize.x * 4) / 2) + 2f + (x * 4), 0,
                    (-(gridSize.y * 4) / 2) + 2f + (z * 4));
                Cell grid = Instantiate(gridPrefab, gridPosition, gridPrefab.transform.rotation, transform).GetComponent<Cell>();
                grid.transform.localPosition = gridPosition;
                grid.index = (x*(int)gridSize.x) + z;
                cells.Add(grid);
            }
        }
    }
}