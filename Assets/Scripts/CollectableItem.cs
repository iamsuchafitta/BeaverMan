using UnityEngine;

public class CollectableItem : MonoBehaviour {
    private Level5Manager _level5Manager;

    private void Awake() {
        this._level5Manager = FindObjectOfType<Level5Manager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!this.gameObject.activeSelf) return;
        if (other.CompareTag("FirstPlayer") || other.CompareTag("SecondPlayer")) {
            this._level5Manager.AddScore(other.gameObject, this.gameObject);
        }
    }
}