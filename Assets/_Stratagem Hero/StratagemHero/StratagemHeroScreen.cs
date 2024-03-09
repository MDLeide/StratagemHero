using System;
using System.Linq;
using Cashew.Utility.Async;
using Cashew.Utility.Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class StratagemHeroScreen : MonoBehaviour
{
    bool[] _state;

    [Title("Configuration")]
    public StratagemHeroController Controller;

    [Space]
    public StratagemHeroScreenUpcomingComponent Upcoming;

    public StratagemHeroScreenResetComponent Reset;
    public StratagemHeroScreenTextElementsComponent Text;
    public StratagemHeroScreenColorComponent Color;
    public StratagemHeroScreenShakeComponent Shake;
    public CommandSprites Sprites;
    [Space]
    public Image MainIcon;
    public Image[] Commands;
    [Space]
    public RectTransform ProgressScalar;

    [Header("Sprites")]
    public Material ActivatedCommandMaterial;
    public Material DeactivatedCommandMaterial;
    public Material ErrorMaterial;
    
    [Title("State")]
    public Stratagem Stratagem;

    void Start()
    {
        SetRound(1);
        SetScore(0);
        _state = new bool[Commands.Length];
    }

    public void SetCommandActivated(int cmdIndex)
    {
        if (Reset.IsResetting)
        {
            Reset.AddToResetBuffer(cmdIndex);
            return;
        }

        _state[cmdIndex] = true;
        Commands[cmdIndex].material = ActivatedCommandMaterial;
    }

    public void SetTimeRemaining(float timeRemainingPercentage)
    {
        ProgressScalar.localScale = ProgressScalar.localScale.WithNewX(timeRemainingPercentage);
        Color.SetTime(timeRemainingPercentage);
    }
    
    public void SetNewStratagem(Stratagem stratagem)
    {
        ClearState();

        Stratagem = stratagem;
        Text.SetName(stratagem.name);
        MainIcon.sprite = stratagem.Icon;
        for (int i = 0; i < stratagem.Commands.Length; i++)
        {
            Commands[i].transform.parent.gameObject.SetActive(true);
            Commands[i].sprite = Sprites.GetSprite(stratagem.Commands[i]);
            Commands[i].material = DeactivatedCommandMaterial;
        }

        for (int i = stratagem.Commands.Length; i < Commands.Length; i++)
            Commands[i].transform.parent.gameObject.SetActive(false);
    }

    void ClearState()
    {
        if (_state == null)
            _state = new bool[Commands.Length];
        for (int i = 0; i < _state.Length; i++)
            _state[i] = false;
    }

    public void SetScore(int score) => Text.SetScore(score);
    public void SetRound(int round) => Text.SetRound(round);
    public void SetUpcoming(Stratagem[] upcoming) => Upcoming.SetUpcoming(upcoming);
    public void ShakeCommands() => Shake.Shake();
    public void ResetCommands() => Reset.ResetCommands(_state);
}