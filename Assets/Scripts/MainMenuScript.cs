using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenuScript : MonoBehaviour {
    [FormerlySerializedAs("LvlButtons")] public UnityEngine.UI.Button[] buttons;

    void Start() {
        foreach (var button in this.buttons) {
            var lvlNumber = int.Parse(button.name[9..]);
            button.interactable = lvlNumber <= ProgressManager.Instance._lvlsCompleted + 1;
        }
    }

    public void LoadLevel(int lvlNumber) {
        SceneManager.LoadScene("Level " + lvlNumber);
    }

    public void ExitGame() {
        Application.Quit();
    }
}