using UnityEngine;
using UnityEngine.InputSystem;

class RoundScoreScreenController : MonoBehaviour
{
    public RoundScoreScreen Screen;

    public InputActionReference ConfirmAction;
    public InputActionReference[] AdditionalConfirmActions;

    void Start()
    {
        foreach (var action in AdditionalConfirmActions)
            action.action.performed += OnConfirmed;

        ConfirmAction.action.performed += OnConfirmed;
    }

    void OnConfirmed(InputAction.CallbackContext obj)
    {
        if (!enabled)
            return;

        Screen.Exit();
    }
}