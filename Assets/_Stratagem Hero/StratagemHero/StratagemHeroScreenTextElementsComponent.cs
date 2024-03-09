using TMPro;
using UnityEngine;

class StratagemHeroScreenTextElementsComponent : MonoBehaviour
{
    public StratagemHeroScreen Screen;

    public TMP_Text NameText;
    public TMP_Text ScoreText;
    public TMP_Text RoundText;


    public void SetName(string n)
    {
        NameText.text = n;
    }

    public void SetScore(int score)
    {
        ScoreText.text = Format.Number(score);
    }

    public void SetRound(int round)
    {
        RoundText.text = Format.Number(round);
    }
}