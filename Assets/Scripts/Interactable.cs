using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Action OnPlayerActionButtonDown;
    public Action OnPlayerActionButtonUp;
    public Action<Vector2> OnPlayerActionCursorDrag;

    private bool lookedAt;
    private bool pressedMouseDown;
    private Vector3 lastFrameMousePos;
    
    /// <summary>
    /// Entry point for interaction. Player is looking at collider
    /// </summary>
    public void LookAt()
    {
        lookedAt = true;
    }
    
    /// <summary>
    /// Exit point for interaction. Player was looking at collider, but not anymore
    /// </summary>
    public void StopLookAt()
    {
        lookedAt = false;
    }

    private void Update()
    {
        if (IsPlayerActionButtonDown())
        {
            pressedMouseDown = true;
            OnPlayerActionButtonDown?.Invoke();
        }

        if (IsPlayerActionButtonUp())
        {
            pressedMouseDown = false;
            OnPlayerActionButtonUp?.Invoke();
        }

        if (IsPlayerActionCursorDrag(out var delta))
        {
            OnPlayerActionCursorDrag?.Invoke(delta);
        }
    }

    private bool IsPlayerActionButtonDown()
    {
        if (!lookedAt)
            return false;
        
        return Input.GetMouseButtonDown(0);
    }
    
    private bool IsPlayerActionButtonUp()
    {
        if (!pressedMouseDown)
            return false;
        
        return Input.GetMouseButtonUp(0);
    }

    private bool IsPlayerActionCursorDrag(out Vector2 delta)
    {
        delta = Vector2.zero;

        if (!pressedMouseDown)
            return false;

        var mousePos = Input.mousePosition;
        delta = (lastFrameMousePos - mousePos).normalized;
        lastFrameMousePos = mousePos;
        
        return delta != Vector2.zero;
    }


}