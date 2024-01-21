using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Level4Manager : MonoBehaviour {
    [FormerlySerializedAs("Door")] public GameObject door;
    private Vector3 _hiddenPosition;
    private Vector3 _originalPosition;
    public float moveTime = 2f;
    private bool _isKeyCollected = false;
    
    public GameObject cameraPlayer1;
    public GameObject cameraPlayer2;
    public GameObject cameraOnDoor;

    private void Start() {
        this._originalPosition = this.door.transform.position;
        this._hiddenPosition = new Vector3(
            this._originalPosition.x,
            this._originalPosition.y - 5,
            this._originalPosition.z
        );
        this.door.transform.position = this._hiddenPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (this._isKeyCollected) return;
        if (!other.gameObject.CompareTag("FirstPlayer") && !other.gameObject.CompareTag("SecondPlayer")) return;
        this._isKeyCollected = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.StartCoroutine(this.MoveDoor(this.door, this._hiddenPosition, this._originalPosition, this.moveTime));
    }

    private IEnumerator MoveDoor(GameObject movingDoor, Vector3 fromPos, Vector3 toPos, float duration) {
        this.cameraPlayer1.SetActive(false);
        this.cameraPlayer2.SetActive(false);
        this.cameraOnDoor.SetActive(true);
        
        float elapsedTime = 0;

        while (elapsedTime < duration) {
            movingDoor.transform.position = Vector3.Lerp(fromPos, toPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        movingDoor.transform.position = toPos;

        yield return new WaitForSeconds(2);
        this.cameraPlayer1.SetActive(true);
        this.cameraPlayer2.SetActive(true);
        this.cameraOnDoor.SetActive(false);
    }
}