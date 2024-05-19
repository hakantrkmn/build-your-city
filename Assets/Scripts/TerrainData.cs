using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class TerrainData : ScriptableObject
{
    public GridData grid;


    [Button]
    public void ClearData()
    {
        grid.cells.Clear();
    }

    [Button]
    public void SaveData()
    {
        SaveManager.SaveGameData(this);
    }
}