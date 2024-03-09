class RoundScoreData
{
    public int Round;
    public int RoundBonus;
    public int TimeBonus;
    public int PerfectBonus;
    public int TotalBonus => RoundBonus + TimeBonus + PerfectBonus;
    public int RoundScore;
    public int TotalRoundScore => TotalBonus + RoundScore;
    public int TotalScore;
}