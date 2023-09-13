using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public GameObject cubeParent;
    public Transform puzzleParent;
    public int index;
    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState==GameStates.OnTerrain)
        {
            EventManager.CellSelected(this);

        }
    }

    public void SavePuzzle(PuzzleController puzzle)
    {
        var data = Scriptable.GetTerrainData();
        for (int i = 0; i < puzzle.puzzleCubes.Count; i++)
        {
            data.gridData[index].cubeDataList.Add(new CubeData(puzzle.puzzleCubes[i].transform.localScale,puzzle.puzzleCubes[i].transform.position,puzzle.puzzleCubes[i].GetComponent<MeshRenderer>().material.color));
        }
    }

    public void CreatePuzzle(List<CubeData> dataList)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            var cube = Instantiate(cubeParent, new Vector3(0,i + .5f,0), Quaternion.identity);
            cube.transform.localScale = dataList[i].localScale;
            cube.transform.position = dataList[i].position + new Vector3(0,10,0);
            cube.transform.DOMoveY(dataList[i].position.y, 1).SetDelay(i*.1f);
            cube.transform.parent = puzzleParent;
            cube.GetComponent<MeshRenderer>().material.color = dataList[i].color;
        }
    }
}
