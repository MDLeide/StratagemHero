using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

class StratagemScoring : MonoBehaviour
{
    [Title("Settings")]
    public int ScorePerCommand = 5;

    public int PerfectBonus = 100;
    public int PossibleTimeBonus = 100;
    public int RoundBonus = 50;

    [Title("State")]
    public RoundScoreData CurrentRound;
    
    public void StratagemComplete(Stratagem stratagem)
    {
        CurrentRound.RoundScore += stratagem.Commands.Length * ScorePerCommand;
        CurrentRound.TotalScore += stratagem.Commands.Length * ScorePerCommand;
    }

    public void BeginNewRound(int round)
    {
        var r = new RoundScoreData();
        if (CurrentRound != null)
            r.TotalScore = CurrentRound.TotalScore;
        CurrentRound = r;
        CurrentRound.Round = round;
    }

    public void Clear()
    {
        CurrentRound = new RoundScoreData();
        CurrentRound.Round = 1;
    }

    public RoundScoreData RoundComplete(bool perfect, float percentTimeRemaining)
    {
        CurrentRound.RoundBonus = RoundBonus * CurrentRound.Round;
        CurrentRound.PerfectBonus = perfect ? PerfectBonus : 0;
        CurrentRound.TimeBonus = (int)(percentTimeRemaining * PossibleTimeBonus);
        CurrentRound.TotalScore += CurrentRound.TotalBonus;
        return CurrentRound;
    }
}