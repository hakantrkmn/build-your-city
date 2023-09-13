using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void SetStackCube(Transform referenceCubeTransform, Transform movingCubeTransform, float gap)
    {
        var stackCubeScale = new Vector3(Mathf.Abs((gap - referenceCubeTransform.lossyScale.x)), 1,
            referenceCubeTransform.lossyScale.z);
        var stackCubeLocalPosition = new Vector3(referenceCubeTransform.localPosition.x, 0,
            0) + new Vector3(
            (movingCubeTransform.lossyScale.x / 2) - (Mathf.Abs(stackCubeScale.x) / 2),
            movingCubeTransform.localPosition.y, referenceCubeTransform.localPosition.z);
            transform.localPosition = stackCubeLocalPosition;
            transform.localScale = stackCubeScale;
    }

    public void Movement(MovementAxis axis,float distance)
    {
        distance++;
        distance *= 2;
        switch (axis)
        {
            case MovementAxis.x:
                transform.localPosition += Vector3.left;
                transform.DOLocalMoveX(transform.localPosition.x + distance, 2).SetLoops(-1, LoopType.Yoyo).SetId("movement");
                break;
            case MovementAxis.z:
                transform.localPosition += Vector3.forward;
                transform.DOLocalMoveZ(transform.localPosition.z-distance, 2).SetLoops(-1, LoopType.Yoyo).SetId("movement");
                break;
        }
    }
}
