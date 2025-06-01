using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchHandler : MonoBehaviour
{
    public static TouchHandler Instance;
    public Action<Vector3> OnTouch;

    private void Awake() 
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable() { EnhancedTouchSupport.Disable(); }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            Touch activeTouch = Touch.activeTouches[0];
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(activeTouch.screenPosition.x, activeTouch.screenPosition.y, Camera.main.nearClipPlane));
            OnTouch?.Invoke(worldPos);
        }
    }
}
