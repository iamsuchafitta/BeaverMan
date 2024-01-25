using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour {
    public float typingSpeed = 0.1f;
    [TextArea] [SerializeField] private string fullText;
    private TextMeshProUGUI _textMeshProUGUI;
    
    private void Start() {
        this._textMeshProUGUI = this.GetComponent<TextMeshProUGUI>();
        this._textMeshProUGUI.text = "";
        this.StartCoroutine(this.TypeText());
    }

    private System.Collections.IEnumerator TypeText() {
        foreach (var letter in this.fullText) {
            this._textMeshProUGUI.text += letter;
            yield return new WaitForSecondsRealtime(this.typingSpeed);
        }
    }
}