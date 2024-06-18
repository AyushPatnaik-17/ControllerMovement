
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    #region Controller Inputs

    private XRIDefaultInputActions _inputActions;
    private XRIDefaultInputActions.XRILeftHandActions _leftHandActions;
    private XRIDefaultInputActions.XRIRightHandActions _rightHandActions;

    #endregion

    public bool IsLeftButtonHeld { get; private set; } = false;
    public bool IsRightButtonHeld { get; private set; } = false;

    public event Action<InputAction.CallbackContext> OnButtonPressed; //check MenuManager
    public event Action<InputAction.CallbackContext> OnButtonReleased;//check MenuManager

    private void Awake()
    {

        _inputActions = new XRIDefaultInputActions();
        _leftHandActions = _inputActions.XRILeftHand;
        _rightHandActions = _inputActions.XRIRightHand;

        _leftHandActions.Menu.started += context =>
        {
            IsLeftButtonHeld = true;
            OnButtonPressed?.Invoke(context);
        };
        _leftHandActions.Menu.canceled += context =>
        {
            IsLeftButtonHeld = false;
            OnButtonReleased?.Invoke(context);
        };
        _rightHandActions.Menu.started += context =>
        {
            IsRightButtonHeld = true;
            OnButtonPressed?.Invoke(context);
        };
        _rightHandActions.Menu.canceled += context =>
        {
            IsRightButtonHeld = false;
            OnButtonReleased?.Invoke(context);
        };

    }

    private void OnEnable()
    {
        _leftHandActions.Enable();
        _rightHandActions.Enable();
    }
    private void OnDisable()
    {
        _leftHandActions.Disable();
        _rightHandActions.Disable();
    }
}
