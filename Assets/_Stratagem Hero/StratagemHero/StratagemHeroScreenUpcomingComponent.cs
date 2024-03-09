using Cashew.Utility;
using Cashew.Utility.Extensions;
using UnityEngine;
using UnityEngine.UI;

class StratagemHeroScreenUpcomingComponent : MonoBehaviour
{
    public Image[] UpcomingIcons;

    public void SetUpcoming(Stratagem[] upcoming)
    {
        for (int i = 0; i < upcoming.Length; i++)
        {
            UpcomingIcons[i].gameObject.SetActive(true);
            UpcomingIcons[i].sprite = upcoming[i].Icon;
        }

        for (int i = upcoming.Length; i < UpcomingIcons.Length; i++)
            UpcomingIcons[i].gameObject.SetActive(false);
    }
}