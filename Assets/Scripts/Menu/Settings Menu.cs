using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;

    private static List<Resolution> _resolutions;
    private static List<string> _resolutionOptions;
    
    void Start()
    {
        InitializeSettingsUI();
    }
    
    private void InitializeSettingsUI()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        volumeSlider.value = AudioListener.volume;
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        if (_resolutions == null)
        {
            _resolutions = new List<Resolution>();
            _resolutionOptions = new List<string>();
            
            Resolution[] allResolutions = Screen.resolutions;
            HashSet<String> addedOptions = new HashSet<String>();

            for (int i = 0; i < allResolutions.Length; i++)
            {
                string optionText = allResolutions[i].width + " x " + allResolutions[i].height;

                if (!addedOptions.Contains(optionText))
                {
                    addedOptions.Add(optionText);
                    _resolutions.Add(allResolutions[i]);
                    _resolutionOptions.Add(optionText);
                }
            }
        }
        
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(_resolutionOptions);
        int currentResolutionIndex = 0;
        
        for (int i = 0; i < _resolutions.Count; i++)
        {
            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
}
