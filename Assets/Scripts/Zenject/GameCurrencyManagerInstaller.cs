    using Zenject;

    public class GameCurrencyManagerInstaller : Installer<GameCurrencyManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCurrencyManager>().AsSingle().NonLazy();
        }
    }
