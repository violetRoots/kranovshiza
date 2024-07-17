using UnityEngine;

public class CraneHook : MonoBehaviour
{
    [SerializeField] private Transform hook;
    [SerializeField] private LayerMask hookLayerMask;

    private Hookable currentHookedItem;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseHook();
        }
    }

    private void UseHook()
    {
        if (currentHookedItem != null)
            ReleaseHook();
        else
            GrabHook();
    }

    private void GrabHook()
    {
        Ray ray = new Ray(hook.position, Vector3.down);
        if (!Physics.Raycast(ray, out var hitInfo, 99f, hookLayerMask))
            return;

        var hooked = hitInfo.transform.GetComponent<Hookable>();
        if (hooked == null)
            return;
        
        Hook(hooked);
    }

    private void Hook(Hookable stuffToHook)
    {
        currentHookedItem = stuffToHook;
        var newPosition = stuffToHook.transform.position;
        newPosition.y = hook.transform.position.y;
        stuffToHook.transform.position = newPosition;
        stuffToHook.transform.SetParent(hook);
    }

    private void ReleaseHook()
    {
        currentHookedItem.transform.SetParent(null);
        var newPosition = currentHookedItem.transform.position;
        newPosition.y = -8f;
        currentHookedItem.transform.position = newPosition;
        currentHookedItem = null;
    }
}