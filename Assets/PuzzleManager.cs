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
        obj.DOJump(selectedCell.transform.position, 2, 1, 1);
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
