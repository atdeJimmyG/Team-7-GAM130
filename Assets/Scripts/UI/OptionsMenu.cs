using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    Resolution[] resolutions;
    string[] qualitys;
    [SerializeField] Text FPSCounter;

    private void Start()
    {
        Screen.fullScreen = true;

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            Debug.Log(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        qualitys = QualitySettings.names;

        qualityDropdown.ClearOptions();

        List<string> qOptions = new List<string>();

        for (int i = 0; i < qualitys.Length; i++)
        {
            string qOPtion = qualitys[i];
            qOptions.Add(qOPtion);
        }

        qualityDropdown.AddOptions(qOptions);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resoltion = resolutions[resolutionIndex];
        Screen.SetResolution(resoltion.width, resoltion.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ShowFPSCounter(bool active)
    {
        FPSCounter.enabled = active;
        Debug.Log(FPSCounter);
        Debug.Log(active);
    }
}
