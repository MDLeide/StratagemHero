using System;
using Cashew.Utility;
using Cashew.Utility.Async;
using Cashew.Utility.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

class SessionManager : MonoBehaviour
{
    [Title("Configuration")]
    public RoundManager RoundManager;
    public StratagemSounds Sounds;
    public StratagemHeroScreen StratagemHeroScreen;
    public RoundScoreScreen RoundScoreScreen;
    public GameOverScreen GameOverScreen;
    public StratagemTimer Timer;
    
    void Start()
    {
        RoundManager.RoundComplete += OnRoundComplete;
        RoundManager.TimeExpired += OnTimeExpired;
        RoundScoreScreen.Complete += OnScoreScreenComplete;
    }

    public void BeginNewSession()
    {
        RoundManager.Clear();
        Sounds.PlayBegin();
        Timer.BeginNewSession();
        StartNewRound();
    }

    void OnTimeExpired()
    {
        Timer.SessionEnded();
        TurnOffHeroScreen();
        ShowGameOverScreen();
    }

    void OnRoundComplete(RoundScoreData score)
    {
        TurnOffHeroScreen();
        TurnOnRoundScoreScreen(score);
    }

    void OnScoreScreenComplete()
    {
        TurnOffRoundScoreScreen();
        StartNewRound();
    }

    void TurnOffRoundScoreScreen()
    {
        RoundScoreScreen.gameObject.SetActive(false);
        RoundScoreScreen.Controller.enabled = false;
    }

    void TurnOnRoundScoreScreen(RoundScoreData score)
    {
        RoundScoreScreen.gameObject.SetActive(true);
        RoundScoreScreen.Controller.enabled = true;

        RoundScoreScreen.Display(score);
    }

    void TurnOffHeroScreen()
    {
        StratagemHeroScreen.Controller.enabled = false;
        StratagemHeroScreen.gameObject.SetActive(false);
    }

    void StartNewRound()
    {
        StratagemHeroScreen.gameObject.SetActive(true);
        StratagemHeroScreen.Controller.enabled = true;

        RoundManager.BeginNewRound();
    }

    void ShowGameOverScreen()
    {
        GameOverScreen.gameObject.SetActive(true);
        GameOverScreen.Controller.enabled = true;

        Sounds.GameOver();
        GameOverScreen.ShowScreen(RoundManager.Score.CurrentRound.TotalScore);
    }
}