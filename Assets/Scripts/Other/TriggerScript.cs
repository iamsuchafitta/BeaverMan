using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour {
    [SerializeField] private UnityEvent onTriggerEnter2DEvent;

    private void OnTriggerEnter2D(Collider2D other) {
        this.onTriggerEnter2DEvent?.Invoke();
    }
}