using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class OptionDisplay : MonoBehaviour
{
    [Title("Configuration")]
    public TMP_Text LabelText;
    public Image OnImage;
    public Image OffImage;
    public Option Option;
    public Color DeselectedColor;
    public Color SelectedColor;

    public void Select()
    {
        LabelText.SetColor(SelectedColor);
    }

    public void Deselect()
    {
        LabelText.SetColor(DeselectedColor);
    }

    public void SetOption(Option option)
    {
        Option = option;
        SetImages();
        LabelText.text = Option.Label;
    }
    
    public void Toggle()
    {
        Option.Value = !Option.Value;
        SetImages();
    }

    void SetImages()
    {
        if (Option.Value)
        {
            OnImage.gameObject.SetActive(true);
            OffImage.gameObject.SetActive(false);
        }
        else
        {
            OnImage.gameObject.SetActive(false);
            OffImage.gameObject.SetActive(true);
        }
    }
}