using UnityEngine;

class StratagemInputMonitor : MonoBehaviour
{
    StratagemRoundInputData _data;

    public StratagemHero Hero;
    public bool IsPerfect => _data.Perfect;

    void Start()
    {
        Hero.StratagemComplete += OnStratagemComplete;
        Hero.BadCommand += OnBadCommand;
        Hero.CorrectCommand += OnCorrectCommand;
        _data = new StratagemRoundInputData();
    }

    void OnCorrectCommand(int obj)
    {
        ++_data.GoodInputs;
    }

    void OnBadCommand(int obj)
    {
        ++_data.BadInputs;
    }

    void OnStratagemComplete()
    {
        ++_data.TotalStratagems;
    }

    public StratagemRoundInputData RoundComplete()
    {
        var returnData =  _data;
        _data = new StratagemRoundInputData();
        return returnData;
    }
}