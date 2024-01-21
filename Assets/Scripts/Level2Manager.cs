using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Level2Manager : MonoBehaviour {
    private Transform _player1;
    private Transform _player2;
    public Tilemap pitTilemap; // Ссылка на Tilemap Pit

    private bool _isPlayer1ReachedDoor;
    private bool _isPlayer2ReachedDoor;

    private void Awake() {
        this._player1 = GameObject.FindGameObjectWithTag("FirstPlayer").transform;
        this._player2 = GameObject.FindGameObjectWithTag("SecondPlayer").transform;
        this._isPlayer1ReachedDoor = false;
        this._isPlayer2ReachedDoor = false;
    }

    private void Update() {
        // Проверяем, касается ли один из игроков Tilemap Pit
        if (this.pitTilemap != null) {
            // Получаем позиции игроков в клеточных координатах Tilemap Pit
            var player1CellPos = this.pitTilemap.WorldToCell(this._player1.position);
            var player2CellPos = this.pitTilemap.WorldToCell(this._player2.position);

            // Проверяем, находятся ли игроки в клетках с тайлами в Tilemap Pit
            if (this.pitTilemap.HasTile(player1CellPos) || this.pitTilemap.HasTile(player2CellPos)) {
                ProgressManager.Instance.RestartLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("FirstPlayer")) {
            Debug.Log("Player 1 reached door");
            this._isPlayer2ReachedDoor = true;
        } else if (other.gameObject.CompareTag("SecondPlayer")) {
            Debug.Log("Player 2 reached door");
            this._isPlayer1ReachedDoor = true;
        }
        if (this._isPlayer1ReachedDoor && this._isPlayer2ReachedDoor) {
            Debug.Log("Both players reached door");
            ProgressManager.Instance.CompleteLevel();
        }
    }
}