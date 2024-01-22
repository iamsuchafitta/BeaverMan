using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private Transform _finish;
    private Transform _player1;
    private Transform _player2;

    private int _player1Score = 0;
    private int _player2Score = 0;
    private int _player1ScoreToWin = 0;
    private int _player2ScoreToWin = 0;

    [SerializeField] private GameObject playersScoreUI;
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;
    
    private AudioSource _eatingAudioSource;
    [SerializeField] private AudioClip[] eatingAudios;

    private void Start() {
        this._finish = GameObject.FindGameObjectWithTag("Finish")?.transform;
        this._player1 = GameObject.FindGameObjectWithTag("FirstPlayer")?.transform;
        this._player2 = GameObject.FindGameObjectWithTag("SecondPlayer")?.transform;
        this._eatingAudioSource = this.GetComponent<AudioSource>();
        this._player1ScoreToWin = Mathf.Min(GameObject.FindGameObjectsWithTag("Pick Up 1").Length, 2);
        this._player2ScoreToWin = Mathf.Min(GameObject.FindGameObjectsWithTag("Pick Up 2").Length, 2);
        // this._player1ScoreToWin = GameObject.FindGameObjectsWithTag("Pick Up 1").Length;
        // this._player2ScoreToWin = GameObject.FindGameObjectsWithTag("Pick Up 2").Length;
        this.UpdatePlayersScoresText();
        this.ShowPlayersScoreUI(this._player1ScoreToWin != 0 || this._player2ScoreToWin != 0);
    }

    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        if (this._player1Score < this._player1ScoreToWin || this._player2Score < this._player2ScoreToWin) return;
        if (this.IsPlayerNearFinish(this._player1) && this.IsPlayerNearFinish(this._player2)) {
            ProgressManager.Instance.CompleteLevel();
        }
    }

    private bool IsPlayerNearFinish(Transform player) {
        if (!this._finish) return this._player1ScoreToWin != 0 && this._player2ScoreToWin != 0;
        const float allowedDistance = 2.0f;
        return Vector2.Distance(player.position, this._finish.transform.position) <= allowedDistance;
    }

    public void AddScore(GameObject player, GameObject item) {
        if (player.CompareTag("FirstPlayer") && item.CompareTag("Pick Up 1")) {
            item.SetActive(false);
            ++this._player1Score;
            this.UpdatePlayersScoresText();
            this.PlayRandomEatingSound();
        } else if (player.CompareTag("SecondPlayer") && item.CompareTag("Pick Up 2")) {
            item.SetActive(false);
            ++this._player2Score;
            this.UpdatePlayersScoresText();
            this.PlayRandomEatingSound();
        }
    }

    private void PlayRandomEatingSound() {
        if (this._eatingAudioSource == null) return;
        this._eatingAudioSource.clip = this.eatingAudios[Random.Range(0, this.eatingAudios.Length)];
        this._eatingAudioSource.Play();
    }

    private void ShowPlayersScoreUI(bool show) {
        if (this.playersScoreUI != null) this.playersScoreUI.SetActive(show);
    }

    private void UpdatePlayersScoresText() {
        if (this.player1ScoreText) this.player1ScoreText.text = this._player1Score + " / " + this._player1ScoreToWin;
        if (this.player2ScoreText) this.player2ScoreText.text = this._player2Score + " / " + this._player2ScoreToWin;
    }
}