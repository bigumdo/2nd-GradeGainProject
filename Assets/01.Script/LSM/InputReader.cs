using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controller;

[CreateAssetMenu(menuName = "SO/Input")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public Vector3 Movement { get; private set; }
    public Action OnJumpEvent;

    Controller controls;
    private void OnEnable()
    {
        if (controls == null)
            controls = new Controller();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnJumpEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();
        Movement = new Vector3(vec.x, 0, vec.y);
    }
}
