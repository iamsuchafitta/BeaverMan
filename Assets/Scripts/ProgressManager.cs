using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour {
    public static ProgressManager Instance { get; set; }
    
    private GameObject _pauseCanvas;

    public int _lvlsCompleted { get; private set; } = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            // this._lvlsCompleted = PlayerPrefs.GetInt("lvlsCompleted", 0);
            this._pauseCanvas = this.transform.Find("PauseCanvas").gameObject;
            if (!this._pauseCanvas) throw new Exception("PauseCanvas not found!");
        } else Destroy(this.gameObject);
    }

    private void Update() {
        // Pause on Escape not in game main "Menu" scene
        var isEscapePressed = Input.GetKeyDown(KeyCode.Escape);
        var isMainMenu = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Menu");
        if (isEscapePressed && !isMainMenu) this.PauseUnpause();
    }

    public void CompleteLevel() {
        this._lvlsCompleted++;
        PlayerPrefs.SetInt("lvlsCompleted", this._lvlsCompleted);
    }

    public void PauseUnpause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        this._pauseCanvas.SetActive(!this._pauseCanvas.activeSelf);
    }

    public void RestartLevel() {
        this.PauseUnpause();
        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
    }

    public void LoadMainMenu() {
        this.PauseUnpause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    
    public void LoadNextLevel() {
        this.PauseUnpause();
        var levelsCount = SceneManager.sceneCountInBuildSettings - 1;
        var currentSceneName = SceneManager.GetActiveScene().name;
        var currentSceneNumber = int.Parse(Regex.Match(currentSceneName, @"\d+$").Value);
        if (currentSceneNumber >= levelsCount) SceneManager.LoadScene("Menu");
        else SceneManager.LoadScene("Level " + (currentSceneNumber + 1));
    }
}