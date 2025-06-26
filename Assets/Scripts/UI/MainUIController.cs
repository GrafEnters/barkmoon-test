using UnityEngine;
using Zenject;

public class MainUIController : MonoBehaviour {
    public GameObject ClickerTab;
    public GameObject WeatherTab;
    public GameObject BreedsTab;

    [Inject]
    private WeatherController _weatherController;
    [Inject]
    private BreedsController _breedsController;

    public void ShowClickerTab(bool isOn) {
        ClickerTab.SetActive(isOn);
        if (!isOn) return;
        _breedsController.CancelAll();
    }

    public void ShowWeatherTab(bool isOn) {
        WeatherTab.SetActive(isOn);
        if (isOn) {
            _weatherController.RequestWeather();
            _breedsController.CancelAll();
        }
    }

    public void ShowBreedsTab(bool isOn) {
        BreedsTab.SetActive(isOn);
        if (isOn) {
            _breedsController.RequestBreeds();
        } else {
            _breedsController.CancelAll();
        }
    }
}