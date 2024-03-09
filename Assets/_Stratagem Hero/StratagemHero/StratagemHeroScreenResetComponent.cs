using System.Linq;
using Cashew.Utility.Async;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

class StratagemHeroScreenResetComponent : MonoBehaviour
{
    bool[] _resetBuffer;

    [Title("Configuration")]
    public StratagemHeroScreen Screen;
    public StratagemHero Hero;

    [Title("Settings")]
    public bool InstantReset;
    public float ResetOverTimeDelay = .1f;

    [Title("State")]
    [ShowInInspector, ReadOnly]
    public bool IsResetting { get; private set; }

    Image[] Commands => Screen.Commands;

    public void ResetCommands(bool[] state)
    {
        // if we're using the fancy reset over time and this gets
        // called again, before we're done animating, fall back to
        // the instant reset
        if (IsResetting)
        {
            if (!InstantReset)
                OnInstantReset(state);

            return;
        }

        if (InstantReset)
            OnInstantReset(state);
        else
            OnResetOverTime(state);
    }

    public void AddToResetBuffer(int index)
    {
        _resetBuffer[index] = true;
    }

    void OnInstantReset(bool[] state)
    {
        for (int i = 0; i < Commands.Length; i++)
        {
            Commands[i].material = Screen.DeactivatedCommandMaterial;
            state[i] = false;
        }

        IsResetting = false;
    }

    void OnResetOverTime(bool[] previousState)
    {
        IsResetting = true;

        _resetBuffer = new bool[Commands.Length];
        //var index = Commands.Length - 1 - _currentState.Count(p => !p);
        var index = Commands.Length - 1 - previousState.Count(p => !p); // start resetting from the end, skipping any that are already off
        if (index < 0)
        {
            IsResetting = false;
            return;
        }

        for (int i = 0; i <= index; i++)
            Commands[i].material = Screen.ErrorMaterial;

        ResetNext();

        void ResetNext()
        {
            if (!_resetBuffer[index])
            {
                Commands[index].material = Screen.DeactivatedCommandMaterial;
                previousState[index] = false;
            }
            else
            {
                previousState[index] = true;
            }

            --index;
            if (index < 0)
            {
                IsResetting = false;
                HandleResetBuffer();
                return;
            }

            Execute.Later(ResetOverTimeDelay, ResetNext);
        }

        void HandleResetBuffer()
        {
            for (int i = 0; i < Commands.Length; i++)
            {
                if (_resetBuffer[i])
                {
                    Commands[i].material = Screen.ActivatedCommandMaterial;
                    previousState[i] = true;
                }
            }
        }
    }
}