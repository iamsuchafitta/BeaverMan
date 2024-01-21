using UnityEngine;

public class Rotator : MonoBehaviour {
    public float bounceHeight = 0.1f; // Высота подпрыгивания
    public float bounceSpeed = 8f; // Скорость подпрыгивания

    private Vector3 _initialPosition;

    private void Start() {
        // Сохраняем начальную позицию для возврата при подпрыгивании
        this._initialPosition = this.transform.position;
    }

    private void Update() {
        // Подпрыгивание вверх-вниз
        var bounce = Mathf.Sin(Time.time * this.bounceSpeed) * this.bounceHeight;
        this.transform.position = this._initialPosition + new Vector3(0f, bounce, 0f);
    }
}
