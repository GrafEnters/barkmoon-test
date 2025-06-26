using Zenject;
using UnityEngine;

public class ClickerInstaller : MonoInstaller
{
    public ClickerView View;
    public ClickerConfig Config;
    public override void InstallBindings()
    {
        Container.BindInstance(View).AsSingle();
        Container.BindInstance(Config).AsSingle();
        Container.Bind<ClickerModel>().AsSingle();
        Container.BindInterfacesTo<ClickerController>().AsSingle().NonLazy();
    }
} 