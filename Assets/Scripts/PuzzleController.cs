using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleController : MonoBehaviour
{
    public GameObject puzzleCubePrefab;

    public Transform puzzleParent;

    public Transform puzzleIndex;

    public List<Cube> puzzleCubes;

    public Cube movingCube;
    public bool isPuzzleDone;

    public MovementAxis currentAxis;

    void InitializeGradient()
    {
        gradient = new Gradient();

        // Blend color from red at 0% to blue at 100%
        var colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Random.ColorHSV(), 0.0f);
        colors[1] = new GradientColorKey(Random.ColorHSV(), 1.0f);

        // Blend alpha from opaque at 0% to transparent at 100%
        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(0.0f, 1.0f);

        gradient.SetKeys(colors, alphas);
    }

    [Button]
    public void CreatePuzzle()
    {
       InitializeGradient();

        var firstCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
        firstCube.transform.position = puzzleIndex.position;
        puzzleCubes.Add(firstCube);
        puzzleIndex.localPosition += Vector3.up;
        firstCube.SetColor(currentColor);
        colorTime += .05f;

        movingCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
        movingCube.transform.position = puzzleIndex.position + Vector3.left * 5;
        puzzleIndex.localPosition += Vector3.up;
        movingCube.SetColor(currentColor);
        movingCube.Movement(currentAxis, 5);
        EventManager.SetPuzzleCamera(puzzleCubes.Last().transform);
    }


    void CalculateCut()
    {
        DOTween.Kill("movement");
        var referenceCube = puzzleCubes.Last();
        var movingCubeTransform = movingCube.transform;
        var referenceCubeTransform = referenceCube.transform;
        
        if (currentAxis == MovementAxis.x)
        {
            var gap = movingCubeTransform.position.x - referenceCubeTransform.position.x;

            if (Mathf.Abs(gap) > referenceCubeTransform.lossyScale.x)
            {
                isPuzzleDone = true;

                movingCube.gameObject.SetActive(false);
                EventManager.PuzzleDone(transform);
                return;
            }

            var stackCubeScale = new Vector3(Mathf.Abs((gap * Mathf.Sign(gap)) - referenceCubeTransform.lossyScale.x),
                1,
                referenceCubeTransform.lossyScale.z);
            
            var stackCubeLocalPosition = new Vector3(referenceCubeTransform.localPosition.x, 0,
                0) + new Vector3(
                (Mathf.Sign(gap)) * (movingCubeTransform.lossyScale.x / 2) +
                (Mathf.Sign(gap) * -1) * (Mathf.Abs(stackCubeScale.x) / 2),
                movingCubeTransform.localPosition.y,
                referenceCubeTransform.localPosition.z);
            
            var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
            stackCube.transform.localPosition = stackCubeLocalPosition;

            stackCube.transform.localScale = stackCubeScale;
            
            puzzleCubes.Add(stackCube);

            

            var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
            var dropCubeScale = new Vector3(movingCubeTransform.lossyScale.x - stackCubeScale.x, 1,
                movingCubeTransform.lossyScale.z);
            dropCube.transform.localPosition = new Vector3(referenceCubeTransform.localPosition.x, 0,
                0) + new Vector3(
                Mathf.Sign(gap) * ((movingCubeTransform.lossyScale.x / 2) + (Mathf.Abs(dropCubeScale.x) / 2)),
                movingCubeTransform.localPosition.y, referenceCubeTransform.localPosition.z);
            puzzleIndex.localPosition += Vector3.up;
            dropCube.transform.localScale = dropCubeScale;
            dropCube.AddComponent<Rigidbody>();
            dropCube.transform.DOScale(0, 2).OnComplete(() => Destroy(dropCube.gameObject));

            currentAxis = MovementAxis.z;
            movingCubeTransform.localScale = stackCube.transform.localScale;
            movingCubeTransform.localPosition = stackCube.transform.localPosition +
                                                new Vector3(0, 1,
                                                    (stackCube.transform.lossyScale.z / 2 +
                                                      movingCubeTransform.lossyScale.z / 2));
            movingCube.Movement(currentAxis, stackCube.transform.lossyScale.z / 2 +
                                             movingCubeTransform.lossyScale.z / 2);
            
            stackCube.SetColor(currentColor);
            dropCube.SetColor(currentColor);
            colorTime += .05f;
            movingCube.SetColor(currentColor);
        }
        else
        {
            var gap = movingCubeTransform.position.z - referenceCubeTransform.position.z;
            if (Mathf.Abs(gap) > referenceCubeTransform.lossyScale.z)
            {
                isPuzzleDone = true;
                movingCube.gameObject.SetActive(false);
                EventManager.PuzzleDone(transform);
                return;
            }

            var stackCubeScale = new Vector3(referenceCubeTransform.lossyScale.x, 1,
                Mathf.Abs((gap * Mathf.Sign(gap)) - referenceCubeTransform.lossyScale.z));
            var stackCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
            puzzleCubes.Add(stackCube);
            stackCube.transform.localPosition = new Vector3(0, 0,
                referenceCubeTransform.localPosition.z) + new Vector3(
                referenceCubeTransform.localPosition.x, movingCubeTransform.localPosition.y,
                (Mathf.Sign(gap)) * (movingCubeTransform.lossyScale.z / 2) +
                (Mathf.Sign(gap) * -1) * (Mathf.Abs(stackCubeScale.z) / 2));
            stackCube.transform.localScale = stackCubeScale;
            var dropCube = Instantiate(puzzleCubePrefab, puzzleParent).GetComponent<Cube>();
            var dropCubeScale = new Vector3(movingCubeTransform.lossyScale.x, 1,
                movingCubeTransform.lossyScale.z - stackCubeScale.z);
            dropCube.transform.localPosition = new Vector3(0, 0,
                referenceCubeTransform.localPosition.z) + new Vector3(
                referenceCubeTransform.localPosition.x, movingCubeTransform.localPosition.y,
                Mathf.Sign(gap) * ((movingCubeTransform.lossyScale.z / 2) + (Mathf.Abs(dropCubeScale.z) / 2)));
            puzzleIndex.localPosition += Vector3.up;
            dropCube.transform.localScale = dropCubeScale;
            dropCube.AddComponent<Rigidbody>();
            currentAxis = MovementAxis.x;
            movingCubeTransform.localScale = stackCube.transform.localScale;

            dropCube.transform.DOScale(0, 2).OnComplete(() => Destroy(dropCube.gameObject));

            movingCubeTransform.localPosition = stackCube.transform.localPosition -
                                                new Vector3(stackCube.transform.lossyScale.x / 2 +
                                                            movingCubeTransform.lossyScale.x / 2, -1,
                                                    0);
            movingCube.Movement(currentAxis, stackCube.transform.lossyScale.x / 2 +
                                             movingCubeTransform.lossyScale.x / 2);
            stackCube.SetColor(currentColor);
            dropCube.SetColor(currentColor);
            colorTime += .05f;
            movingCube.SetColor(currentColor);
        }

        EventManager.SetPuzzleCamera(puzzleCubes.Last().transform);
    }

    private Gradient gradient;
    private Color currentColor => gradient.Evaluate(colorTime);
    private float colorTime;
    

    private void Update()
    {
        if (GameManager.Instance.gameState == GameStates.OnPuzzle && !isPuzzleDone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CalculateCut();
            }
        }
    }
}