using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
class SessionTime
{
    [Title("State")]
    public bool IsComplete;
    public float SessionStartTime;
    public float SessionEndTime;
    public List<RoundTime> RoundTimes = new();

    [Title("Calculations")]
    public float SessionTotalTime => (IsComplete ? SessionEndTime : Time.time) - SessionStartTime;
    public float AverageRoundTime => RoundTimes.Sum(p => p.RoundTotalTime) / RoundTimes.Count;
    public float AverageStratagemTime => RoundTimes.SelectMany(p => p.StratagemTimes).Sum(p => p.StratagemTotalTime) /
                                         RoundTimes.SelectMany(p => p.StratagemTimes).Count();
}