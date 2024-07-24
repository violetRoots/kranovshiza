using UnityEngine;

/// <summary>
/// The only thing this does is sending raycast, and sending callback to Interactable class. 
/// </summary>
public class CameraPointerRaycast : MonoBehaviour
{
    /// <summary>
    /// Everything on this physical layers catches OR BLOCKS raycasts
    /// </summary>
    [SerializeField] private LayerMask interactableLayers;

    /// <summary>
    /// Will print more debug logs
    /// </summary>
    [SerializeField] private bool isDebug;
    
    private Camera cachedCamera;
    private Vector3 middleScreenPosition;
    private Interactable previousLookedAt;

    private void Start()
    {
        cachedCamera = Camera.main;
        if (cachedCamera == null)
        {
            Debug.LogError("No cached camera at the start of the scene, camera raycaster not initialized");
            return;
        }
        middleScreenPosition = new Vector3(cachedCamera.pixelWidth / 2f, cachedCamera.pixelHeight / 2f);
    }

    private void Update()
    {
        //Ray middleScreenRay = cachedCamera.ScreenPointToRay(middleScreenPosition);
        Ray middleScreenRay = cachedCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(middleScreenRay, out var hitInfo, 100f, interactableLayers, QueryTriggerInteraction.Collide))
        {
            return;
        }
        
        if (isDebug)
            Debug.Log($"Hit object: {hitInfo.collider.gameObject}");

        Interactable currentLookedAt = hitInfo.collider.GetComponent<Interactable>();
        
        // We were looking at something else, notify it
        if (previousLookedAt != null && previousLookedAt != currentLookedAt)
        {
            previousLookedAt.StopLookAt();
            previousLookedAt = null;
        }

        // No interactable
        if (currentLookedAt == null)
        {
            return;
        }
        
        // We are looking at interactable
        currentLookedAt.LookAt();
        previousLookedAt = currentLookedAt;
    }
}