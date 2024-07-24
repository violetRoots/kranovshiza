using UnityEngine;

public class CraneController : MonoBehaviour
{
    [SerializeField] private Vector2 bordersHeight;
    [SerializeField] private Vector2 bordersRotation;
    [SerializeField] private Vector2 bordersForward;

    [SerializeField] private float elevationSpeed;
    [SerializeField] private float trolleySpeed;
    [SerializeField] private float rotateSpeed;

    /// <summary>
    /// Rotate and change height of this
    /// </summary>
    [SerializeField] private Transform craneObject;

    /// <summary>
    /// Move back and forward this
    /// </summary>
    [SerializeField] private Transform trolleyObject;

    private void Update()
    {
        var elevation = Input.GetAxis("Elevation"); 
        if (elevation != 0)
        {
            Elevate(elevation);
        }

        var vertical = Input.GetAxis("Vertical"); 
        if (vertical != 0)
        {
            MoveTrolley(vertical);
        }

        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            RotateCrane(horizontal);
        }
    }

    private void Elevate(float direction)
    {
        var currentHeight = craneObject.transform.position.y;
        currentHeight += direction * elevationSpeed * Time.deltaTime;
        var finalPos = craneObject.transform.position;
        finalPos.y = currentHeight;
        finalPos.y = Mathf.Clamp(finalPos.y, bordersHeight.x, bordersHeight.y);
        craneObject.transform.position = finalPos;
    }

    private void MoveTrolley(float direction)
    {
        var currentForward = trolleyObject.transform.localPosition.z;
        currentForward += direction * trolleySpeed * Time.deltaTime * -1f;
        var finalPos = trolleyObject.transform.localPosition;
        finalPos.z = currentForward;
        finalPos.z = Mathf.Clamp(finalPos.z, bordersForward.x, bordersForward.y);
        trolleyObject.transform.localPosition = finalPos;
    }

    private void RotateCrane(float direction)
    {
        var currentRotation = direction * rotateSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation, bordersRotation.x, bordersRotation.y);
        
        craneObject.transform.Rotate(Vector3.up, currentRotation);
    }
}