using System;
using Cashew.Utility.Async;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

class RoundScoreScreen : MonoBehaviour
{
    bool _readyToExit;
    bool _isRevealing;
    float _nextReveal;
    int _index;

    [Title("Configuration")]
    public RoundScoreScreenController Controller;

    public StratagemSounds Sounds;
    [Space]
    public TMP_Text RoundBonusText;
    public TMP_Text TimeBonusText;
    public TMP_Text PerfectBonusText;
    public TMP_Text TotalScoreText;

    public TMP_Text RoundBonusTextLabel;
    public TMP_Text TimeBonusTextLabel;
    public TMP_Text PerfectBonusTextLabel;
    public TMP_Text TotalScoreTextLabel;

    [Title("Settings")]
    public float TimeBetweenReveals;
    
    [Title("State")]
    public RoundScoreData RoundScore;

    public event Action Complete;

    void Start()
    {
        DisableAll();
    }

    void DisableAll()
    {
        RoundBonusText.enabled = false;
        TimeBonusText.enabled = false;
        PerfectBonusText.enabled = false;
        TotalScoreText.enabled = false;

        RoundBonusTextLabel.enabled = false;
        TimeBonusTextLabel.enabled = false;
        PerfectBonusTextLabel.enabled = false;
        TotalScoreTextLabel.enabled = false;
    }

    public void Exit()
    {
        if (_readyToExit)
        {
            Sounds.MenuConfirm();
            _readyToExit = false;
            DisableAll();
            Complete?.Invoke();
        }
        else
        {
            Sounds.Error();
        }
    }
    
    void Update()
    {
        if (_isRevealing && _nextReveal <= Time.time)
        {
            Reveal(_index);
        }
    }

    public void Display(RoundScoreData score)
    {
        RoundScore = score;
        _isRevealing = true;
        _nextReveal = Time.time + TimeBetweenReveals;
    }

    void Reveal(int index)
    {
        if (index == 0)
        {
            RoundBonusText.text = Format.Number(RoundScore.RoundBonus);
            RoundBonusText.enabled = true;
            RoundBonusTextLabel.enabled = true;
            _nextReveal += TimeBetweenReveals;
            Sounds.RoundEndScreenTextAppears();

            ++_index;
        }
        else if (index == 1)
        {

            TimeBonusText.text = Format.Number(RoundScore.TimeBonus);
            TimeBonusText.enabled = true;
            TimeBonusTextLabel.enabled = true;
            _nextReveal += TimeBetweenReveals;
            if (RoundScore.PerfectBonus > 0)
                Execute.Later(TimeBetweenReveals / 2, Sounds.PerfectRound);
            Sounds.RoundEndScreenTextAppears();

            ++_index;
        }
        else if (index == 2)
        {
            PerfectBonusText.text = Format.Number(RoundScore.PerfectBonus);
            PerfectBonusText.enabled = true;
            PerfectBonusTextLabel.enabled = true;
            _nextReveal += TimeBetweenReveals;
            Sounds.RoundEndScreenTextAppears();

            ++_index;
        }
        else if (index == 3)
        {
            TotalScoreText.text = Format.Number(RoundScore.TotalScore);
            TotalScoreText.enabled = true;
            TotalScoreTextLabel.enabled = true;

            Sounds.RoundEndScreenTextAppears();

            _readyToExit = true;
            _isRevealing = false;
            _index = 0;
        }
    }
}