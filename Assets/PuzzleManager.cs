using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleController puzzlePrefab;
    public GridCell selectedCell;


    private void OnEnable()
    {
        EventManager.PuzzleDone += PuzzleDone;
        EventManager.CellSelected += CellSelected;
    }

    private void OnDisable()
    {
        EventManager.PuzzleDone -= PuzzleDone;
        EventManager.CellSelected -= CellSelected;
    }

    private void PuzzleDone(Transform obj)
    {
        var tempCell = selectedCell;
        obj.transform.parent = selectedCell.puzzleParent;
        
        obj.DOLocalJump(Vector3.zero, 2, 1, 1).OnComplete(() =>
        {
            tempCell.SavePuzzle(obj.GetComponent<PuzzleController>());
        });
        obj.DOLocalRotate(new Vector3(-90,0,0), 1);
        GameManager.Instance.gameState = GameStates.OnTerrain;

    }

    private async void CellSelected(GridCell obj)
    {
        selectedCell = obj;

        var puzzle = Instantiate(puzzlePrefab.gameObject, selectedCell.transform.position, Quaternion.identity)
            .GetComponent<PuzzleController>();

        puzzle.CreatePuzzle();
        await Task.Delay(1);
        GameManager.Instance.gameState = GameStates.OnPuzzle;
    }
}
