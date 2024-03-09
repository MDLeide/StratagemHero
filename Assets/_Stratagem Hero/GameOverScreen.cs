using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cashew.Utility.Async;
using DigitalRuby.Tween;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

class GameOverScreen : MonoBehaviour
{
    bool _fullyFaded;

    [Title("Configuration")]
    public GameOverScreenController Controller;
    public StratagemSounds Sounds;
    [Space]
    public TMP_Text GameOverLabel;

    public TMP_Text FinalScoreLabel;
    public TMP_Text FinalScoreText;
    public TMP_Text TryAgainLabel;
    public TMP_Text YesText;
    public TMP_Text NoText;

    [Space]
    public SessionManager SessionManager;

    public MainMenuScreen MainMenu;
    
    [Title("Settings")]
    public Color SelectedColor;
    public Color UnselectedColor;
    public float FadeTime;
    public float FadeDelay;

    public bool YesIsSelected;

    void Start()
    {
        ResetAll();
    }

    public void Confirm()
    {
        if (!_fullyFaded)
            return;

        Sounds.MenuConfirm();
        if (YesIsSelected)
        {
            gameObject.SetActive(false);
            Controller.enabled = false;
            SessionManager.BeginNewSession();
        }
        else
        {
            gameObject.SetActive(false);
            Controller.enabled = false;
            MainMenu.gameObject.SetActive(true);
            MainMenu.Controller.enabled = true;
        }
    }

    public void SwitchSelection()
    {
        Sounds.MenuSelect();
        YesIsSelected = !YesIsSelected;
        if (YesIsSelected)
        {
            YesText.SetColor(SelectedColor);
            NoText.SetColor(UnselectedColor);
        }
        else
        {
            YesText.SetColor(UnselectedColor);
            NoText.SetColor(SelectedColor);
        }
    }

    void ResetAll()
    {
        GameOverLabel.SetAlpha(0);
        FinalScoreLabel.SetAlpha(0);
        FinalScoreText.SetAlpha(0); 
        TryAgainLabel.SetAlpha(0);

        YesText.SetColor(UnselectedColor);
        NoText.SetColor(UnselectedColor);

        YesText.SetAlpha(0);
        NoText.SetAlpha(0);
    }



    public void ShowScreen(int score)
    {
        ResetAll();
        FinalScoreText.text = Format.Number(score);

        // start fading in the gameover screen
        TweenFactory.Tween(
            GameOverLabel,
            0,
            1,
            FadeTime,
            TweenScaleFunctions.CubicEaseIn,
            p => GameOverLabel.SetAlpha(p.CurrentValue),
            p =>
            {
                Sounds.GameOverScreenTextAppears();
                Execute.Later(FadeDelay, FadeScore);
            });
    }

    void FadeScore()
    {
        TweenFactory.Tween(
            FinalScoreLabel,
            0,
            1,
            FadeTime,
            TweenScaleFunctions.CubicEaseIn,
            p =>
            {
                Sounds.GameOverScreenTextAppears();
                FinalScoreLabel.SetAlpha(p.CurrentValue);
                FinalScoreText.SetAlpha(p.CurrentValue);
            },
            p => Execute.Later(FadeDelay, FadeTryAgain));
    }

    void FadeTryAgain()
    {
        TweenFactory.Tween(
            FinalScoreLabel,
            0,
            1,
            FadeTime,
            TweenScaleFunctions.CubicEaseIn,
            p =>
            {
                TryAgainLabel.SetAlpha(p.CurrentValue);
                YesText.SetAlpha(p.CurrentValue);
                NoText.SetAlpha(p.CurrentValue);
                YesText.color = SelectedColor;
                YesIsSelected = true;
            },
            p =>
            {
                Sounds.GameOverScreenTextAppears();
                _fullyFaded = true;
            });
    }
}