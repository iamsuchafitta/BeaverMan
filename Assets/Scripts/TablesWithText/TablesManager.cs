using TMPro;
using UnityEngine;

public class TablesManager : MonoBehaviour {
    public static TablesManager Instance { get; private set; }

    [SerializeField] private GameObject table;
    [SerializeField] private TMP_Text tableText;

    private void Awake() {
        if (!Instance) Instance = this;
        else Destroy(this.gameObject);
    }

    public void ShowText(string text) {
        if (this.table.activeSelf) return;
        this.table.SetActive(true);
        this.tableText.text = text;
    }

    public void HideText() {
        this.table.SetActive(false);
    }
}
