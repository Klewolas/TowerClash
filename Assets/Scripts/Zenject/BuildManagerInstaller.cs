using UnityEngine;
using Zenject;

public class BuildManagerInstaller : MonoInstaller
{
    [SerializeField] private BuildManager _buildManager;
    public override void InstallBindings()
    {
        Container.BindInstance(_buildManager).AsSingle().NonLazy();
    }
}