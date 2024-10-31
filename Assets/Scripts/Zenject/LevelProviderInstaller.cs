using UnityEngine;
using Zenject;

public class LevelProviderInstaller : MonoInstaller
{
    [SerializeField] private LevelProvider _levelProvider;
    public override void InstallBindings()
    {
        Container.BindInstance(_levelProvider).AsSingle().NonLazy();
    }
}