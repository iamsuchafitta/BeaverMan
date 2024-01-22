using UnityEngine;

public class TeleportScript : MonoBehaviour {
    public Vector2 targetPosition;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("FirstPlayer") || collision.CompareTag("SecondPlayer")) {
            collision.transform.position = this.targetPosition;
        }
    }
}