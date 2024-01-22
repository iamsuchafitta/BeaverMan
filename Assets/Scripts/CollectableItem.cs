using UnityEngine;

public class CollectableItem : MonoBehaviour {
    private LevelManager _levelManager;

    private void Awake() => this._levelManager = FindObjectOfType<LevelManager>();

    private void OnTriggerEnter2D(Collider2D other) {
        if (!this.gameObject.activeSelf) return;
        if (other.CompareTag("FirstPlayer") || other.CompareTag("SecondPlayer")) {
            this._levelManager.AddScore(other.gameObject, this.gameObject);
        }
    }
}