using UnityEngine;
using Zenject;

namespace Balda
{
    public class LocalizationManagerInstaller : MonoInstaller
    {
        [Inject]
        private IGameSettings _gameSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameSettings.Lang);
            Container.BindInterfacesAndSelfTo<LocalizationManager>().AsSingle().NonLazy();
        }
    }
}