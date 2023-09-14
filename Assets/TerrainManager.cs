using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public List<TerrainController> terrains;


    private void Start()
    {
        var terrainIndex = Scriptable.GetTerrainData().currentTerrainIndex;
        foreach (var t in terrains)
        {
            t.LockUnlockGrids(false);
        }
        terrains[terrainIndex].LockUnlockGrids(true);
    }

    private void OnValidate()
    {
        terrains.Clear();
        foreach (var terr in GetComponentsInChildren<TerrainController>())
        {
            terrains.Add(terr);
        }

        for (int i = 0; i < terrains.Count; i++)
        {
            terrains[i].terrainCreator.index = i;
        }
    }

    private void OnEnable()
    {
        EventManager.CurrentTerrainDone += CurrentTerrainDone;
    }

    private void OnDisable()
    {
        EventManager.CurrentTerrainDone -= CurrentTerrainDone;
    }

    private void CurrentTerrainDone()
    {
        Debug.Log("sa");
        var terrainIndex = Scriptable.GetTerrainData().currentTerrainIndex;
        terrains[terrainIndex].LockUnlockGrids(false);
        terrainIndex++;
        terrains[terrainIndex].LockUnlockGrids(true);
        Scriptable.GetTerrainData().currentTerrainIndex++;
        SaveManager.SaveGameData(Scriptable.GetTerrainData());
    }
}
