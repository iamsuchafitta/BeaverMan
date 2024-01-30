using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
    public AudioMixer audioMixer;
    //private AudioSourse audioScr;

    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;
    private const float CurrentVolume = 1f;
    Resolution[] _resolutions;

    private static AudioSource _musicSource;
    public GameObject musicObject;

    [FormerlySerializedAs("SettingsSavedText")]
    public GameObject settingsSavedText;

    private void Start() {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        _resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height + " " + _resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (_resolutions[i].width == Screen.currentResolution.width
                  && _resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    //private void Awake() {
    //    _musicSource = GetComponent<AudioSource>();
    //    DontDestroyOnLoad(_musicSource.gameObject);
    //}

    private void Awake()
    {
        if (musicObject != null)
        {
            _musicSource = musicObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("MusicObject не задан в SettingsScript");
        }
    }


    public void SetVolumeSlider() {
        var volume = volumeSlider.value;
        SetVolume(volume);
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }


    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex) {
        var resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,
            resolution.height, Screen.fullScreen);
    }


    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ExitGame() {
        SceneManager.LoadScene("Menu");
    }

    public void SaveSettings() {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference",
            Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference", CurrentVolume);
        this.settingsSavedText.SetActive(true);
        // Запустить корутину для деактивации SettingsSavedText через 5 секунд
        this.StartCoroutine(this.DeactivateSettingsSavedText());
    }

    private IEnumerator DeactivateSettingsSavedText() {
        // Ждать 3 секунды
        yield return new WaitForSeconds(5f);

        // Деактивировать SettingsSavedText
        this.settingsSavedText.SetActive(false);
    }

    private void LoadSettings(int currentResolutionIndex) {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            this.qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            this.qualityDropdown.value = 3;
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            this.resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            this.resolutionDropdown.value = currentResolutionIndex;
        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
        if (PlayerPrefs.HasKey("VolumePreference"))
            this.volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        else
            this.volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
    }
}