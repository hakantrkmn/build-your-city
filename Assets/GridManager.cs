using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Grid> terrains;


    private void Start()
    {
        var terrainIndex = Scriptable.GetTerrainData().currentGridIndex;
        foreach (var t in terrains)
        {
            t.LockUnlockGrid(LockUnlockTypes.Lock);
        }
        terrains[terrainIndex].LockUnlockGrid(LockUnlockTypes.Unlock);
    }

    private void OnValidate()
    {
        terrains.Clear();
        foreach (var terr in GetComponentsInChildren<Grid>())
        {
            terrains.Add(terr);
        }

        for (int i = 0; i < terrains.Count; i++)
        {
            terrains[i].index = i;
        }
    }

    private void OnEnable()
    {
        EventManager.CurrentGridCompleted += CurrentGridCompleted;
    }

    private void OnDisable()
    {
        EventManager.CurrentGridCompleted -= CurrentGridCompleted;
    }

    private void CurrentGridCompleted()
    {
        var gridIndex = Scriptable.GetTerrainData().currentGridIndex;
        terrains[gridIndex].LockUnlockGrid(LockUnlockTypes.Lock);
        gridIndex++;
        terrains[gridIndex].LockUnlockGrid(LockUnlockTypes.Unlock);
        Scriptable.GetTerrainData().currentGridIndex++;
        SaveManager.SaveGameData(Scriptable.GetTerrainData());
    }
}
