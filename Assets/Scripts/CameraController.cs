using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Sensitivity
    {
        get => sensitivity;
        set => sensitivity = value;
    }

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits horizontal camera rotation. Allows spinning 180 degrees, but not 360. No need to look back")]
    [Range(0f, 90f)][SerializeField] float xRotationLimit = 50f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; // Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string yAxis = "Mouse Y";
    private const float xRotationOffset = 180; // Due to how camera is set up, offset is applied for every X axis clamp

    void Update()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -xRotationLimit + xRotationOffset, xRotationLimit + xRotationOffset);
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
    }
}
