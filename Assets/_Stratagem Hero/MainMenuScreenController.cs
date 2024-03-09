using UnityEngine;
using UnityEngine.InputSystem;

class MainMenuScreenController : MonoBehaviour
{
    public MainMenuScreen Screen;

    public InputActionReference DownAction;
    public InputActionReference UpAction;
    public InputActionReference ConfirmAction;

    void Start()
    {
        DownAction.action.performed += c =>
        {
            if (!enabled)
                return;

            Screen.Down();
        };
        UpAction.action.performed += c =>
        {
            if (!enabled)
                return;

            Screen.Up();
        };
        ConfirmAction.action.performed += c =>
        {
            if (!enabled)
                return;

            Screen.Confirm();
        };
    }
}