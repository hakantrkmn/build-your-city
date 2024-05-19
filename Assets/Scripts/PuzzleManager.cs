using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleController puzzlePrefab;
    public Cell selectedCell;


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
        selectedCell.isCellDone = true;
        obj.DOLocalMove(Vector3.zero, 2).OnComplete(() =>
        {
            tempCell.SavePuzzle(obj.GetComponent<PuzzleController>());
        });
        obj.DOLocalRotate(new Vector3(-90,0,0), 1);
        DOVirtual.DelayedCall(1, () =>
        {
            GameManager.Instance.gameState = GameStates.OnTerrain;
        });
        EventManager.CheckIfGridDone(selectedCell);
    }

    private async void CellSelected(Cell obj)
    {
        selectedCell = obj;

        var puzzle = Instantiate(puzzlePrefab.gameObject, selectedCell.transform.position + Vector3.up*15, Quaternion.identity)
            .GetComponent<PuzzleController>();

        puzzle.CreatePuzzle();
        await Task.Delay(1);
        GameManager.Instance.gameState = GameStates.OnPuzzle;
    }
}
