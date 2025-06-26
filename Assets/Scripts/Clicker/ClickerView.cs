using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class ClickerView : MonoBehaviour {
    public Button ClickButton;
    public TextMeshProUGUI CurrencyText;
    public TextMeshProUGUI EnergyText;
    public GameObject VFXTextPrefab;
    public RectTransform VFXParent;
    public ClickerConfig Config;

    public System.Action OnClick;

    private void Awake() {
        ClickButton.onClick.AddListener(() => {
            PlayButtonBounce();
            OnClick?.Invoke();
        });
    }

    public void UpdateCurrency(int value) {
        CurrencyText.text = value.ToString();
    }

    public void UpdateEnergy(int value) {
        EnergyText.text = value.ToString();
    }

    public void SpawnVFX(Vector2 screenPosition, string text)
    {
        var vfxObj = Instantiate(VFXTextPrefab, VFXParent);
        var vfxText = vfxObj.GetComponent<TextMeshProUGUI>();
        vfxText.text = text;
        vfxObj.transform.position = screenPosition;
        vfxObj.transform.SetAsLastSibling();
        // Анимация вверх
        vfxObj.transform.DOMoveY(vfxObj.transform.position.y + 80f, 0.7f).SetEase(Ease.OutQuad);
        // Fade out
        vfxText.DOFade(0, 0.7f).SetEase(Ease.InQuad);
        // Удаление
        RemoveVFXAfterDelay(vfxObj, 0.7f).Forget();
    }

    private async UniTaskVoid RemoveVFXAfterDelay(GameObject obj, float delay)
    {
        await UniTask.Delay((int)(delay * 1000));
        Destroy(obj);
    }

    private void PlayButtonBounce()
    {
        var rect = ClickButton.transform as RectTransform;
        Vector2 localPoint;
        // Получаем позицию мыши относительно кнопки
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out localPoint);
        // Нормализуем: -0.5...0.5 по X и Y (центр — 0,0)
        Vector2 normalized = new Vector2(
            Mathf.Clamp(localPoint.x / (rect.rect.width * 0.5f), -1f, 1f),
            Mathf.Clamp(localPoint.y / (rect.rect.height * 0.5f), -1f, 1f)
        );
        // Задаём максимальный угол наклона
        float maxAngle = Config != null ? Config.ButtonMaxAngle : 18f;
        float angleX = -normalized.y * maxAngle; // Вверх/вниз
        float angleY = normalized.x * maxAngle;  // Влево/вправо
        float duration = Config != null ? Config.ButtonPressDuration : 0.13f;
        float returnDuration = Config != null ? Config.ButtonReturnDuration : 0.25f;
        rect.DOKill();
        rect.DOLocalRotateQuaternion(Quaternion.Euler(angleX, angleY, 0), duration).SetEase(Ease.OutQuad)
            .OnComplete(() =>
                rect.DOLocalRotateQuaternion(Quaternion.identity, returnDuration).SetEase(Ease.OutElastic)
            );
    }
}