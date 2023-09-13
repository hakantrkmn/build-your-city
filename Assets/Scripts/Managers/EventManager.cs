using System;
using UnityEngine;


public static class EventManager
{
    
    

    #region InputSystem
    public static Func<Vector2> GetInput;
    public static Func<Vector2> GetInputDelta;
    public static Action InputStarted;
    public static Action InputEnded;
    public static Func<bool> IsTouching;
    public static Func<bool> IsPointerOverUI;
    #endregion

  

    public static Action<Transform> SetPuzzleCamera;
    public static Action<Transform> PuzzleDone;

    public static Action<GridCell> CellSelected;



}