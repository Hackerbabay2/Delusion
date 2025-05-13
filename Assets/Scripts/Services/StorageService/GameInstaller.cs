using KinematicCharacterController.Examples;
using Zenject;

namespace Storage.Scripts
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStorageService>().To<JsonToFileStorageService>().AsSingle();
            Container.Bind<StorageService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DayEventService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DayCycle>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerStats>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ExamplePlayer>().FromComponentInHierarchy().AsSingle();
        }
    }
}