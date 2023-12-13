using UnityEngine;
using Zenject;

namespace Balda
{
    public class GameSettingsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameSettings>().To<GameSettings>().AsSingle();
        }
    }
}