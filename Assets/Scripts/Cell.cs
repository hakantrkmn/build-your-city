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
    public int index;
    public CanvasGroup canvasGroup;
    public Image lockImage;
    public TextMeshProUGUI buildText;
    public LockUnlockTypes lockType;
    public bool isCellDone;
    private void OnMouseDown()
    {
        if (lockType==LockUnlockTypes.Unlock)
        {
            if (GameManager.Instance.gameState == GameStates.OnTerrain)
            {
                EventManager.CellSelected(this);
            }
        }
    }

    public void LockUnlockCell(LockUnlockTypes type)
    {
        lockType = type;
        switch (lockType)
        {
            case LockUnlockTypes.Lock:
                canvasGroup.alpha = 1;
                buildText.gameObject.SetActive(false);
                lockImage.gameObject.SetActive(true);
                break;
            case LockUnlockTypes.Unlock:
                canvasGroup.alpha = 0;
                buildText.gameObject.SetActive(true);
                lockImage.gameObject.SetActive(false);
                break;
        }
        
    }

    public void FadeInOutCanvas(bool canClick)
    {
        if (lockType == LockUnlockTypes.Unlock)
        {
            canvasGroup.DOFade(canClick ? 1 : 0, .5f);
        }
       
    }

    public void SavePuzzle(PuzzleController puzzle)
    {
        var data = Scriptable.GetTerrainData().terrains[GetComponentInParent<Grid>().index].terrain;
        for (int i = 0; i < puzzle.puzzleCubes.Count; i++)
        {
            
            data[index].terrainGrids.Add(new TerrainCube(puzzle.puzzleCubes[i].transform.localScale,
                puzzle.puzzleCubes[i].transform.position,
                puzzle.puzzleCubes[i].GetComponent<MeshRenderer>().material.color));
                
        }
        SaveManager.SaveGameData(Scriptable.GetTerrainData());

    }

    public void CreatePuzzle(List<TerrainCube> dataList)
    {
        lockType = LockUnlockTypes.Lock;
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