using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// Класс ProgressManager отвечает за управление прогрессом игры.
/// </summary>
public class ProgressManager : MonoBehaviour {
    /// <summary>
    /// Ссылка на единственный экземпляр ProgressManager.
    /// </summary>
    public static ProgressManager Instance { get; set; }

    /// <summary>
    /// Ссылка на объект, который будет отображаться при завершении уровня.
    /// </summary>
    [SerializeField] private GameObject nextLevelImage;

    /// <summary>
    /// Ссылка на контейнер интерфейса паузы.
    /// </summary>
    [SerializeField] private GameObject pauseMenu;

    /// <summary>
    /// Свойство LevelsCompletedCount возвращает количество пройденных уровней.
    /// </summary>
    public int LevelsCompletedCount { get; private set; } = 0;

    /// <summary>
    /// Свойство CanNextLevel возвращает и устанавливает активность объекта nextLevelImage.
    /// </summary>
    private bool CanNextLevel {
        get => this.nextLevelImage.activeSelf;
        set => this.nextLevelImage.gameObject.SetActive(value);
    }

    /// <summary>
    /// Метод Awake вызывается при загрузке экземпляра скрипта.
    /// Метод используется для инициализации единственного экземпляра ProgressManager.
    /// Если экземпляр равен null, он устанавливает этот экземпляр как экземпляр и предотвращает уничтожение объекта, к которому прикреплен этот скрипт, при смене сцен.
    /// Он также добавляет метод OnSceneLoaded в делегат, который будет вызван при загрузке сцены.
    /// Если уже существует экземпляр, он уничтожает игровой объект, к которому прикреплен этот скрипт.
    /// </summary>
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            // this._lvlsCompleted = PlayerPrefs.GetInt("lvlsCompleted", 0);
            SceneManager.sceneLoaded += this.OnSceneLoaded;
        } else Destroy(this.gameObject);
    }

    /// <summary>
    /// Обрабатывает событие загрузки сцены. Вызывается каждый раз, когда загружается новая сцена.
    /// Отключает паузу, устанавливает флаг CanNextLevel в false.
    /// </summary>
    /// <param name="scene">Объект Scene, представляющий загруженную сцену.</param>
    /// <param name="mode">LoadSceneMode, указывающий, как загружается сцена.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        this.PauseUnpause(false);
        this.CanNextLevel = false;
        Debug.Log("Scene loaded: " + scene.name);
    }

    /// <summary>
    /// Метод Update вызывается один раз за кадр.
    /// Метод используется для проверки нажатия клавиши Escape и Enter.
    /// Если нажата клавиша Escape и сцена не является сценой главного меню, вызывается метод PauseUnpause.
    /// Если нажата клавиша Enter и флаг CanNextLevel равен true, вызывается метод LoadNextLevel.
    /// </summary>
    private void Update() {
        // Pause on Escape not in game main "Menu" scene
        var isEscapePressed = Input.GetKeyDown(KeyCode.Escape);
        var isMainMenu = SceneManager.GetActiveScene().name.Equals("Menu");
        if (isEscapePressed && !isMainMenu) this.PauseUnpause();
        if (this.CanNextLevel && Input.GetKeyDown(KeyCode.Return)) this.LoadNextLevel();
    }

    /// <summary>
    /// Метод CompleteLevel вызывается при завершении уровня.
    /// Устанавливает флаг CanNextLevel в true
    /// И сохраняет номер уровня в PlayerPrefs, если он больше, чем сохраненный номер.
    /// </summary>
    public void CompleteLevel() {
        this.CanNextLevel = true;
        var levelNumber = int.Parse(Regex.Match(SceneManager.GetActiveScene().name, @"\d+$").Value);
        this.LevelsCompletedCount = Mathf.Max(this.LevelsCompletedCount, levelNumber);
        PlayerPrefs.SetInt("lvlsCompleted", this.LevelsCompletedCount);
    }

    /// <summary>
    /// Метод PauseUnpause вызывается для паузы или возобновления игры.
    /// </summary>
    /// <param name="isPause">Необязательный параметр, указывающий, должна ли игра быть поставлена на паузу. Если параметр не указан, состояние паузы переключается.</param>
    /// <remarks>
    /// Метод устанавливает масштаб времени в 0, если игра должна быть поставлена на паузу, и в 1, если игра должна быть возобновлена.
    /// Также активирует или деактивирует меню паузы в зависимости от состояния паузы.
    /// </remarks>
    public void PauseUnpause(bool? isPause = null) {
        Time.timeScale = isPause ?? !Time.timeScale.Equals(0) ? 0 : 1;
        this.pauseMenu.SetActive(isPause ?? !this.pauseMenu.activeSelf);
    }

    /// <summary>
    /// Метод RestartLevel вызывается для перезапуска текущего уровня.
    /// </summary>
    public void RestartLevel() {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    /// <summary>
    /// Метод LoadMainMenu вызывается для загрузки сцены главного меню.
    /// </summary>
    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Загрузка следующего уровня.
    /// </summary>
    private void LoadNextLevel() {
        var levelsCount = SceneManager.sceneCountInBuildSettings - 1;
        var currentSceneName = SceneManager.GetActiveScene().name;
        var currentSceneNumber = int.Parse(Regex.Match(currentSceneName, @"\d+$").Value);
        if (currentSceneNumber >= levelsCount) SceneManager.LoadScene("Menu");
        else SceneManager.LoadScene("Level " + (currentSceneNumber + 1));
    }
}