using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class TerrainData : ScriptableObject
{
    public List<Terrains> terrains;
    public int currentGridIndex;

    
    

    [Button]
    public void ClearData()
    {
        foreach (var terrain in terrains)
        {
            foreach (var terr in terrain.terrain)
            {
                terr.terrainGrids.Clear();
            }
        }
    }
    [Button]
    public void SaveData()
    {
        SaveManager.SaveGameData(this);
    }
}
