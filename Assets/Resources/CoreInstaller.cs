using Zenject;

public class CoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ShouldLoadFlag>().AsSingle().NonLazy();
    }
}