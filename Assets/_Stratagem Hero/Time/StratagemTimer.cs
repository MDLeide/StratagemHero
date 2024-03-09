using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

class StratagemTimer : MonoBehaviour
{
    bool _roundComplete;
    bool _expired;

    [Title("Settings")]
    public float RoundTimeLimit;
    public float StratagemExtraTime = 1;
    public bool LimitlessTime;

    [Title("State")]
    public SessionTime ActiveSession;
    public RoundTime ActiveRound;
    public StratagemTime ActiveStratagem;
    public float TimeRemaining;

    [Title("Calculations")]
    public float RoundTimePercentageRemaining
    {
        get
        {
            if (LimitlessTime)
                return 1;

            return TimeRemaining / RoundTimeLimit;
        }
    }
    
    public event Action RoundExpired;
    
    void Update()
    {
        if (_expired || _roundComplete)
            return;

        TimeRemaining -= Time.deltaTime;

        if (!LimitlessTime && TimeRemaining <= 0)
        {
            _expired = true;
            OnRoundComplete();
            RoundExpired?.Invoke();
        }
    }

    public void BeginNewSession()
    {
        ActiveSession = new SessionTime();
        ActiveSession.SessionStartTime = Time.time;
    }

    public void BeginNewRound()
    {
        _roundComplete = false;
        _expired = false;
        TimeRemaining = RoundTimeLimit;
        ActiveRound = new RoundTime();
        ActiveRound.RoundStartTime = Time.time;
        ActiveRound.RoundLimit = RoundTimeLimit;
    }
    public void BeginNewStratagem()
    {
        ActiveStratagem = new StratagemTime();
        ActiveStratagem.StratagemStartTime = Time.time;
    }

    public void StratagemComplete()
    {
        AddBonusTime();
        CompleteActiveStratagem();
    }

    public RoundTime OnRoundComplete()
    {
        _roundComplete = true;
        ActiveRound.RoundEndTime = Time.time;
        ActiveRound.IsComplete = true;
        ActiveRound.TimeRemaining = TimeRemaining;
        ActiveSession.RoundTimes.Add(ActiveRound);
        var returnTime = ActiveRound;
        ActiveRound = null;
        return returnTime;
    }

    public SessionTime SessionEnded()
    {
        ActiveSession.SessionEndTime = Time.time;
        ActiveSession.IsComplete = true;
        var session = ActiveSession;
        ActiveSession = null;
        return session;
    }

    void CompleteActiveStratagem()
    {
        ActiveStratagem.StratagemEndTime = Time.time;
        ActiveStratagem.IsComplete = true;
        ActiveRound.StratagemTimes.Add(ActiveStratagem);
        ActiveStratagem = null;
    }

    void AddBonusTime()
    {
        TimeRemaining += StratagemExtraTime;
        if (TimeRemaining > RoundTimeLimit)
            TimeRemaining = RoundTimeLimit;
    }
}