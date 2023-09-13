using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ScriptableManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] PlayerMovementSettings PlayerMovementSettings;
    public TerrainData terrainData;

    //-------------------------------------------------------------------
    void Awake()
    {
        SaveManager.LoadGameData(gameData);

        Scriptable.GetTerrainData = GetTerrainData;
        Scriptable.GameData = GetGameData;
        Scriptable.PlayerSettings = GetPlayerMovementSettings;
    }


    //-------------------------------------------------------------------
    GameData GetGameData() => gameData;
    TerrainData GetTerrainData() => terrainData;

    //-------------------------------------------------------------------
    PlayerMovementSettings GetPlayerMovementSettings() => PlayerMovementSettings;

}



public static class Scriptable
{
    public static Func<GameData> GameData;
    public static Func<PlayerMovementSettings> PlayerSettings;
    public static Func<TerrainData> GetTerrainData;

}