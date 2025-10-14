using NUnit.Framework;
using System;
using System.Linq;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_VideoOptions : UI_SettingsMenu
{
    [Header("Video")]
    [SerializeField] private TMP_Dropdown resolDropdown;
    [SerializeField] private Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown fsDropDown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private string[] qualityLevels;
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
    [SerializeField] private Camera mainCameraRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var cams = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        foreach (var cam in cams)
        {
            if (cam.CompareTag("MainCamera"))
            {
                mainCameraRef = cam;
            } 
        }
    }

    void Start()
    {
        //Init Menus
        InitResolutionDD();
        InitFullscreenDD();
        InitQualityDD();
        InitAAQualityDD();
        InitFrameLimitDD();
        InitTextureQualityDD();
        InitShadowQualityDD();

        //loads settings if found in PlayerPrefs
        if (PlayerPrefs.HasKey("RenderDistance"))
            SetRenderDistance(PlayerPrefs.GetFloat("RenderDistance"));
        if (PlayerPrefs.HasKey("FOV"))
            SetCameraFOV(PlayerPrefs.GetFloat("CameraFOV"));
        if (PlayerPrefs.HasKey("CameraSensitivity"))
            SetCamSensitivity(PlayerPrefs.GetFloat("CameraSensitivity"));
        if (PlayerPrefs.HasKey("Vsync"))
            SetVsyncState(PlayerPrefs.GetInt("Vsync") == 1);
        if (PlayerPrefs.HasKey("AAQuality"))
            SetAAQuality(PlayerPrefs.GetInt("AA_Quality"));
        if (PlayerPrefs.HasKey("ResolutionIndex"))
            SetResolution(PlayerPrefs.GetInt("ResolutionIndex"),true);
        if (PlayerPrefs.HasKey("FullscreenMode"))
            OnFullScreenChanged(PlayerPrefs.GetInt("FullscreenMode"));
        if (PlayerPrefs.HasKey("QualityLevel"))
            SetQuality(PlayerPrefs.GetInt("QualityLevel"));
        if (PlayerPrefs.HasKey("TextureQuality"))
            SetTextureQuality(PlayerPrefs.GetInt("TextureQuality"));
        if (PlayerPrefs.HasKey("ShadowQuality"))
            SetShadowQuality(PlayerPrefs.GetInt("ShadowQuality"));

    }

    protected void InitResolutionDD()
    {
        resolutions = Screen.resolutions;
        resolDropdown.ClearOptions();
        var options = new List<string>();
        int currentIndex = 0;
        for (int i = 0;i < resolutions.Length; i++)
        {
            string option = resolutions[i].width+ " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
                SetResolution(currentIndex, isFullScreen);
            }
        }
        resolDropdown.AddOptions(options);
        resolDropdown.value = currentIndex;
        resolDropdown.RefreshShownValue();
        resolDropdown.onValueChanged.AddListener(OnResolutionChanged);


    }
    protected void OnResolutionChanged(int index)
    {
        switch (index)
        {
            case 0: SetResolution(index,true); break;
            case 1: SetResolution(index, true); break;
            case 2: SetResolution(index, true); break;
            case 3: SetResolution(index,true); break;
            case 4: SetResolution(index,true); break;
            case 5: SetResolution(index, true);break;
        }
    }

    protected void SetResolution(int index, bool fullscreen)
    {
        var res = resolutions[index];
        Screen.SetResolution(res.width, res.height,fullscreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
        
    }

    protected void InitFullscreenDD()
    {
        fsDropDown.ClearOptions();
        var modes = new List<string>
    {
        "Exclusive Fullscreen",
        "Borderless Window",
        "Windowed",
        "Maximized Window"
    };

        fsDropDown.AddOptions(modes);

        int currentMode = (int)Screen.fullScreenMode;
        fsDropDown.value = currentMode;
        fsDropDown.RefreshShownValue();

        fsDropDown.onValueChanged.AddListener(OnFullScreenChanged);

    }

    protected void OnFullScreenChanged(int index) 
    {
        switch (index)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 2: Screen.fullScreenMode = FullScreenMode.Windowed; break;
            case 3: Screen.fullScreenMode = FullScreenMode.MaximizedWindow; break;
        }

        isFullScreen = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen ||
               Screen.fullScreenMode == FullScreenMode.FullScreenWindow;

        PlayerPrefs.SetInt("FullscreenMode",index);
        PlayerPrefs.Save();
    }

    protected void InitQualityDD()
    {
        qualityDropdown.ClearOptions();
        var qualityNames = QualitySettings.names;
        qualityDropdown.AddOptions(new List<string>(qualityNames));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
    }

    protected void OnQualityChanged(int index)
    {
        SetQuality(index);
    }

    protected void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
        PlayerPrefs.SetInt("QualityLevel", index);
        PlayerPrefs.Save();
    }

    protected void InitAAQualityDD()
    {
        AADropdown.ClearOptions();
        AADropdown.AddOptions(new List<string> {"None", "FXAA","SMAA" });

        var camData = mainCameraRef.GetComponent<UniversalAdditionalCameraData>();
        int index = 0;
        if(camData != null)
        {
            index = (int)camData.antialiasing;
        }
        AADropdown.value = index;
        AADropdown.RefreshShownValue();
        AADropdown.onValueChanged.AddListener(OnAAChanged);
    }
    protected void OnAAChanged(int index)
    {
        SetAAQuality(index);
    }

    protected void SetAAQuality(int index)
    {
        var camData = mainCameraRef.GetComponent<UniversalAdditionalCameraData>();
        if (camData == null) return;
        switch (index)
        {
            case 0: camData.antialiasing = AntialiasingMode.None; break;
            case 1: camData.antialiasing = AntialiasingMode.FastApproximateAntialiasing; break;
            case 2: camData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing; break;
        }
        PlayerPrefs.SetInt("AAQuality", index);
        PlayerPrefs.Save();

    }

    protected void InitFrameLimitDD()
    {
        frameLimitDropdown.ClearOptions();
        var options = new List<string>()
        {
            "30FPS",
            "60FPS",
            "120FPS",
            "Unlimited"
        };
        frameLimitDropdown.AddOptions(options);

        int index = PlayerPrefs.GetInt("FrameLimit", 1);
        frameLimitDropdown.value = index;
        frameLimitDropdown.RefreshShownValue();
        ApplyFrameLimit(index);
        frameLimitDropdown.onValueChanged.AddListener(OnFrameLimitChanged);
    }

    protected void OnFrameLimitChanged(int index)
    {
        SetFrameLimit(index);
    }

    protected void ApplyFrameLimit(int rate)
    {
        int targetRate = 60;
        switch (rate)
        {
            case 0: targetRate = 30; break;
            case 1: targetRate = 60; break;
            case 2: targetRate = 90; break;
            case 3: targetRate = 120; break;
            case 4: targetRate = -1; break;
        }
         SetFrameLimit(targetRate);
        PlayerPrefs.SetInt("FrameLimit", rate);
        PlayerPrefs.Save();
    }

    protected void SetFrameLimit(int rate)
    {
        if (rate < 0)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            Application.targetFrameRate = rate;
            QualitySettings.vSyncCount = 0;
        }
    }

    protected void InitTextureQualityDD()
    {
        textureQualDropdown.ClearOptions();
        var options = new List<string>()
        {
            "Full Resolution",
            "Half Resolution",
            "Quarter Resolution",
            "Eighth Resolution"
        };
        int index = PlayerPrefs.GetInt("TextureQuality", QualitySettings.globalTextureMipmapLimit);
        textureQualDropdown.value = index;
        textureQualDropdown.RefreshShownValue();
        SetTextureQuality(index);
        textureQualDropdown.onValueChanged.AddListener(OnTextureQualityChanged);
    }

    protected void OnTextureQualityChanged(int index)
    {
        SetTextureQuality(index);
    }

    protected void SetTextureQuality(int index)
    {
        QualitySettings.globalTextureMipmapLimit = index;
        PlayerPrefs.SetInt("TextureQuality", index);
        PlayerPrefs.Save();
    }

    protected void InitShadowQualityDD()
    {
        shadowQualDropdown.ClearOptions();
        var options = new List<string>()
        {
            "High",
            "Medium",
            "Low"
        };
        int index = PlayerPrefs.GetInt("ShadowQuality", GetShadowQualityIndex());
        shadowQualDropdown.value = index;
        shadowQualDropdown.RefreshShownValue();

        SetShadowQuality(index);
        shadowQualDropdown.onValueChanged.AddListener(OnShadowQualityChanged);
    }

    protected void OnShadowQualityChanged(int index)
    {
        SetShadowQuality(index);
    }

    protected void SetShadowQuality(int index)
    {
        switch (index)
        {
            case 0: QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Low; break;
            case 1: QualitySettings.shadowResolution= UnityEngine.ShadowResolution.Medium; break;
            case 2: QualitySettings.shadowResolution = UnityEngine.ShadowResolution.High; break;
            case 3: QualitySettings.shadowResolution = UnityEngine.ShadowResolution.VeryHigh; break;
        }
        PlayerPrefs.SetInt("ShadowQuality", index);
        PlayerPrefs.Save();
    }

    private int GetShadowQualityIndex()
    {
        switch (QualitySettings.shadowResolution)
        {
            case UnityEngine.ShadowResolution.Low: return 0;
            case UnityEngine.ShadowResolution.Medium: return 1;
            case UnityEngine.ShadowResolution.High: return 2;
            case UnityEngine.ShadowResolution.VeryHigh: return 3;
            default: return 2;
        }
    }

    protected void OnRenderDistanceChanged(float value)
    {
        SetRenderDistance(value);
        SetRenderDistanceText();
    }

    protected void SetRenderDistance(float value)
    {
        if(mainCameraRef != null)
        {
            mainCameraRef.farClipPlane  = value;
        }
        PlayerPrefs.SetFloat("RenderDistance", value);
        PlayerPrefs.Save();
    }

    protected void SetRenderDistanceText()
    {
        renderDisText.SetText(renderDisSlider.value.ToString());
    }

    protected void OnCamSensitivityChanged(float value)
    {
        SetCamSensitivity(value);
        SetCamSensitivityText();
    }

    protected void SetCamSensitivity(float value)
    {
        if(mainCameraRef != null)
        {
            //Need to link this to player camera controller
        }
    }

    protected void SetCamSensitivityText()
    {
        camSensText.SetText(camSensSlider.value.ToString());
    }

    protected void OnFOVChanged(float value)
    {
        SetCameraFOV(value);
        SetCameraFOVText();
    }

    protected void SetCameraFOV(float value)
    {
        if(mainCameraRef != null)
        {
            mainCameraRef.fieldOfView = value;
        }
        PlayerPrefs.SetFloat("FOV", value);
        PlayerPrefs.Save();
    }

    protected void SetCameraFOVText()
    {
        fovText.SetText(fovSlider.value.ToString());
    }

    protected void OnVsyncToggled(bool value)
    {
        SetVsyncState(value);
    }

    protected void SetVsyncState(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
        PlayerPrefs.SetInt("Vsync",value ? 1 : 0);
        PlayerPrefs.Save();
    }

    protected void OnInvertCamToggled(bool value)
    {
        SetInvertCamState(value);
    }

    protected void SetInvertCamState(bool value)
    {
        //need to use player cam controller
    }
}
