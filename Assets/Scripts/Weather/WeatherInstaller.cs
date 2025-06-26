using Zenject;
using UnityEngine;

public class WeatherInstaller : MonoInstaller
{
    public WeatherView View;
    public override void InstallBindings()
    {
        Container.BindInstance(View).AsSingle();
        Container.Bind<WeatherModel>().AsSingle();
        Container.Bind<WeatherController>().AsSingle().NonLazy();
    }
} 