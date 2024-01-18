using TMPro;
using UnityEngine;

public class Level5Manager : MonoBehaviour {
    private int _player1Score = 0;
    private int _player2Score = 0;

    private int _player1ScoreToWin = 0;
    private int _player2ScoreToWin = 0;
    
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    private void Awake() {
        this._player1ScoreToWin = GameObject.FindGameObjectsWithTag("Pick Up 1").Length;
        this._player2ScoreToWin = GameObject.FindGameObjectsWithTag("Pick Up 2").Length;
        this.player1ScoreText.text = this._player1Score + " / " + this._player1ScoreToWin;
        this.player2ScoreText.text = this._player2Score + " / " + this._player2ScoreToWin;
    }

    public void AddScore(GameObject player, GameObject item) {
        if (player.CompareTag("FirstPlayer") && item.CompareTag("Pick Up 1")) {
            item.SetActive(false);
            this._player1Score++;
            Debug.Log("Player 1 scored " + this._player1Score + " of " + this._player1ScoreToWin);
            this.player1ScoreText.text = this._player1Score + " / " + this._player1ScoreToWin;
        } else if (player.CompareTag("SecondPlayer") && item.CompareTag("Pick Up 2")) {
            item.SetActive(false);
            this._player2Score++;
            Debug.Log("Player 2 scored " + this._player2Score + " of " + this._player2ScoreToWin);
            this.player2ScoreText.text = this._player2Score + " / " + this._player2ScoreToWin;
        }

        if (this._player1Score >= this._player1ScoreToWin && this._player2Score >= this._player2ScoreToWin) {
            Debug.Log("Win!");
        }
    }
}