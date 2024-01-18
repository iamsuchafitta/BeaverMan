using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    private float _horizontalMove = 0f;
    private bool _jump = false;
    private bool _crouch = false;

    public bool isWasd = false;

    private int count;
    public Text countText1;
    public Text countText2;
    public Text winText;
    private bool conditionMet = false;

    void Start()
    {
        count = 0;
        SetCountText();
        winText.text = "";

        CheckCondition();
    }

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

        CheckCondition();
    }

    public void OnLanding() {
        this.animator.SetBool("IsJumping", false);
        this._jump = false;
    }

    public void OnCrouching(bool isCrouching) {
        this.animator.SetBool("IsCrouching", isCrouching);
        this._crouch = isCrouching;
    }

    private void FixedUpdate() {
        this.controller.Move(this._horizontalMove * Time.fixedDeltaTime, this._crouch, this._jump);
        this._jump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up 1"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Pick Up 2"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText1.text = "Your score: " + count.ToString();
        countText2.text = "Your score: " + count.ToString();
        if (count >= 10)
        {
            ConditionMet();
        }
    }

    private void CheckCondition()
    {
        // Проверяем выполнение условия
        if (conditionMet)
        {
            Debug.Log("winner"); 
        }
        else
        {
            
        }
    }

    public void ConditionMet()
    {
        conditionMet = true;
    }
}