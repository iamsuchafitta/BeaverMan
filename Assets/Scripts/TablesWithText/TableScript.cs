using UnityEngine;
using UnityEngine.Serialization;

public class TableScript : MonoBehaviour {
    [TextArea] [SerializeField] private string text;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("FirstPlayer") || other.CompareTag("SecondPlayer")) TablesManager.Instance.ShowText(this.text);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("FirstPlayer") || other.CompareTag("SecondPlayer")) TablesManager.Instance.HideText();
    }
}