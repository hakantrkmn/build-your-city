using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState==GameStates.OnTerrain)
        {
            EventManager.CellSelected(this);

        }
    }
}
