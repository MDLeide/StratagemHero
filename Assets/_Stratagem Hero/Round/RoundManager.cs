using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

class RoundManager : MonoBehaviour
{
    bool _complete;
    bool _expired;
    bool _stratagemQueued;
    float _stratagemTime;

    [Header("Configuration")]
    public StratagemFilter Filter;
    public StratagemHeroScreen StratagemHeroScreen;
    public StratagemHero Hero;
    public StratagemSounds Sounds;
    public StratagemScoring Score;
    public StratagemInputMonitor InputMonitor;
    public StratagemTimer Timer;

    public int NumberOfUpcomingStratagems;

    [Header("Settings")]
    public bool EndlessMode;
    public float TimeBetweenStratagems;
    public bool UseTimer;
    public float RoundTime;

    [Header("State")]
    public int RoundNumber;
    public int TotalNumberOfStratagems => RoundNumber + 5;
    public int StratagemsCompleted;

    public Stratagem CurrentStratagem;
    public Queue<Stratagem> StratagemQueue = new();

    public event Action<RoundScoreData> RoundComplete;
    public event Action TimeExpired;

    void Start()
    {
        Timer.RoundTimeLimit = RoundTime;
        Timer.LimitlessTime = !UseTimer;
        
        Timer.RoundExpired += OnRoundTimerExpired;

        Hero.BadCommand += OnBadCommand;
        Hero.CorrectCommand += OnCorrectCommand;
        Hero.StratagemComplete += OnStratagemComplete;
    }

    void Update()
    {
        if (_expired || _complete || !enabled)
            return;
        
        if (_stratagemQueued && _stratagemTime <= Time.time)
        {
            if (!EndlessMode && StratagemsCompleted >= TotalNumberOfStratagems)
            {
                OnRoundComplete();
                return;
            }

            SetNewStratagem();
        }

        StratagemHeroScreen.SetTimeRemaining(Timer.RoundTimePercentageRemaining);
    }

    public void Clear()
    {
        _complete = false;
        _expired = false;
        _stratagemQueued = false;
        Score.Clear();
        RoundNumber = 0;
    }

    void SetNewStratagem()
    {
        if (EndlessMode || StratagemsCompleted + StratagemQueue.Count < TotalNumberOfStratagems)
            StratagemQueue.Enqueue(Filter.GetNextStratagem());

        SetStratagem(StratagemQueue.Dequeue());
        StratagemHeroScreen.SetUpcoming(StratagemQueue.ToArray());
        Timer.BeginNewStratagem();
        _stratagemQueued = false;
    }

    void OnRoundComplete()
    {
        _complete = true;
        var inputData = InputMonitor.RoundComplete();
        var time = Timer.OnRoundComplete();
        var score = Score.RoundComplete(inputData.Perfect, time.TimeRemainingPercentage);
        InputMonitor.RoundComplete();
        RoundComplete?.Invoke(score);
        StratagemsCompleted = 0;
        Sounds.RoundComplete();
    }
    
    public void BeginNewRound()
    {
        enabled = true;
        ++RoundNumber;

        _expired = false;
        _complete = false;
        StratagemQueue.Clear();
        var next = Filter.GetNextStratagem();
        var onDeck = Math.Min(TotalNumberOfStratagems - 1, NumberOfUpcomingStratagems);
        for (int i = 0; i < onDeck; i++)
            StratagemQueue.Enqueue(Filter.GetNextStratagem());

        SetStratagem(next);
        Timer.BeginNewRound();
        Score.BeginNewRound(RoundNumber);
        StratagemHeroScreen.SetUpcoming(StratagemQueue.ToArray());
        StratagemHeroScreen.SetScore(Score.CurrentRound.TotalScore);
        StratagemHeroScreen.SetRound(RoundNumber);
    }

    void OnStratagemComplete()
    {
        Score.StratagemComplete(CurrentStratagem);
        StratagemHeroScreen.SetScore(Score.CurrentRound.TotalScore);
        Sounds.StratagemComplete();
        Timer.StratagemComplete();
        ++StratagemsCompleted;
        _stratagemQueued = true;
        _stratagemTime = Time.time + TimeBetweenStratagems;
    }

    void OnCorrectCommand(int index)
    {
        StratagemHeroScreen.SetCommandActivated(index);
        Sounds.CorrectCommand();
    }

    void OnBadCommand(int index)
    {
        StratagemHeroScreen.ShakeCommands();
        StratagemHeroScreen.ResetCommands();
        Sounds.BadCommand();
    }
    
    void SetStratagem(Stratagem strat)
    {
        CurrentStratagem = strat;
        Hero.SetNewStratagem(strat);
        StratagemHeroScreen.SetNewStratagem(strat);
    }

    void OnRoundTimerExpired()
    {
        Score.RoundComplete(false, 0f);
        TimeExpired?.Invoke();
    }
}