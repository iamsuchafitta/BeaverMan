using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PitScript : MonoBehaviour {
    private Tilemap[] _pitTilemaps;
    private Transform _player1;
    private Transform _player2;

    private void Awake() {
        this._pitTilemaps = GameObject.FindGameObjectsWithTag("DeadlyTouch")
            .Select(obj => obj.GetComponent<Tilemap>())
            .ToArray();
        this._player1 = GameObject.FindGameObjectWithTag("FirstPlayer")?.transform;
        this._player2 = GameObject.FindGameObjectWithTag("SecondPlayer")?.transform;
    }

    private void Update() {
        if (!this._player1 || !this._player2) return;
        // Проверяем, касается ли один из игроков Tilemap Pit
        foreach (var pitTilemap in this._pitTilemaps) {
            // Получаем позиции игроков в клеточных координатах Tilemap Pit
            var player1CellPos = pitTilemap.WorldToCell(this._player1.position);
            var player2CellPos = pitTilemap.WorldToCell(this._player2.position);

            // Проверяем, находятся ли игроки в клетках с тайлами в Tilemap Pit
            if (pitTilemap.HasTile(player1CellPos) || pitTilemap.HasTile(player2CellPos)) {
                ProgressManager.Instance.RestartLevel();
            }
        }
    }
}