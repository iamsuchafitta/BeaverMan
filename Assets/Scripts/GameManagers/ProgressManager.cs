using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ProgressManager : MonoBehaviour {
    public static ProgressManager Instance { get; set; }

    [SerializeField] private GameObject nextLevelUI;
    [SerializeField] private TMP_Text nextLevelUIText;

    [SerializeField] private GameObject pauseMenu;

    public static bool PlayVideoOnMenuLoad = false;
    [SerializeField] private GameObject bobrVideoContainer;
    [SerializeField] private VideoPlayer bobrVideoPlayer;

    public static int LevelsCompletedCount { get; private set; } = 0;

    private bool CanNextLevel {
        get => this.nextLevelUI.activeSelf;
        set => this.nextLevelUI.gameObject.SetActive(value);
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            // TODO: uncomment this for progress saving
            // this.LevelsCompletedCount = PlayerPrefs.GetInt("lvlsCompleted", 0);
        } else Destroy(this.gameObject);

        SceneManager.sceneLoaded += this.OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        this.PauseUnpause(false);
        if (PlayVideoOnMenuLoad) {
            this.BobrVideoPlay();
            PlayVideoOnMenuLoad = false;
            this.bobrVideoPlayer.loopPointReached += this.BobrVideoEnd;
        }
    }

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
        LevelsCompletedCount = Mathf.Max(LevelsCompletedCount, levelNumber);
        this.nextLevelUIText.text = levelNumber >= SceneManager.sceneCountInBuildSettings - 1
            ? "Вы прошли все уровни!\nНажмите <color=green>ENTER</color> для возврата в главное меню"
            : "Нажмите <color=green>ENTER</color> для перехода на следующий уровень";
        this.CanNextLevel = true;
        PlayerPrefs.SetInt("lvlsCompleted", LevelsCompletedCount);
    }

    private void PauseUnpause(bool? isPause = null) {
        Time.timeScale = isPause ?? !Time.timeScale.Equals(0) ? 0 : 1;
        this.pauseMenu.SetActive(isPause ?? !this.pauseMenu.activeSelf);
    }

    public void Unpause() => this.PauseUnpause(false);

    public void RestartLevel() {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadMainMenu() => SceneManager.LoadScene("Menu");

    private void LoadNextLevel() {
        var levelsCount = SceneManager.sceneCountInBuildSettings - 2;
        var currentSceneName = SceneManager.GetActiveScene().name;
        var currentSceneNumber = int.Parse(Regex.Match(currentSceneName, @"\d+$").Value);
        if (currentSceneNumber >= levelsCount) {
            PlayVideoOnMenuLoad = true;
            SceneManager.LoadScene("Menu");
        } else SceneManager.LoadScene("Level " + (currentSceneNumber + 1));
    }

    private void BobrVideoPlay() {
        this.bobrVideoContainer.SetActive(true);
        this.bobrVideoPlayer.Play();
    }

    private void BobrVideoEnd(VideoPlayer source) {
        this.bobrVideoContainer.SetActive(false);
        this.bobrVideoPlayer.Stop();
    }
}