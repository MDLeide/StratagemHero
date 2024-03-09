using UnityEngine;
using UnityEngine.InputSystem;

class StratagemHeroController : MonoBehaviour
{
    public StratagemHero StratagemHero;

    [Header("Input")]
    public InputActionReference UpAction;
    public InputActionReference RightAction;
    public InputActionReference DownAction;
    public InputActionReference LeftAction;

    void Start()
    {
        UpAction.action.performed += c => ProcessInput(CommandKey.Up);
        RightAction.action.performed += c => ProcessInput(CommandKey.Right);
        DownAction.action.performed += c => ProcessInput(CommandKey.Down);
        LeftAction.action.performed += c => ProcessInput(CommandKey.Left);
    }

    void ProcessInput(CommandKey key)
    {
        if (!enabled)
            return;

        StratagemHero.ProcessInput(key);
    }
}