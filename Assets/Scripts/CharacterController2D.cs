using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController2D : MonoBehaviour {
    [FormerlySerializedAs("m_JumpForce")] [SerializeField]
    private float mJumpForce = 400f; // Amount of force added when the player jumps.

    [FormerlySerializedAs("m_MovementSmoothing")] [Range(0, .3f)] [SerializeField]
    private float mMovementSmoothing = .05f; // How much to smooth out the movement

    [FormerlySerializedAs("m_AirControl")] [SerializeField]
    private bool mAirControl = false; // Whether or not a player can steer while jumping;

    [FormerlySerializedAs("m_WhatIsGround")] [SerializeField]
    private LayerMask mWhatIsGround; // A mask determining what is ground to the character

    [FormerlySerializedAs("m_GroundCheck")] [SerializeField]
    private Transform mGroundCheck; // A position marking where to check if the player is grounded.

    private const float KGroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool _mGrounded; // Whether or not the player is grounded.
    private Rigidbody2D _mRigidbody2D;
    private bool _mFacingRight = true; // For determining which way the player is currently facing.
    private Vector3 _mVelocity = Vector3.zero;

    public float runSpeed = 40f;
    private Animator _animator;
    public bool isWasd = false;
    private float _horizontalMove = 0f;

    private void Awake() {
        this._mRigidbody2D = this.GetComponent<Rigidbody2D>();
        this._animator = this.GetComponent<Animator>();
    }

    private void Update() {
        this._horizontalMove = Input.GetAxisRaw(this.isWasd ? "WASDHorizontal" : "ArrowsHorizontal") * this.runSpeed;
        this._animator.SetFloat("Speed", Mathf.Abs(this._horizontalMove));
        if (!this._mGrounded || Input.GetKeyDown(this.isWasd ? KeyCode.W : KeyCode.UpArrow)) {
            this._animator.SetBool("IsJumping", true);
        }
    }

    private void FixedUpdate() {
        var wasGrounded = this._mGrounded;
        this._mGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        var colliders = Physics2D.OverlapCircleAll(this.mGroundCheck.position, KGroundedRadius, this.mWhatIsGround);
        for (var i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != this.gameObject) {
                this._mGrounded = true;
                if (!wasGrounded) {
                    this._animator.SetBool("IsJumping", false);
                }
            }
        }

        this.Move(this._horizontalMove * Time.fixedDeltaTime, this._animator.GetBool("IsJumping"));
    }

    private void Move(float move, bool jump) {
        // Only control the player if grounded or airControl is turned on
        if (this._mGrounded || this.mAirControl) {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, this._mRigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            this._mRigidbody2D.velocity = Vector3.SmoothDamp(this._mRigidbody2D.velocity, targetVelocity, ref this._mVelocity, this.mMovementSmoothing);
            // If the input is moving the player left and the player is facing right...
            // Otherwise if the input is moving the player right and the player is facing left...
            if (move < 0 && this._mFacingRight || move > 0 && !this._mFacingRight) this.Flip();
        }

        // If the player should jump...
        if (this._mGrounded && jump) {
            // Add a vertical force to the player.
            this._mGrounded = false;
            this._mRigidbody2D.AddForce(new Vector2(0f, this.mJumpForce));
            this._animator.SetBool("IsJumping", true);
        }
    }

    private void Flip() {
        // Switch the way the player is labelled as facing.
        this._mFacingRight = !this._mFacingRight;
        // Multiply the player's x local scale by -1.
        var theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }
}