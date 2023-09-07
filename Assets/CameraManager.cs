using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera puzzleCamera;

    public Transform lookAt;

    private void OnEnable()
    {
        EventManager.SetPuzzleCamera += SetPuzzleCamera;
    }

    private void OnDisable()
    {
        EventManager.SetPuzzleCamera -= SetPuzzleCamera;
    }

    private void SetPuzzleCamera(Transform cube)
    {
        DOTween.To(()=> lookAt.position, x=> lookAt.position = x, cube.position, 1);

    }
}
