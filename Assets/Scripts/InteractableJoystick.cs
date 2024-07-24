using UnityEngine;

public class InteractableJoystick : MonoBehaviour
{
    [SerializeField] private Transform joystickTransform;
    
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] private float limitValue = 40f;
    

    private Vector3 initialRotation;
    private bool isInteracted;
    
    private void Start()
    {
        var interactable = GetComponent<Interactable>();
        interactable.OnPlayerActionCursorDrag += MoveJoystick;
        interactable.OnPlayerActionButtonDown += DisableAutoMovements;
        interactable.OnPlayerActionButtonUp += EnableAutoMovements;
        initialRotation = joystickTransform.rotation.eulerAngles;
    }

    private void EnableAutoMovements()
    {
        isInteracted = false;
    }

    private void DisableAutoMovements()
    {
        isInteracted = true;
    }

    // Y on mouse = rotate along X axis. +y = -x
    // X on mouse = rotate along Z axis -x = -z
    private void MoveJoystick(Vector2 delta)
    {
        float xRotation = delta.y * rotateSpeed * Time.deltaTime;
        float zRotation = -delta.x * rotateSpeed * Time.deltaTime;
        
        var currentRotation = joystickTransform.localEulerAngles;
        currentRotation.x += xRotation;
        currentRotation.z += zRotation;
        
        currentRotation.x = Mathf.Clamp(currentRotation.x, -limitValue, limitValue);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -limitValue, limitValue);

        // Apply clamped rotation
        joystickTransform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void Update()
    {
        if (isInteracted)
            return;

        //var newRotation = Vector3.Lerp(joystickTransform.rotation.eulerAngles, initialRotation, rotateSpeed * Time.deltaTime);
        //joystickTransform.rotation = Quaternion.Euler(newRotation);

        joystickTransform.rotation = Quaternion.Euler(initialRotation);
    }
}