using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
class StratagemTime
{
    [Title("State")]
    public bool IsComplete;
    public float StratagemStartTime;
    public float StratagemEndTime;
    
    [Title("Calculations")]
    public float StratagemTotalTime => (IsComplete ? StratagemEndTime : Time.time) - StratagemStartTime;
}