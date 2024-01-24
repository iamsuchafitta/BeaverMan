using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.1f;
    public string fullText;

    private string currentText = "";
    private bool isTyping = false;

    private void Start()
    {
        StartTyping();
    }

    private void Update()
    {
        if (isTyping)
        {
            currentText = fullText.Substring(0, Mathf.Min(fullText.Length, currentText.Length + 1));
            GetComponent<TextMeshProUGUI>().text = currentText;

            if (currentText == fullText)
            {
                isTyping = false;
            }
        }
    }

    public void StartTyping()
    {
        currentText = "";
        isTyping = true;
        StartCoroutine(TypeText());
    }

    private System.Collections.IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            currentText += letter;
            GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }
}
