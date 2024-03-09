using UnityEngine;
using UnityEngine.InputSystem;

class GameOverScreenController : MonoBehaviour
{
    public GameOverScreen Screen;

    public InputActionReference LeftAction;
    public InputActionReference RightAction;
    public InputActionReference ConfirmAction;

    void Start()
    {
        LeftAction.action.performed += SwitchSelection;
        RightAction.action.performed += SwitchSelection;
        ConfirmAction.action.performed += OnConfirmed;
    }

    void OnConfirmed(InputAction.CallbackContext obj)
    {
        if (!enabled)
            return;

        Screen.Confirm();
    }

    void SwitchSelection(InputAction.CallbackContext obj)
    {
        if (!enabled)
            return;

        Screen.SwitchSelection();
    }

}