using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchProvider : MonoBehaviour
{
    public static TouchProvider Instance;

    public event Action<Vector2, Vector2> OnSwipe;
    public event Action<Vector2> OnTap;

    private Vector2? _primaryTouchBeganPosition;
    private Vector2? _primaryTouchEndedPosition;

    private const float SwipeThreshold = 64f;
    private const float TapThreshold = 64f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            TouchSimulation.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        var phase = Touchscreen.current.primaryTouch.phase.ReadValue();

        switch (phase)
        {
            case TouchPhase.Began:
                OnTouchBegan();
                break;
            case TouchPhase.Ended:
                OnTouchEnded();
                break;
            default:
                break;
        }
    }

    void OnTouchBegan()
    {
        _primaryTouchBeganPosition = Touchscreen.current.primaryTouch.position.ReadValue();
    }

    void OnTouchEnded()
    {
        _primaryTouchEndedPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        if (_primaryTouchBeganPosition != null && _primaryTouchEndedPosition != null)
        {
            CheckIfSwiped(_primaryTouchBeganPosition.Value, _primaryTouchEndedPosition.Value);
            CheckIfTapped(_primaryTouchBeganPosition.Value, _primaryTouchEndedPosition.Value);
        }

        _primaryTouchBeganPosition = null;
        _primaryTouchEndedPosition = null;
    }

    void CheckIfSwiped(Vector2 from, Vector2 to)
    {
        var deltaX = to.x - from.x;
        var deltaY = to.y - from.y;

        Vector2 direction;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (Mathf.Abs(deltaX) < SwipeThreshold)
            {
                return;
            }

            direction = deltaX > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            if (Mathf.Abs(deltaY) < SwipeThreshold)
            {
                return;
            }

            direction = deltaY > 0 ? Vector2.up : Vector2.down;
        }
        
        OnSwipe?.Invoke(direction, from);

        Debug.Log($"OnSwipe {direction} {from}");
    }

    void CheckIfTapped(Vector2 from, Vector2 to)
    {
        var deltaX = to.x - from.x;
        var deltaY = to.y - from.y;

        if (Mathf.Abs(deltaX) > TapThreshold || Mathf.Abs(deltaY) > TapThreshold)
        {
            return;
        }

        OnTap?.Invoke(from);

        Debug.Log($"OnTap {from}");
    }
}