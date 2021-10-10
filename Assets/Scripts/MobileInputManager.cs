using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class MobileInputManager : MonoBehaviour
{
    private TouchControls _touchControls;

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    
    private void Awake()
    {
        _touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
    }

    private void OnDisable()
    {
        _touchControls.Disable();
    }

    private void Start()
    {
        _touchControls.Touch.Press.started += StartTouch;
        _touchControls.Touch.Press.canceled += EndTouch;
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log($"Touch Started: {_touchControls.Touch.PressPosition.ReadValue<Vector2>()}");
        if (OnStartTouch != null)
            OnStartTouch(_touchControls.Touch.PressPosition.ReadValue<Vector2>(), (float) context.startTime);
    }
    
    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log($"Touch Ended: {_touchControls.Touch.PressPosition.ReadValue<Vector2>()}");
        if (OnEndTouch != null)
            OnEndTouch(_touchControls.Touch.PressPosition.ReadValue<Vector2>(), (float) context.time);
    }
    
}
