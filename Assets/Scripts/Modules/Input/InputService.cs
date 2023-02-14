using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour, InputProvider.IPlayerActions
{
    private InputProvider _inputProvider;
    public Vector2 MovementValue { get; private set; }
    public event Action OnJumpEvent;
    public event Action OnDodgeEvent;
    public event Action OnLockUnlockTargetEvent;

    private void Awake()
    {
        _inputProvider = new InputProvider();
        _inputProvider.Player.SetCallbacks(this);
        _inputProvider.Player.Enable();
    }

    private void OnDestroy()
    {
        _inputProvider.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnJumpEvent?.Invoke();
        Debug.Log($"{nameof(OnJump)} Pressed");
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnDodgeEvent?.Invoke();
        Debug.Log($"{nameof(OnDodge)} Pressed");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnLockUnlockTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnLockUnlockTargetEvent?.Invoke();
        Debug.Log($"{nameof(OnLockUnlockTarget)} Pressed");
    }
}