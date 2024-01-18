using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 _offset;

    //Use this for initialization
    private void Start() {
        this._offset = this.transform.position - this.player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate() {
        this.transform.position = this.player.transform.position + this._offset;
    }
}