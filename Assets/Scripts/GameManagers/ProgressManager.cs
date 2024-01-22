using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour {
    public static ProgressManager Instance { get; set; }

    [SerializeField] private GameObject nextLevelUI;
    [SerializeField] private TMP_Text nextLevelUIText;

    [SerializeField] private GameObject pauseMenu;

    public int LevelsCompletedCount { get; private set; } = 0;

    private bool CanNextLevel {
        get => this.nextLevelUI.activeSelf;
        set => this.nextLevelUI.gameObject.SetActive(value);
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            // this._lvlsCompleted = PlayerPrefs.GetInt("lvlsCompleted", 0);
        } else Destroy(this.gameObject);
        SceneManager.sceneLoaded += this.OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => this.PauseUnpause(false);

    private void OnDestroy() => SceneManager.sceneLoaded -= this.OnSceneLoaded;

    private void Update() {
        // Pause on Escape not in game main "Menu" scene
        var isEscapePressed = Input.GetKeyDown(KeyCode.Escape);
        var isMainMenu = SceneManager.GetActiveScene().name.Equals("Menu");
        if (isEscapePressed && !isMainMenu) this.PauseUnpause();
        if (this.CanNextLevel && Input.GetKeyDown(KeyCode.Return)) this.LoadNextLevel();
    }

    public void CompleteLevel() {
        if (this.CanNextLevel) return;
        var levelNumber = int.Parse(Regex.Match(SceneManager.GetActiveScene().name, @"\d+$").Value);
        this.LevelsCompletedCount = Mathf.Max(this.LevelsCompletedCount, levelNumber);
        this.nextLevelUIText.text = levelNumber >= SceneManager.sceneCountInBuildSettings - 1
            ? "Вы прошли все уровни!\nНажмите <color=green>ENTER</color> для возврата в главное меню"
            : "Нажмите <color=green>ENTER</color> для перехода на следующий уровень";
        this.CanNextLevel = true;
        PlayerPrefs.SetInt("lvlsCompleted", this.LevelsCompletedCount);
    }

    public void PauseUnpause(bool? isPause = null) {
        Time.timeScale = isPause ?? !Time.timeScale.Equals(0) ? 0 : 1;
        this.pauseMenu.SetActive(isPause ?? !this.pauseMenu.activeSelf);
    }

    public void RestartLevel() {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    private void LoadNextLevel() {
        var levelsCount = SceneManager.sceneCountInBuildSettings - 1;
        var currentSceneName = SceneManager.GetActiveScene().name;
        var currentSceneNumber = int.Parse(Regex.Match(currentSceneName, @"\d+$").Value);
        if (currentSceneNumber >= levelsCount) SceneManager.LoadScene("Menu");
        else SceneManager.LoadScene("Level " + (currentSceneNumber + 1));
    }
}