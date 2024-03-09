using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
class RoundTime
{
    [Title("Settings")]
    public float RoundLimit;

    [Title("State")]
    public bool IsComplete;
    public float RoundStartTime;
    public float RoundEndTime;
    public float TimeRemaining;
    public List<StratagemTime> StratagemTimes = new();

    [Title("Calculations")]
    public float RoundTotalTime => (IsComplete ? RoundEndTime : Time.time) - RoundStartTime;
    public float AverageStratagemTime => StratagemTimes.Sum(p => p.StratagemTotalTime) / StratagemTimes.Count;
    public float TimeRemainingPercentage => TimeRemaining / RoundLimit;
}