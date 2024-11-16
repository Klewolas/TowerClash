using Zenject;

public class NonMonoInstallersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        GameCurrencyManagerInstaller.Install(Container);
        BuildManagerInstaller.Install(Container);
    }
}