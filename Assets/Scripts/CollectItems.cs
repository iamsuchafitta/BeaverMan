using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collectibles : MonoBehaviour {
    // Ссылки на спрайты
    public GameObject cherry;
    public GameObject gem;

    public GameObject cherryImage;
    public GameObject gemImage;

    public string nextScene = "Level 2";

    void Start() {
        this.cherryImage.SetActive(false);
        this.gemImage.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("FirstPlayer") && this.gameObject.CompareTag("Cherry")) {
            this.cherryImage.SetActive(true);
            Level1Manager.Instance.ItemCollected();
            Destroy(this.gameObject); // Уничтожить предмет после сбора
        }
        else if (other.gameObject.CompareTag("SecondPlayer") && this.gameObject.CompareTag("Gem")) {
            this.gemImage.SetActive(true);
            Level1Manager.Instance.ItemCollected();
            Destroy(this.gameObject); // Уничтожить предмет после сбора
        }
    }

    // Метод для перехода на следующий уровень
    private void LoadNextLevel() {
        // Загружаем сцену с номером nextScene
        SceneManager.LoadScene(this.nextScene);
    }
}