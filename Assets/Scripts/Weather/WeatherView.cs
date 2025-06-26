using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public class WeatherView : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI TempText;
    public TextMeshProUGUI LabelText;

    public void UpdateWeather(string iconUrl, string temp, string label)
    {
        TempText.text = temp;
        LabelText.text = label;
        LoadIcon(iconUrl).Forget();
    }

    private async UniTaskVoid LoadIcon(string url)
    {
        if (string.IsNullOrEmpty(url)) return;
        using (var req = UnityWebRequestTexture.GetTexture(url))
        {
            await req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.Success)
            {
                var tex = DownloadHandlerTexture.GetContent(req);
                Icon.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.5f,0.5f));
            }
        }
    }
} 