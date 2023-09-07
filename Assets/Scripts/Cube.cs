using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{


    public void Movement(MovementAxis axis,float distance)
    {
        distance *= 2;
        switch (axis)
        {
            case MovementAxis.x:
                transform.DOLocalMoveX(transform.localPosition.x + distance, 2).SetLoops(-1, LoopType.Yoyo).SetId("movement");
                break;
            case MovementAxis.z:
                transform.DOLocalMoveZ(transform.localPosition.z+distance, 2).SetLoops(-1, LoopType.Yoyo).SetId("movement");
                break;
        }
    }
}
