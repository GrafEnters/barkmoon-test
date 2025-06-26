using Zenject;
using UnityEngine;

public class BreedsInstaller : MonoInstaller
{
    public BreedsView View;
    public PopupController Popup;
    public override void InstallBindings()
    {
        Container.BindInstance(View).AsSingle();
        Container.BindInstance(Popup).AsSingle();
        Container.Bind<BreedsController>().AsSingle().NonLazy();
    }
} 