using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour ,InputSystem_Actions.IPlayerActions
{
    InputSystem_Actions inputActions;
    
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       InputEventBus.OnMove(context.ReadValue<float>());
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InputEventBus.OnJump();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InputEventBus.OnAttack();
        }
    }
}
