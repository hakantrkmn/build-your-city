using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public GameObject cubeParent;
    public Transform puzzleParent;
    public CanvasGroup canvasGroup;
    public int index;
    public bool isCellDone;
    

    public void FadeInOutCanvas(bool canClick)
    {
       
            canvasGroup.DOFade(canClick ? 1 : 0, .5f);
       
    }

    public void LoadPuzzle()
    {
        var data = Scriptable.GetTerrainData().grid.cells[index];
        if (!data.isCompleted) return;
        CreatePuzzle(data.cubeInfo);

       

    }
    
    public void SavePuzzle(PuzzleController puzzle)
    {
        var data = Scriptable.GetTerrainData().grid;
        for (int i = 0; i < puzzle.puzzleCubes.Count; i++)
        {
            
            data.cells[index].cubeInfo.Add(new CellCube(puzzle.puzzleCubes[i].transform.localScale,
                puzzle.puzzleCubes[i].transform.position,
                puzzle.puzzleCubes[i].GetComponent<MeshRenderer>().material.color));
                
        }

        data.cells[index].isCompleted = true;
        SaveManager.SaveGameData(Scriptable.GetTerrainData());

    }

    public void CreatePuzzle(List<CellCube> dataList)
    {
        isCellDone = true;
        for (int i = 0; i < dataList.Count; i++)
        {
            var cube = Instantiate(cubeParent, new Vector3(0, i + .5f, 0), Quaternion.identity);
            cube.transform.localScale = dataList[i].localScale;
            cube.transform.position = dataList[i].position + new Vector3(0, 10, 0);
            cube.transform.DOMoveY(dataList[i].position.y, 1).SetDelay(i * .1f);
            cube.transform.parent = puzzleParent;
            cube.GetComponent<MeshRenderer>().material.color = dataList[i].color;
        }
    }
}