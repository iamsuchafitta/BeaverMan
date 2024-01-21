using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour {
    public static Level1Manager Instance;

    private int _itemsCollected = 0;
    public Button nextLevelButton;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(this.gameObject);
        }

        this.nextLevelButton.gameObject.SetActive(false); // Изначально кнопка неактивна
    }

    public void ItemCollected() {
        this._itemsCollected++;
        if (this._itemsCollected >= 2) {
            // Предполагается, что нужно собрать 2 предмета
            // this.nextLevelButton.gameObject.SetActive(true);
            if (ProgressManager.Instance.LevelsCompletedCount < 1) ProgressManager.Instance.CompleteLevel();
        }
    }

    public void NextLevel() {
        SceneManager.LoadScene("Level 2");
    }
}