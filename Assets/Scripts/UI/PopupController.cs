using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {
    public GameObject PopupPanel;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI ContentText;
    public Button HideButton;

    private void Awake() {
        if (HideButton != null)
            HideButton.onClick.AddListener(Hide);
    }

    public void Show(string title, string content) {
        TitleText.text = title;
        ContentText.text = content;
        PopupPanel.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(PopupPanel.GetComponent<RectTransform>());
    }

    public void Hide() {
        PopupPanel.SetActive(false);
    }
}