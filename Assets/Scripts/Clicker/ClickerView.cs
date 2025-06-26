using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerView : MonoBehaviour {
    public Button ClickButton;
    public TextMeshProUGUI CurrencyText;
    public TextMeshProUGUI EnergyText;

    public System.Action OnClick;

    private void Awake() {
        ClickButton.onClick.AddListener(() => OnClick?.Invoke());
    }

    public void UpdateCurrency(int value) {
        CurrencyText.text = value.ToString();
    }

    public void UpdateEnergy(int value) {
        EnergyText.text = value.ToString();
    }
}