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

    private void Start()
    {
        CheckTerrainData();
    }


    public void CheckTerrainData()
    {
        var data = Scriptable.GetTerrainData();
        for (int i = 0; i < data.gridData.Count; i++)
        {
            if (data.gridData[i].cubeDataList.Count != 0)
            {
                gridObjects[i].GetComponent<GridCell>().CreatePuzzle(data.gridData[i].cubeDataList);
            }
        }
    }

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
                Vector3 gridPosition = new Vector3((-(gridWidth * 4) / 2) + 2f + (x * 4), 0,
                    (-(gridHeight * 4) / 2) + 2f + (z * 4));
                GameObject grid = Instantiate(gridPrefab, gridPosition, gridPrefab.transform.rotation, transform);
                grid.transform.localPosition = gridPosition;
                grid.GetComponent<GridCell>().index = (x*gridHeight) + z;
                gridObjects.Add(grid);
            }
        }
    }
}