using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    private float _horizontalMove = 0f;
    private bool _jump = false;
    private bool _crouch = false;

    public bool isWasd = false;

    private void Update() {
        this._horizontalMove = Input.GetAxisRaw(this.isWasd ? "WASDHorizontal" : "ArrowsHorizontal") * this.runSpeed;

        this.animator.SetFloat("Speed", Mathf.Abs(this._horizontalMove));

        if (Input.GetKeyDown(this.isWasd ? KeyCode.W : KeyCode.UpArrow)) {
            this.animator.SetBool("IsJumping", true);
            this._jump = true;
        }

        if (Input.GetKeyDown(this.isWasd ? KeyCode.S : KeyCode.DownArrow)) {
            this.animator.SetBool("IsCrouching", true);
            this._crouch = true;
        } else if (Input.GetKeyUp(this.isWasd ? KeyCode.S : KeyCode.DownArrow)) {
            this.animator.SetBool("IsCrouching", false);
            this._crouch = false;
        }
    }

    public void OnLanding() {
        this.animator.SetBool("IsJumping", false);
        this._jump = false;
    }

    public void OnCrouching(bool isCrouching) {
        this.animator.SetBool("IsCrouching", isCrouching);
    }

    private void FixedUpdate() {
        this.controller.Move(this._horizontalMove * Time.fixedDeltaTime, this._crouch, this._jump);
        this._jump = false;
    }
}