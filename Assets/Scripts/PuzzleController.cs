using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public GameObject puzzleCubePrefab;

    public Transform puzzleParent;

    public Transform puzzleIndex;

    public List<Cube> puzzleCubes;

    public Cube movingCube;
    public Vector3 puzzleCubeScale;

    public MovementAxis currentAxis;

    [Button]
    public void CreatePuzzle()
    {
        var firstCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
        firstCube.transform.position = puzzleIndex.position;
        puzzleCubes.Add(firstCube);
        puzzleIndex.localPosition += Vector3.up;


        movingCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
        movingCube.transform.position = puzzleIndex.position + Vector3.left * 5;
        puzzleIndex.localPosition += Vector3.up;

        movingCube.Movement(currentAxis,5);
    }


    public void CalculateCut()
    {
        DOTween.Kill("movement");
        var referenceCube = puzzleCubes.Last();

        if (currentAxis == MovementAxis.x)
        {
            var gap = movingCube.transform.position.x - referenceCube.transform.position.x;
            if (gap > 0)
            {
                var newScale = new Vector3(Mathf.Abs((gap - referenceCube.transform.lossyScale.x)), 1,
                    referenceCube.transform.lossyScale.z);
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                puzzleCubes.Add(stackCube);
                stackCube.transform.localPosition = new Vector3(referenceCube.transform.localPosition.x, 0,
                    0) + new Vector3(
                    (movingCube.transform.lossyScale.x / 2) - (Mathf.Abs(newScale.x) / 2),
                    movingCube.transform.localPosition.y, referenceCube.transform.localPosition.z);

                stackCube.transform.localScale = newScale;
                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCube.transform.lossyScale.x - newScale.x, 1,
                    movingCube.transform.lossyScale.z);
                dropCube.transform.localPosition = new Vector3(referenceCube.transform.localPosition.x, 0,
                    0) + new Vector3(
                    (movingCube.transform.lossyScale.x / 2) + (Mathf.Abs(dropCubeScale.x) / 2),
                    movingCube.transform.localPosition.y, referenceCube.transform.localPosition.z);
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.z;
                movingCube.transform.localScale = stackCube.transform.localScale;
                movingCube.transform.localPosition = stackCube.transform.localPosition -
                                                     new Vector3(0, -1,
                                                         stackCube.transform.lossyScale.z/2 +
                                                         movingCube.transform.lossyScale.z/2);
                movingCube.Movement(currentAxis,stackCube.transform.lossyScale.z/2 +
                                                movingCube.transform.lossyScale.z/2);
            }
            else
            {
                var newScale = new Vector3(Mathf.Abs((gap + referenceCube.transform.lossyScale.x)), 1,
                    referenceCube.transform.lossyScale.z);
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                puzzleCubes.Add(stackCube);
                stackCube.transform.localPosition = new Vector3(referenceCube.transform.localPosition.x, 0,
                    0) + new Vector3(
                    -(movingCube.transform.lossyScale.x / 2) + (Mathf.Abs(newScale.x) / 2),
                    movingCube.transform.localPosition.y, referenceCube.transform.localPosition.z);
                stackCube.transform.localScale = newScale;
                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCube.transform.lossyScale.x - newScale.x, 1,
                    movingCube.transform.lossyScale.z);
                dropCube.transform.localPosition = new Vector3(referenceCube.transform.localPosition.x, 0,
                    0) + new Vector3(
                    -(movingCube.transform.lossyScale.x / 2) - (Mathf.Abs(dropCubeScale.x) / 2),
                    movingCube.transform.localPosition.y, referenceCube.transform.localPosition.z);
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.z;
                movingCube.transform.localScale = stackCube.transform.localScale;

                movingCube.transform.localPosition = stackCube.transform.localPosition -
                                                     new Vector3(0, -1,
                                                         stackCube.transform.lossyScale.z/2 +
                                                         movingCube.transform.lossyScale.z/2);
                movingCube.Movement(currentAxis,stackCube.transform.lossyScale.z/2 +
                                                movingCube.transform.lossyScale.z/2);
            }
        }
        else
        {
            var gap = movingCube.transform.position.z - referenceCube.transform.position.z;
            if (gap > 0)
            {
                var newScale = new Vector3(referenceCube.transform.lossyScale.x, 1,
                    Mathf.Abs((gap - referenceCube.transform.lossyScale.z)));
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                puzzleCubes.Add(stackCube);
                stackCube.transform.localPosition =new Vector3(0, 0,
                    referenceCube.transform.localPosition.z)+ new Vector3(
                    referenceCube.transform.localPosition.x, movingCube.transform.localPosition.y,
                    (movingCube.transform.lossyScale.z / 2) - (Mathf.Abs(newScale.z) / 2));
                stackCube.transform.localScale = newScale;
                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCube.transform.lossyScale.x, 1,
                    movingCube.transform.lossyScale.z - newScale.z);
                dropCube.transform.localPosition =new Vector3(0, 0,
                    referenceCube.transform.localPosition.z)+ new Vector3(
                    referenceCube.transform.localPosition.x, movingCube.transform.localPosition.y,
                    (movingCube.transform.lossyScale.z / 2) + (Mathf.Abs(dropCubeScale.z) / 2));
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.x;
                movingCube.transform.localScale = stackCube.transform.localScale;

                movingCube.transform.localPosition = stackCube.transform.localPosition -
                                                     new Vector3(stackCube.transform.lossyScale.x/2 +
                                                                 movingCube.transform.lossyScale.x/2,-1,
                                                         0);
                movingCube.Movement(currentAxis,stackCube.transform.lossyScale.x/2 +
                                                movingCube.transform.lossyScale.x/2);
            }
            else
            {
                var newScale = new Vector3(referenceCube.transform.lossyScale.x, 1,
                    Mathf.Abs((gap + referenceCube.transform.lossyScale.z)));
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                puzzleCubes.Add(stackCube);
                stackCube.transform.localPosition =new Vector3(0, 0,
                    referenceCube.transform.localPosition.z)+ new Vector3(
                    referenceCube.transform.localPosition.x, movingCube.transform.localPosition.y,
                    -(movingCube.transform.lossyScale.z / 2) + (Mathf.Abs(newScale.z) / 2));
                stackCube.transform.localScale = newScale;
                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCube.transform.lossyScale.x, 1,
                    movingCube.transform.lossyScale.z - newScale.z);
                dropCube.transform.localPosition =new Vector3(0, 0,
                    referenceCube.transform.localPosition.z)+ new Vector3(
                    referenceCube.transform.localPosition.x, movingCube.transform.localPosition.y,
                    -(movingCube.transform.lossyScale.z / 2) - (Mathf.Abs(dropCubeScale.z) / 2));
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.x;
                movingCube.transform.localScale = stackCube.transform.localScale;
                movingCube.transform.localPosition = stackCube.transform.localPosition -
                                                     new Vector3(stackCube.transform.lossyScale.x/2 +
                                                                 movingCube.transform.lossyScale.x/2, -1,
                                                         0);
                movingCube.Movement(currentAxis,stackCube.transform.lossyScale.x/2 +
                                                movingCube.transform.lossyScale.x/2);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CalculateCut();
        }
    }
}