using UnityEngine;
using UnityEngine.UI;

class StratagemHeroScreenColorComponent : MonoBehaviour
{
    bool _lowTimeTriggered;

    public StratagemHeroScreen Screen;

    public Image NameBar;
    public Image ProgressBar;
    public Image[] IconBorder;

    public float LowTimeThreshold = .2f;

    public void SetTime(float timeRemainingPercentage)
    {
        if (timeRemainingPercentage <= LowTimeThreshold && !_lowTimeTriggered)
        {
            _lowTimeTriggered = true;
            SetLowTimeColors();
        }
        else if (timeRemainingPercentage > LowTimeThreshold && _lowTimeTriggered)
        {
            _lowTimeTriggered = false;
            SetNormalTimeColors();
        }
    }

    void SetLowTimeColors()
    {
        NameBar.material = Screen.ErrorMaterial;
        ProgressBar.material = Screen.ErrorMaterial;
        for (int i = 0; i < IconBorder.Length; i++)
            IconBorder[i].material = Screen.ErrorMaterial;
    }

    void SetNormalTimeColors()
    {
        NameBar.material = Screen.ActivatedCommandMaterial;
        ProgressBar.material = Screen.ActivatedCommandMaterial;
        for (int i = 0; i < IconBorder.Length; i++)
            IconBorder[i].material = Screen.ActivatedCommandMaterial;
    }
}