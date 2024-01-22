using System.Collections;
using System.Linq;
using UnityEngine;

public class Level4CrankScript : MonoBehaviour {
    public GameObject leafs;
    private CompositeCollider2D _leafsCollider;
    private bool _isCranked = false;
    public float moveTime = 2f;
    private Vector3 _targetPosition;
    private Coroutine _moveLeafsCoroutine;
    private bool _isPlayerInLeafs = false;

    // Start is called before the first frame update
    private void Start() {
        this._targetPosition = this.leafs.transform.position;
        this._leafsCollider = this.leafs.GetComponent<CompositeCollider2D>();
    }

    private void Update() {
        // Set _isPlayerInLeafs to true if player is in leafs
        var leafsBounds = this._leafsCollider.bounds;
        var overlappingColliders = Physics2D.OverlapBoxAll(leafsBounds.center, leafsBounds.size, 0f);
        this._isPlayerInLeafs = overlappingColliders
            .Any(collider => collider.gameObject.CompareTag("FirstPlayer") || collider.gameObject.CompareTag("SecondPlayer"));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Moving leafs up");
        if (this._isCranked) return;
        if (!other.gameObject.CompareTag("FirstPlayer") && !other.gameObject.CompareTag("SecondPlayer")) return;
        this._isCranked = true;
        // move leafs by 3 up;
        this._targetPosition = new Vector3(this._targetPosition.x, this._targetPosition.y + 3, this._targetPosition.z);
        if (this._moveLeafsCoroutine != null) this.StopCoroutine(this._moveLeafsCoroutine);
        this._moveLeafsCoroutine = this.StartCoroutine(this.MoveLeafs(this.leafs.transform.position));
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Moving leafs down");
        if (!this._isCranked) return;
        if (!other.gameObject.CompareTag("FirstPlayer") && !other.gameObject.CompareTag("SecondPlayer")) return;
        this._isCranked = false;
        // move leafs by 3 down;
        this._targetPosition = new Vector3(this._targetPosition.x, this._targetPosition.y - 3, this._targetPosition.z);
        if (this._moveLeafsCoroutine != null) this.StopCoroutine(this._moveLeafsCoroutine);
        this._moveLeafsCoroutine = this.StartCoroutine(this.MoveLeafs(this.leafs.transform.position));
    }

    private IEnumerator MoveLeafs(Vector3 fromPos) {
        while (this._isPlayerInLeafs) yield return null;
        this._leafsCollider.isTrigger = this._isCranked;
        float elapsedTime = 0;
        while (elapsedTime < this.moveTime) {
            this.leafs.transform.position = Vector3.Lerp(fromPos, this._targetPosition, (elapsedTime / this.moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.leafs.transform.position = this._targetPosition;
    }
}