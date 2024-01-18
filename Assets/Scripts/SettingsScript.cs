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
    private Resolution[] _resolutions;

    private static AudioSource _musicSource;

    [FormerlySerializedAs("SettingsSavedText")]
    public GameObject settingsSavedText;

    private void Start() {
        this.resolutionDropdown.ClearOptions();
        var options = new List<string>();
        this._resolutions = Screen.resolutions;
        var currentResolutionIndex = 0;

        for (var i = 0; i < this._resolutions.Length; i++) {
            var option = this._resolutions[i].width + "x" + this._resolutions[i].height + " " + this._resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (this._resolutions[i].width == Screen.currentResolution.width
                && this._resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        this.resolutionDropdown.AddOptions(options);
        this.resolutionDropdown.RefreshShownValue();
        this.LoadSettings(currentResolutionIndex);
    }

    private void Awake() {
        _musicSource = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(_musicSource.gameObject);
    }

    public void SetVolumeSlider() {
        var volume = this.volumeSlider.value;
        this.SetVolume(volume);
    }

    public void SetVolume(float volume) {
        this.audioMixer.SetFloat("Volume", volume);
    }


    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex) {
        var resolution = this._resolutions[resolutionIndex];
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
        PlayerPrefs.SetInt("QualitySettingPreference", this.qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", this.resolutionDropdown.value);
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