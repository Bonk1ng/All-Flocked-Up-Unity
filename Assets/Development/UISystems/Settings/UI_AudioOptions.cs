using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AudioOptions : UI_SettingsMenu
{
    [Header("Audio")]
    [SerializeField] private Slider mainVolSlider;
    [SerializeField] private TextMeshProUGUI mainVolText;
    [SerializeField]private Slider sfxVolSlider;
    [SerializeField] private TextMeshProUGUI sfxVolText;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private TextMeshProUGUI musicVolText;
    [SerializeField] private Slider dialogueVolSlider;
    [SerializeField] private TextMeshProUGUI dialogueVolText;
    [SerializeField] private Toggle focusMuteToggle;
    [SerializeField] private TMP_Dropdown outputDropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnMainVolChanged(float value)
    {

    }

    protected void SetMainVol()
    {

    }

    protected void SetMainVolText()
    {

    }

    protected void OnSFXVolChanged(float value) 
    {
    
    }

    protected void SetSFXVol()
    {

    }

    protected void SetSFXVolText()
    {

    }

    protected void OnMusicVolChanged(float value)
    {

    }

    protected void SetMusicVol()
    {

    }

    protected void SetMusicVolText()
    {

    }

    protected void OnDialogueVolChanged(float value)
    {

    }

    protected void SetDialogueVol()
    {

    }

    protected void SetDialogueVolText()
    {

    }

    protected void OnFocusMuteChanged(bool value)
    {

    }

    protected void SetFocusMuteState()
    {

    }

    protected void OnOutputChanged(int index)
    {
        switch (index)
        {
            case 0: SetOutputSource(); break;
            case 1: SetOutputSource(); break;
            case 2: SetOutputSource(); break;
        }
    }

    protected void SetOutputSource()
    {

    }
}
