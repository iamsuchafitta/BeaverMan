using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public void LoadLevel() {
        SceneManager.LoadScene("Level 1"); // в кавычках название сцены на которую осуществляется переход
    }

    public void ExitGame() {
        Application.Quit();
    }
}