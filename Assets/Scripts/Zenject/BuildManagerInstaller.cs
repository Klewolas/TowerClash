using UnityEngine;
using Zenject;

public class BuildManagerInstaller : Installer<BuildManagerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<BuildManager>().AsSingle().NonLazy();
    }
}