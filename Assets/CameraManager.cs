using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera puzzleCamera;

    public CinemachineVirtualCamera terrainCamera;
    
    public Transform lookAt;

    private void OnEnable()
    {
        EventManager.PuzzleDone += PuzzleDone;
        EventManager.SetPuzzleCamera += SetPuzzleCamera;
        EventManager.CellSelected += CellSelected;
    }

    private void CellSelected(GridCell obj)
    {
        terrainCamera.Priority = 0;
        puzzleCamera.Priority = 10;
    }

    private void PuzzleDone(Transform obj)
    {
        terrainCamera.Priority = 10;
        puzzleCamera.Priority = 0;
        DOTween.To(()=> lookAt.position, x=> lookAt.position = x, Vector3.zero, 1);
    }

    private void OnDisable()
    {
        EventManager.CellSelected -= CellSelected;

        EventManager.PuzzleDone -= PuzzleDone;
        EventManager.SetPuzzleCamera -= SetPuzzleCamera;
    }

    private void SetPuzzleCamera(Transform cube)
    {
        DOTween.To(()=> lookAt.position, x=> lookAt.position = x, cube.position, 1);

    }
}
