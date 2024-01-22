using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenuScript : MonoBehaviour {
    [FormerlySerializedAs("LvlButtons")] public UnityEngine.UI.Button[] buttons;

    private void Start() {
        foreach (var button in this.buttons) {
            // TODO: Uncomment this when levels are implemented
            var lvlNumber = int.Parse(button.name[9..]);
            button.interactable = lvlNumber <= ProgressManager.LevelsCompletedCount + 1;
            // button.interactable = true;
        }
    }

    public void LoadLevel(int lvlNumber) {
        SceneManager.LoadScene("Level " + lvlNumber);
    }

    public void ExitGame() {
        Application.Quit();
    }
}