using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

class MainMenuScreen : MonoBehaviour
{
    int _selectedIndex;
    TMP_Text[] _allTexts;

    [Title("Configuration")]
    public MainMenuScreenController Controller;
    public StratagemSounds Sounds;
    [Space]
    public TMP_Text PlayText;
    public TMP_Text SettingsText;
    public TMP_Text QuitText;
    [Space]
    public SessionManager SessionManager;
    public SettingsScreen SettingsScreen;

    [Title("Settings")]
    public Color SelectedColor;
    public Color DeselectedColor;

    void Start()
    {
        _allTexts = new[]
        {
            PlayText,
            QuitText
        };

        _allTexts[0].SetColor(SelectedColor);
    }

    public void Up()
    {
        SetNewIndex(_selectedIndex - 1);
        Sounds.MenuSelect();
    }

    public void Down()
    {
        SetNewIndex(_selectedIndex + 1);
        Sounds.MenuSelect();
    }

    public void Confirm()
    {
        switch (_selectedIndex)
        {
            case 0: // Play
                Sounds.MenuConfirm();
                gameObject.SetActive(false);
                Controller.enabled = false;
                SessionManager.BeginNewSession();
                break;
            //case 1: // Settings
            //    gameObject.SetActive(false);
            //    Controller.enabled = false; 
            //    SettingsScreen.gameObject.SetActive(true);
            //    SettingsScreen.Controller.enabled = true;
            //    break;
            case 1: // Quit
                Application.Quit();
                break;
        }
    }

    void SetNewIndex(int index)
    {
        _allTexts[_selectedIndex].SetColor(DeselectedColor);

        _selectedIndex = index;
        if (_selectedIndex < 0)
            _selectedIndex = _allTexts.Length - 1;
        if (_selectedIndex >= _allTexts.Length)
            _selectedIndex = 0;

        _allTexts[_selectedIndex].SetColor(SelectedColor);
    }
}