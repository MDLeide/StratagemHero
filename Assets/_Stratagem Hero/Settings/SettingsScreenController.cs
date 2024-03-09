using UnityEngine;
using UnityEngine.InputSystem;

class SettingsScreenController : MonoBehaviour
{
    public InputActionReference DownAction;
    public InputActionReference UpAction;
    public InputActionReference ConfirmAction;
    public InputActionReference CancelAction;

    public SettingsScreen SettingsScreen;

    void Start()
    {
        DownAction.action.performed += c =>
        {
            if (!enabled)
                return;

            SettingsScreen.Down();
        };

        UpAction.action.performed += c =>
        {
            if (!enabled)
                return;

            SettingsScreen.Up();
        };

        ConfirmAction.action.performed += c =>
        {

            if (!enabled)
                return;

            SettingsScreen.Toggle();
        };

        CancelAction.action.performed += c =>
        {
            if (!enabled)
                return;

            SettingsScreen.Quit();
        };
    }
}