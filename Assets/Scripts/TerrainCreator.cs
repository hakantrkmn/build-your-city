using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TerrainCreator : MonoBehaviour
{
    public GameObject gridPrefab;

    public TerrainController terrainController;
    public int gridWidth;
    public int gridHeight;
    public List<GameObject> gridObjects;
    public int index;
    public bool terrainDone;
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
                gridObjects[i].GetComponent<GridCell>().CreatePuzzle(data[i].terrainGrids);
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