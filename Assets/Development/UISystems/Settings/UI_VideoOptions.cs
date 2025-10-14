using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_VideoOptions : UI_SettingsMenu
{
    [Header("Video")]
    [SerializeField] private TMP_Dropdown resolDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown AADropdown;
    [SerializeField] private TMP_Dropdown frameLimitDropdown;
    [SerializeField] private TMP_Dropdown textureQualDropdown;
    [SerializeField] private TMP_Dropdown shadowQualDropdown;
    [SerializeField] private Slider renderDisSlider;
    [SerializeField] private TextMeshProUGUI renderDisText;
    [SerializeField] private Slider camSensSlider;
    [SerializeField] private TextMeshProUGUI camSensText;
    [SerializeField] private Slider fovSlider;
    [SerializeField] private TextMeshProUGUI fovText;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Toggle invertCamToggle;
    [SerializeField] private bool isFullScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnResolutionChanged(int index)
    {
        switch (index)
        {
            case 0: SetResolution(1920,1080,true); break;
            case 1: break;
            case 2: break;  
        }
    }

    protected void SetResolution(int width, int height, bool fullscreen)
    {

    }

    protected void OnFullScreenChanged(int index) 
    {
        switch (index)
        {
            case 0: SetIsFullscreen(); break;
            case 1: SetIsFullscreen(); break;
            case 2: SetIsFullscreen(); break;
        }
    }

    protected void SetIsFullscreen()
    {

    }

    protected void OnQualityChanged(int index)
    {
        switch (index)
        {
            case 0: SetQuality(); break;
            case 1: SetQuality(); break;
            case 2: SetQuality(); break;
        }
    }

    protected void SetQuality()
    {

    }

    protected void OnAAChanged(int index)
    {
        switch (index)
        {
            case 0: SetAAQuality(); break;
            case 1: SetAAQuality(); break;
            case 2: SetAAQuality(); break;
        }
    }

    protected void SetAAQuality()
    {

    }

    protected void OnFrameLimitChanged(int index)
    {
        switch (index)
        {
            case 0: SetFrameLimit(); break;
            case 1: SetFrameLimit(); break;
            case 2: SetFrameLimit(); break;
        }
    }

    protected void SetFrameLimit()
    {

    }

    protected void OnTextureQualityChanged(int index)
    {
        switch (index)
        {
            case 0: SetTextureQuality(); break;
            case 1: SetTextureQuality(); break;
            case 2: SetTextureQuality(); break;
        }
    }

    protected void SetTextureQuality()
    {

    }

    protected void OnShadowQualityChanged(int index)
    {
        switch (index)
        {
            case 0: SetShadowQuality(); break;
            case 1: SetShadowQuality(); break;
            case 2: SetShadowQuality(); break;
        }
    }

    protected void SetShadowQuality()
    {

    }

    protected void OnRenderDistanceChanged(float value)
    {
        SetRenderDistance(value);
    }

    protected void SetRenderDistance(float value)
    {

    }

    protected void SetRenderDistanceText()
    {

    }

    protected void OnCamSensitivityChanged(float value)
    {
        SetCamSensitivity(value);
    }

    protected void SetCamSensitivity(float value)
    {

    }

    protected void SetCamSensitivityText()
    {

    }

    protected void OnFOVChanged(float value)
    {
        SetCameraFOV(value);
    }

    protected void SetCameraFOV(float value)
    {

    }

    protected void SetCameraFOVText()
    {

    }

    protected void OnVsyncToggled(bool value)
    {
        SetVsyncState(value);
    }

    protected void SetVsyncState(bool value)
    {
        
    }

    protected void OnInvertCamToggled(bool value)
    {
        SetInvertCamState(value);
    }

    protected void SetInvertCamState(bool value)
    {

    }
}
