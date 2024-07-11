using System;
using UnityEngine;

public class LogInteractables : MonoBehaviour
{
    private void Start()
    {
        var interactable = GetComponent<Interactable>();
        interactable.OnPlayerActionButtonDown += OnPlayerActionButtonDown;
        interactable.OnPlayerActionButtonUp += OnPlayerActionButtonUp;
        interactable.OnPlayerActionCursorDrag += OnPlayerActionCursorDrag;
    }

    private void OnPlayerActionCursorDrag(Vector2 delta)
    {
        Debug.Log($"Drag: {delta}");
    }

    private void OnPlayerActionButtonUp()
    {
        Debug.Log("Mouse Up");
    }

    private void OnPlayerActionButtonDown()
    {
        Debug.Log("Mouse Down");
    }
}