using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class GameInputController : MonoBehaviour
{
    public float moveInput {get; private set;}

    public bool isSprinting { get; private set; }
    public bool isCrouching { get; private set; }

    private InputControl inputActions;

    private void Awake()
    {
        inputActions = new InputControl();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Sprint.performed += ActivateSprint;
        inputActions.Player.Sprint.canceled += CancelSprint;
        inputActions.Player.Crouch.performed += ActivateCrouch;
        inputActions.Player.Crouch.canceled += CancelCrouch;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Sprint.performed -= ActivateSprint;
        inputActions.Player.Sprint.canceled -= CancelSprint; 
        inputActions.Player.Crouch.performed -= ActivateCrouch;
        inputActions.Player.Crouch.canceled -= CancelCrouch;
    }

    private void CancelCrouch(InputAction.CallbackContext context)
    {
        isCrouching = false;
    }

    private void ActivateCrouch(InputAction.CallbackContext context)
    {
        isCrouching = true;
    }

    public void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<float>();
    }

    private void Start()
    {
    }

    private void CancelSprint(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }

    private void ActivateSprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }
}
