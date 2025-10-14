using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AccessOptions : UI_SettingsMenu
{
    [Header("Accessibility")]
    [SerializeField] private TMP_Dropdown cbModeDropdown;
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private Toggle highContrastToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnCBModeChanged(int index)
    {
        switch (index)
        {
            case 0: SetCBMode(); break;
            case 1: SetCBMode(); break;
            case 2: SetCBMode(); break;
        }
    }

    protected void SetCBMode()
    {

    }

    protected void OnLevelWasLoaded(int index)
    {
        switch (index)
        {
            case 0: SetLanguage(); break;
            case 1: SetLanguage(); break;
            case 2: SetLanguage(); break;
        }
    }

    protected void SetLanguage()
    {

    }

    protected void OnContrastModeChanged(bool value)
    {
        SetContrastMode();
    }

    protected void SetContrastMode()
    {

    }
}
