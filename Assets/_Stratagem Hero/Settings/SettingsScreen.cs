using Sirenix.OdinInspector;
using UnityEngine;


class SettingsScreen : MonoBehaviour
{
    int _selectedIndex;

    OptionDisplay[] _displays;

    [Title("Configuration")]
    public SettingsScreenController Controller;
    [Space]
    public OptionDisplay SoundOption;
    public OptionDisplay BloomOption;
    public OptionDisplay ShakeOption;

    [Space]
    public MainMenuScreen MainMenu;

    [Title("Settings")]
    public Settings Settings;

    void Start()
    {
        SoundOption.SetOption(Settings.SoundOn);
        BloomOption.SetOption(Settings.BloomOn);
        ShakeOption.SetOption(Settings.ShakeOn);

        _displays = new[]
        {
            SoundOption,
            BloomOption,
            ShakeOption
        };
    }

    public void Down()
    {
        UpdateSelection(_selectedIndex + 1);
    }

    public void Up()
    {
        UpdateSelection(_selectedIndex - 1);
    }

    public void Toggle()
    {
        _displays[_selectedIndex].Toggle();
    }

    public void Quit()
    {
        Controller.enabled = false;
        gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        MainMenu.Controller.enabled = true;
    }

    void UpdateSelection(int newIndex)
    {
        if (newIndex < 0)
            newIndex = _displays.Length - 1;
        else if (newIndex >= _displays.Length)
            newIndex = 0;

        _displays[_selectedIndex].Deselect();
        _selectedIndex = newIndex;
        _displays[_selectedIndex].Select();
    }
}