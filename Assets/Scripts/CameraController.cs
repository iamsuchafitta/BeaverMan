using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 _offset;
    private Animator _animator;

    //Use this for initialization
    private void Start() {
        this._offset = this.transform.position - this.player.transform.position;
        this._animator = this.player.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void LateUpdate() {
        if (this._animator.GetBool("isDead")) return;
        this.transform.position = this.player.transform.position + this._offset;
    }
}