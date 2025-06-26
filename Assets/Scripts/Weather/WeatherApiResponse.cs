[System.Serializable]
public class WeatherApiResponse
{
    public Properties properties;
}

[System.Serializable]
public class Properties
{
    public Period[] periods;
}

[System.Serializable]
public class Period
{
    public int number;
    public string name;
    public bool isDaytime;
    public int temperature;
    public string temperatureUnit;
    public string icon;
    public string shortForecast;
} 