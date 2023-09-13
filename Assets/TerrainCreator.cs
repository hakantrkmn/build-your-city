using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TerrainCreator : MonoBehaviour
{
    public GameObject gridPrefab;
    
    public int gridWidth;
    public int gridHeight;
    public List<GameObject> gridObjects;

    


    [Button]
    private void CreateGrid()
    {
        foreach (var grid in gridObjects)
        {
            DestroyImmediate(grid);
        }

        gridObjects.Clear();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 gridPosition = new Vector3((-(gridWidth*4) / 2) + 2f + (x*4), 0, (-(gridHeight*4) / 2) + 2f + (z*4));
                GameObject grid = Instantiate(gridPrefab, gridPosition, gridPrefab.transform.rotation,transform);
                grid.transform.localPosition = gridPosition;
                gridObjects.Add(grid);
            }
        }
    }
}
