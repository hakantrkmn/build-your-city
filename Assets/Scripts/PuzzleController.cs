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

        movingCube.Movement(currentAxis, 5);
        EventManager.SetPuzzleCamera(puzzleCubes.Last().transform);
    }


    public void CreateStackCube(Vector3 localScale, Vector3 localPosition)
    {
    }

    public void CalculateCut()
    {
        DOTween.Kill("movement");
        var referenceCube = puzzleCubes.Last();
        var movingCubeTransform = movingCube.transform;
        var referenceCubeTransform = referenceCube.transform;
        if (currentAxis == MovementAxis.x)
        {
            var gap = movingCubeTransform.position.x - referenceCubeTransform.position.x;

            if (gap > 0)
            {
                var stackCubeScale = new Vector3(Mathf.Abs((gap - referenceCubeTransform.lossyScale.x)), 1,
                    referenceCubeTransform.lossyScale.z);
                var stackCubeLocalPosition = new Vector3(referenceCubeTransform.localPosition.x, 0,
                    0) + new Vector3(
                    (movingCubeTransform.lossyScale.x / 2) - (Mathf.Abs(stackCubeScale.x) / 2),
                    movingCubeTransform.localPosition.y, referenceCubeTransform.localPosition.z);
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();

                puzzleCubes.Add(stackCube);

                stackCube.transform.localPosition = stackCubeLocalPosition;

                stackCube.transform.localScale = stackCubeScale;

                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCubeTransform.lossyScale.x - stackCubeScale.x, 1,
                    movingCubeTransform.lossyScale.z);
                dropCube.transform.localPosition = new Vector3(referenceCubeTransform.localPosition.x, 0,
                    0) + new Vector3(
                    (movingCubeTransform.lossyScale.x / 2) + (Mathf.Abs(dropCubeScale.x) / 2),
                    movingCubeTransform.localPosition.y, referenceCubeTransform.localPosition.z);
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.z;
                movingCubeTransform.localScale = stackCube.transform.localScale;
                movingCubeTransform.localPosition = stackCube.transform.localPosition -
                                                    new Vector3(0, -1,
                                                        -(stackCube.transform.lossyScale.z / 2 +
                                                          movingCubeTransform.lossyScale.z / 2));
                movingCube.Movement(currentAxis, stackCube.transform.lossyScale.z / 2 +
                                                 movingCubeTransform.lossyScale.z / 2);
            }
            else
            {
                var stackCubeScale = new Vector3(Mathf.Abs((gap + referenceCubeTransform.lossyScale.x)), 1,
                    referenceCubeTransform.lossyScale.z);
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                puzzleCubes.Add(stackCube);
                stackCube.transform.localPosition = new Vector3(referenceCubeTransform.localPosition.x, 0,
                    0) + new Vector3(
                    -(movingCubeTransform.lossyScale.x / 2) + (Mathf.Abs(stackCubeScale.x) / 2),
                    movingCubeTransform.localPosition.y, referenceCubeTransform.localPosition.z);
                stackCube.transform.localScale = stackCubeScale;
                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCubeTransform.lossyScale.x - stackCubeScale.x, 1,
                    movingCubeTransform.lossyScale.z);
                dropCube.transform.localPosition = new Vector3(referenceCubeTransform.localPosition.x, 0,
                    0) + new Vector3(
                    -(movingCubeTransform.lossyScale.x / 2) - (Mathf.Abs(dropCubeScale.x) / 2),
                    movingCubeTransform.localPosition.y, referenceCubeTransform.localPosition.z);
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.z;
                movingCubeTransform.localScale = stackCube.transform.localScale;

                movingCubeTransform.localPosition = stackCube.transform.localPosition -
                                                    new Vector3(0, -1,
                                                        -(stackCube.transform.lossyScale.z / 2 +
                                                          movingCubeTransform.lossyScale.z / 2));
                movingCube.Movement(currentAxis, stackCube.transform.lossyScale.z / 2 +
                                                 movingCubeTransform.lossyScale.z / 2);
            }
        }
        else
        {
            var gap = movingCubeTransform.position.z - referenceCubeTransform.position.z;
            if (gap > 0)
            {
                var stackCubeScale = new Vector3(referenceCubeTransform.lossyScale.x, 1,
                    Mathf.Abs((gap - referenceCubeTransform.lossyScale.z)));
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                puzzleCubes.Add(stackCube);
                stackCube.transform.localPosition = new Vector3(0, 0,
                    referenceCubeTransform.localPosition.z) + new Vector3(
                    referenceCubeTransform.localPosition.x, movingCubeTransform.localPosition.y,
                    (movingCubeTransform.lossyScale.z / 2) - (Mathf.Abs(stackCubeScale.z) / 2));
                stackCube.transform.localScale = stackCubeScale;
                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCubeTransform.lossyScale.x, 1,
                    movingCubeTransform.lossyScale.z - stackCubeScale.z);
                dropCube.transform.localPosition = new Vector3(0, 0,
                    referenceCubeTransform.localPosition.z) + new Vector3(
                    referenceCubeTransform.localPosition.x, movingCubeTransform.localPosition.y,
                    (movingCubeTransform.lossyScale.z / 2) + (Mathf.Abs(dropCubeScale.z) / 2));
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.x;
                movingCubeTransform.localScale = stackCube.transform.localScale;

                movingCubeTransform.localPosition = stackCube.transform.localPosition -
                                                    new Vector3(stackCube.transform.lossyScale.x / 2 +
                                                                movingCubeTransform.lossyScale.x / 2, -1,
                                                        0);
                movingCube.Movement(currentAxis, stackCube.transform.lossyScale.x / 2 +
                                                 movingCubeTransform.lossyScale.x / 2);
            }
            else
            {
                var stackCubeScale = new Vector3(referenceCubeTransform.lossyScale.x, 1,
                    Mathf.Abs((gap + referenceCubeTransform.lossyScale.z)));
                var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                puzzleCubes.Add(stackCube);
                stackCube.transform.localPosition = new Vector3(0, 0,
                    referenceCubeTransform.localPosition.z) + new Vector3(
                    referenceCubeTransform.localPosition.x, movingCubeTransform.localPosition.y,
                    -(movingCubeTransform.lossyScale.z / 2) + (Mathf.Abs(stackCubeScale.z) / 2));
                stackCube.transform.localScale = stackCubeScale;
                var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
                var dropCubeScale = new Vector3(movingCubeTransform.lossyScale.x, 1,
                    movingCubeTransform.lossyScale.z - stackCubeScale.z);
                dropCube.transform.localPosition = new Vector3(0, 0,
                    referenceCubeTransform.localPosition.z) + new Vector3(
                    referenceCubeTransform.localPosition.x, movingCubeTransform.localPosition.y,
                    -(movingCubeTransform.lossyScale.z / 2) - (Mathf.Abs(dropCubeScale.z) / 2));
                puzzleIndex.localPosition += Vector3.up;
                dropCube.transform.localScale = dropCubeScale;
                dropCube.AddComponent<Rigidbody>();
                currentAxis = MovementAxis.x;
                movingCubeTransform.localScale = stackCube.transform.localScale;
                movingCubeTransform.localPosition = stackCube.transform.localPosition -
                                                    new Vector3(stackCube.transform.lossyScale.x / 2 +
                                                                movingCubeTransform.lossyScale.x / 2, -1,
                                                        0);
                movingCube.Movement(currentAxis, stackCube.transform.lossyScale.x / 2 +
                                                 movingCubeTransform.lossyScale.x / 2);
            }
        }

        EventManager.SetPuzzleCamera(puzzleCubes.Last().transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CalculateCut();
        }
    }
}