using UnityEngine;
using UnityEngine.Serialization;

public class SpringJumper : MonoBehaviour {
    private Animator _springAnimator;
    [FormerlySerializedAs("JumpForce")] public float jumpForce;

    private void Start() {
        this._springAnimator = this.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("FirstPlayer") || collision.transform.CompareTag("SecondPlayer")) {
            this._springAnimator.SetBool("isWorking", true);
        }

        if (collision.relativeVelocity.y <= 0f) {
            var rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null) {
                var velocity = rb.velocity;
                velocity.y = this.jumpForce;
                rb.velocity = velocity;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.transform.CompareTag("FirstPlayer") || collision.transform.CompareTag("SecondPlayer")) {
            this._springAnimator.SetBool("isWorking", false);
        }
    }
}