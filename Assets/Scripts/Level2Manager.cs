using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Level2Manager : MonoBehaviour {
    private Transform player1;
    private Transform player2;
    public Tilemap pitTilemap; // Ссылка на Tilemap Pit
    public GameObject nextLevelButton;

    private bool _player1ReachedDoor;
    private bool _player2ReachedDoor;

    private void Awake() {
        this.player1 = GameObject.FindGameObjectWithTag("FirstPlayer").transform;
        this.player2 = GameObject.FindGameObjectWithTag("SecondPlayer").transform;
        this.nextLevelButton.SetActive(false); // Изначально кнопка неактивна
        this._player1ReachedDoor = false;
        this._player2ReachedDoor = false;
    }

    private void Update() {
        // Проверяем, касается ли один из игроков Tilemap Pit
        if (this.pitTilemap != null) {
            var player1CellPos = this.pitTilemap.WorldToCell(this.player1.position);
            var player2CellPos = this.pitTilemap.WorldToCell(this.player2.position);

            // Проверяем, находятся ли игроки в клетках с тайлами в Tilemap Pit
            if (this.pitTilemap.HasTile(player1CellPos) || this.pitTilemap.HasTile(player2CellPos)) {
                this.RestartLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("FirstPlayer")) {
            Debug.Log("Player 1 reached door");
            this._player2ReachedDoor = true;
        } else if (other.gameObject.CompareTag("SecondPlayer")) {
            Debug.Log("Player 2 reached door");
            this._player1ReachedDoor = true;
        }
        if (this._player1ReachedDoor && this._player2ReachedDoor) {
            Debug.Log("Both players reached door");
            this.nextLevelButton.SetActive(true);
        }
    }

    private void RestartLevel() {
        // Здесь можно добавить логику перезапуска уровня, например, перезагрузку сцены
        SceneManager.LoadScene("Level 2");
    }
}