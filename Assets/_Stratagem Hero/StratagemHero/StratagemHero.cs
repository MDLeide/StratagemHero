using System;
using Sirenix.OdinInspector;
using UnityEngine;

class StratagemHero : MonoBehaviour
{
    bool _isComplete = true;
    int _index;
    
    [Title("State")]
    public Stratagem CurrentStratagem { get; private set; }
    public bool[] State { get; private set; }

    public event Action<int> BadCommand;
    public event Action<int> CorrectCommand;
    public event Action StratagemComplete;
    
    public void SetNewStratagem(Stratagem strat)
    {
        CurrentStratagem = strat;
        _isComplete = false;
        _index = 0;
        State = new bool[CurrentStratagem.Commands.Length];
    }

    public void ProcessInput(CommandKey input)
    {
        if (_isComplete)
            return;

        var correct = CurrentStratagem.Commands[_index] == input;
        if (correct)
        {
            State[_index] = true;
            ++_index;

            if (_index >= CurrentStratagem.Commands.Length)
            {
                _isComplete = true;
                StratagemComplete?.Invoke();
            }
            
            CorrectCommand?.Invoke(_index - 1);
        }
        else
        {
            for (int i = 0; i < State.Length; i++)
                State[i] = false;

            var original = _index;
            _index = 0;
            BadCommand?.Invoke(original);
        }
    }
}