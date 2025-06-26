using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class WeatherController
{
    private readonly WeatherModel _model;
    private readonly WeatherView _view;
    private readonly RequestQueue _queue;
    private const string ApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
    private RequestToken _currentToken;

    public WeatherController(WeatherModel model, WeatherView view, RequestQueue queue)
    {
        _model = model;
        _view = view;
        _queue = queue;
    }

    public void RequestWeather()
    {
        CancelCurrent();
        _currentToken = new RequestToken();
        _queue.Enqueue(() => FetchWeather(_currentToken));
    }

    public void CancelCurrent()
    {
        _currentToken?.Cancel();
    }

    private async Task FetchWeather(RequestToken token)
    {
        using (var req = UnityWebRequest.Get(ApiUrl))
        {
            var op = req.SendWebRequest();
            while (!op.isDone)
            {
                if (token.IsCancellationRequested)
                    return;
                await Task.Yield();
            }
            if (req.result != UnityWebRequest.Result.Success)
                return;
            var json = req.downloadHandler.text;
            var data = JsonUtility.FromJson<WeatherApiResponse>(json);
            if (data?.properties?.periods != null && data.properties.periods.Length > 0)
            {
                // Берём первый дневной период (isDaytime == true), иначе первый
                var period = System.Array.Find(data.properties.periods, p => p.isDaytime) ?? data.properties.periods[0];
                _model.IconUrl = period.icon;
                _model.TodayTemp = period.temperature + period.temperatureUnit;
                _model.TodayLabel = period.name;
                _view.UpdateWeather(_model.IconUrl, _model.TodayTemp, _model.TodayLabel);
            }
        }
    }
} 