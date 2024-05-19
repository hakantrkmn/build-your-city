using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public GameObject prefab;
    public Vector2 size;
    public List<Cell> cells;


    [Button]
    private void CreateGrid()
    {
        foreach (var cell in cells)
        {
            DestroyImmediate(cell.gameObject);
        }

        cells.Clear();
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
                Vector3 gridPosition = new Vector3((-(size.x * 4) / 2) + 2f + (x * 4), 
                    0,
                    (-(size.y * 4) / 2) + 2f + (z * 4));
                Cell grid = Instantiate(prefab, gridPosition, prefab.transform.rotation, transform).GetComponent<Cell>();
                grid.transform.localPosition = gridPosition;
                grid.index = cells.Count;
                cells.Add(grid);
            }
        }
    }
}