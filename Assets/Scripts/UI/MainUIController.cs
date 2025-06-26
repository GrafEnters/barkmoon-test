using UnityEngine;
using Zenject;

public class MainUIController : MonoBehaviour {
    public GameObject ClickerTab;
    public GameObject WeatherTab;
    public GameObject BreedsTab;

    [Inject]
    private WeatherController _weatherController;

    public void ShowClickerTab(bool isOn) {
        ClickerTab.SetActive(isOn);
    }

    public void ShowWeatherTab(bool isOn) {
        WeatherTab.SetActive(isOn);
        if (isOn) {
            _weatherController.RequestWeather();
        }
    }

    public void ShowBreedsTab(bool isOn) {
        BreedsTab.SetActive(isOn);
    }
}